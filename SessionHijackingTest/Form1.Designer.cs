namespace SessionHijackingTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnGetCookies = new System.Windows.Forms.Button();
            this.btnSecondBrowserTest = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(58, 32);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(455, 20);
            this.txtUrl.TabIndex = 0;
            this.txtUrl.Text = "https://app.pluralsight.com/id/";
            // 
            // btnGetCookies
            // 
            this.btnGetCookies.Location = new System.Drawing.Point(58, 67);
            this.btnGetCookies.Name = "btnGetCookies";
            this.btnGetCookies.Size = new System.Drawing.Size(127, 23);
            this.btnGetCookies.TabIndex = 1;
            this.btnGetCookies.Text = "Get Cookies";
            this.btnGetCookies.UseVisualStyleBackColor = true;
            this.btnGetCookies.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSecondBrowserTest
            // 
            this.btnSecondBrowserTest.Location = new System.Drawing.Point(191, 67);
            this.btnSecondBrowserTest.Name = "btnSecondBrowserTest";
            this.btnSecondBrowserTest.Size = new System.Drawing.Size(151, 23);
            this.btnSecondBrowserTest.TabIndex = 2;
            this.btnSecondBrowserTest.Text = "Second Browser Test";
            this.btnSecondBrowserTest.UseVisualStyleBackColor = true;
            this.btnSecondBrowserTest.Visible = false;
            this.btnSecondBrowserTest.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(519, 31);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(68, 23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 114);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnSecondBrowserTest);
            this.Controls.Add(this.btnGetCookies);
            this.Controls.Add(this.txtUrl);
            this.Name = "Form1";
            this.Text = "Hijacking Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnGetCookies;
        private System.Windows.Forms.Button btnSecondBrowserTest;
        private System.Windows.Forms.Button btnGo;
    }
}

