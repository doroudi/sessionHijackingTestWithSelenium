using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

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
            var startIndex = textBox1.Text.IndexOf("//") + 2;
            var endInedx = textBox1.Text.IndexOf("/", startIndex);
            driver.Navigate().GoToUrl(textBox1.Text);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(3);
            currentUrl = textBox1.Text.Substring(startIndex,endInedx - startIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var stringBuilder = new StringBuilder();
            var cookies = driver.Manage().Cookies.AllCookies;
            foreach (var cookie in cookies)
            {
                stringBuilder.Append(
                    $"{cookie.Name},{cookie.Value},{cookie.Domain},{cookie.Path},{cookie.Expiry},{cookie.IsHttpOnly},{cookie.Secure},{Environment.NewLine}");
            }

            using (TextWriter writer = new StreamWriter($"{currentUrl}.txt"))
            {
                writer.Write(stringBuilder.ToString());
            }

            TestInSecondBrowser();
        }

        private void TestInSecondBrowser()
        {
            _secondDriver = new ChromeDriver();
            _secondDriver.Navigate().GoToUrl(textBox1.Text);
            var cookies = new List<Cookie>();
            var cookiesContent = File.ReadAllLines($"{currentUrl}.txt");
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

        private void button2_Click(object sender, EventArgs e)
        {
            TestInSecondBrowser();
        }
    }
}
