using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver webDriver = new FirefoxDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            webDriver.Navigate().GoToUrl("https:\\www.google.com.tr");

            webDriver.FindElement(By.Name("q")).SendKeys("araba");

            webDriver.FindElement(By.Id("hplogo")).Click();

            webDriver.FindElement(By.XPath("/html/body/div[2]/div[2]/form/div[2]/div[1]/div[3]/center/input[1]")).Click();

            var linkList = webDriver.FindElements(By.CssSelector("a"));

            var myList = linkList.Where(x => x.GetAttribute("href") != null);

            myList.Where(x => x.GetAttribute("href").Contains("www.sahibinden.com")).FirstOrDefault().Click();

            //webDriver.FindElement(By.CssSelector(".paging-size")).Click(); 50 İlan Gösterme Elementi

            webDriver.Navigate().GoToUrl(webDriver.Url + "/otomobil?pagingSize=50");

            Thread.Sleep(2000);

            webDriver.FindElement(By.Name("a5_min")).SendKeys("2015");
            webDriver.FindElement(By.Name("a5_max")).SendKeys("2018");

            webDriver.FindElement(By.XPath("/html/body/div[4]/div[4]/form/div/div[2]/div[24]/button")).Click();

            //webDriver.FindElement(By.XPath("//*[@id='hideSearchFilter']")); String İçinde Tek Tırnak Kullanımı

            Thread.Sleep(2000);

            var headerList = webDriver.FindElements(By.ClassName("classifiedTitle"));

            Console.WriteLine();
            Console.WriteLine("Liste Eleman Sayısı : " + headerList.Count);
            Console.WriteLine();
            foreach (var item in headerList)
            {
                Console.WriteLine(item.Text);
            }

            Console.WriteLine("Sayfa Sonu");

            //webDriver.Quit();

            Console.Read();
        }
    }
}
