using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WinSelenium
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
        IWebDriver driver;
        IWebElement final_span;


        private void abreSelenium()
        {
            string str = @"D:\Unidade G\Sistemas\Projetos Web\Bruno\Interacoes\SpeechToText\WinSelenium\chromedriver_win32";

            var chrome_options = new ChromeOptions();

            chrome_options.AddArgument("allow-file-access-from-files");
            chrome_options.AddArgument("use-fake-device-for-media-stream");
            chrome_options.AddArgument("use-fake-ui-for-media-stream");

            /*

                    prefs = {"hardware.audio_capture_allowed_urls" : ["example.org"]}
                    chrome_options.add_experimental_option("prefs",prefs)
                    driver = webdriver.Chrome(chrome_options=chrome_options)

            */
            string URLTraducao = "http://localhost:8080/chromehtml/Chrome_browser_speech.html";
            driver = new ChromeDriver(str, chrome_options);

            driver.Navigate().GoToUrl(URLTraducao);

            IWebElement myField = driver.FindElement(By.Id("start_button"));
            myField.Click();

         

        }

        private void obtemTexto()
        {

            if (driver != null)
            {

                IWebElement final_span = driver.FindElement(By.Id("final_span"));

                string text = final_span.Text;

                setText(text); // richTextBox1.Text = text;

            }
        }



        delegate void MensagemCallback(string text);
        /*
        void setMensagem(string text)
        {
            if (label2.InvokeRequired)
            {
                MensagemCallback d = new MensagemCallback(setMensagem);
                this.Invoke(d, new object[] { text });

            }
            else
            {
                label2.Text = text;
            }

        }
        */
        void setText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                MensagemCallback d = new MensagemCallback(setText);
                this.Invoke(d, new object[] { text });

            }
            else
            {
                richTextBox1.Text = text;
            }

        }




        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            abreSelenium();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (driver != null)
            {
                IWebElement hd_status = driver.FindElement(By.Id("hd_status"));

                if (hd_status.GetAttribute("value") == "on")
                {

                    IWebElement myField = driver.FindElement(By.Id("start_button"));
                    myField.Click();
                }
                

                obtemTexto();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (driver != null)
            {
                driver.Close();

            }
        }
    }
}
