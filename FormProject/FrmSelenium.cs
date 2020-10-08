using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormProject
{
    public partial class FrmSelenium : Form
    {
        public FrmSelenium()
        {
            InitializeComponent();
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            DtgList.DataSource = null;
            IWebDriver webDriver = new FirefoxDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            webDriver.Manage().Window.Maximize();

            webDriver.Navigate().GoToUrl("https://www.sahibinden.com/" + CmbBrands.Text.ToLower());

            Thread.Sleep(2000);

            webDriver.FindElement(By.Name("a5_min")).SendKeys(MskMin.Text);
            webDriver.FindElement(By.Name("a5_max")).SendKeys(MskMax.Text);

            webDriver.FindElement(By.Name("price_max")).SendKeys(MskMaxPrice.Text);

            webDriver.FindElement(By.XPath("//*[@id='searchResultsSearchForm']/div/div[3]/div[24]/button")).Click();

            Thread.Sleep(2000);

            List<HeaderClass> headers = webDriver.FindElements(By.ClassName("classifiedTitle")).Select(x => new HeaderClass(x.Text, x.GetAttribute("href"))).ToList();

            var priceList = webDriver.FindElements(By.ClassName("searchResultsPriceValue")).Select(x => x.Text).ToList();

            var locationList = webDriver.FindElements(By.ClassName("searchResultsLocationValue")).Select(x => x.Text).ToList();

            List<Car> cars = new List<Car>();
            for (int i = 0; i < priceList.Count; i++)
            {
                Car car = new Car(headers[i].Header, headers[i].Link, priceList[i], locationList[i]);
                cars.Add(car);
            }

            DtgList.DataSource = cars;
            webDriver.Quit();
        }

        private void DtgList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string link = DtgList.CurrentRow.Cells["Link"].Value.ToString();
            System.Diagnostics.Process.Start(link);
        }
    }
    class HeaderClass
    {
        string header, link;

        public string Header { get => header; set => header = value; }
        public string Link { get => link; set => link = value; }

        public HeaderClass(string header, string link)
        {
            this.header = header;
            this.link = link;
        }
    }

    class Car
    {
        string header, link, price, location;

        public string Header { get => header; set => header = value; }
        public string Link { get => link; set => link = value; }
        public string Price { get => price; set => price = value; }
        public string Location { get => location; set => location = value; }

        public Car(string header, string link, string price, string location)
        {
            this.header = header;
            this.link = link;
            this.price = price;
            this.location = location;
        }
    }
}

