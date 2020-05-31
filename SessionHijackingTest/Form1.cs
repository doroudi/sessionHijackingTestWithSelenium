using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SessionHijackingTest
{
    public partial class Form1 : Form
    {
        private ChromeDriver driver;
        private ChromeDriver _secondDriver;
        private string currentUrl = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            var startIndex = txtUrl.Text.IndexOf("//") + 2;
            var endIndex = txtUrl.Text.IndexOf("/", startIndex);
            if(endIndex == -1)
            {
                endIndex = txtUrl.Text.Length;
            }
            driver.Navigate().GoToUrl(txtUrl.Text);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(3);
            currentUrl = txtUrl.Text.Substring(startIndex, endIndex - startIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(driver == null)
            {
                return;
            }

            var stringBuilder = new StringBuilder();
            var cookies = driver.Manage().Cookies.AllCookies;
            foreach (var cookie in cookies)
            {
                stringBuilder.Append(
                    $"{cookie.Name},{cookie.Value},{cookie.Domain},{cookie.Path},{cookie.Expiry},{cookie.IsHttpOnly},{cookie.Secure}," +
                    $"{Environment.NewLine}");
            }

            if(Directory.Exists("cookies"))
            {
                Directory.CreateDirectory("cookies");
            }
            using (TextWriter writer = new StreamWriter($"cookies\\{currentUrl}.txt"))
            {
                writer.Write(stringBuilder.ToString());
            }

            TestInSecondBrowser();
        }

        private void TestInSecondBrowser()
        {
            _secondDriver = new ChromeDriver();
            _secondDriver.Navigate().GoToUrl(txtUrl.Text);
            var cookies = new List<Cookie>();
            var cookiesContent = File.ReadAllLines($"cookies\\{currentUrl}.txt");
            foreach (var cookieLine in cookiesContent)
            {
                var spitted = cookieLine.Split(',');
                cookies.Add(new Cookie(spitted[0], spitted[1], spitted[2], spitted[3], DateTime.Now.AddMinutes(20)));
            }

            Thread.Sleep(2000);
            _secondDriver.Manage().Cookies.DeleteAllCookies();
            foreach (var cookie in cookies)
            {
                try
                {
                    _secondDriver.Manage().Cookies.AddCookie(cookie);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
            _secondDriver.Navigate().GoToUrl(driver.Url);
            _secondDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(3);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (driver != null)
                {
                    driver.Close();
                    driver.Dispose();
                }

                if (_secondDriver != null)
                {
                    _secondDriver.Close();
                    _secondDriver.Dispose();
                }
            }
            catch { }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TestInSecondBrowser();
        }
    }
}
