using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Filer
{
    class ShowBooks
    {
        public static List<Book> Show(string language, int number)
        {
            List<Book> bookslist = new List<Book>();
            var options = new ChromeOptions();
            options.AddArgument("headless");//Аргумент, чтобы не открывался браузер
            options.AddArgument("disable-extensions");
            using (var browser = new ChromeDriver(options))
            {
                browser.Navigate().GoToUrl($"https://www.amazon.com/"); 
                IWebElement search = browser.FindElement(By.Id("twotabsearchtextbox"));
                search.SendKeys(language + OpenQA.Selenium.Keys.Enter);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                //скачиваем страницу браузер
                doc.LoadHtml(browser.PageSource);
                //просматриваем только результаты поиска
                var tables = doc.DocumentNode.SelectNodes(".//div[@class='sg-col-4-of-12 s-result-item s-asin sg-col-4-of-16 sg-col sg-col-4-of-20']");
                if (tables != null)
                {
                    foreach (var table in tables)
                    {
                        if (bookslist.Count < number)
                        {
                            Book book = new Book();
                            //заполняем информацией о каждой книге
                            book.Name = table.SelectSingleNode(".//span[@class='a-size-base-plus a-color-base a-text-normal']") == null ? "null" : table.SelectSingleNode(".//span[@class='a-size-base-plus a-color-base a-text-normal']").InnerText;
                            book.Author = table.SelectSingleNode(".//a[@class='a-size-base a-link-normal']") == null ? "null" : table.SelectSingleNode(".//a[@class='a-size-base a-link-normal']").InnerText;
                            book.Price = table.SelectSingleNode(".//span[@class='a-offscreen']") == null ? "null" : table.SelectSingleNode(".//span[@class='a-offscreen']").InnerText;
                            book.Rating = table.SelectSingleNode(".//span[@class='a-icon-alt']") == null ? "null" : table.SelectSingleNode(".//span[@class='a-icon-alt']").InnerText.Substring(0, 3);
                            book.Link = table.SelectSingleNode(".//a[@class='a-link-normal a-text-normal']") == null ? "null" : "http://amazon.com" + table.SelectSingleNode(".//a[@class='a-link-normal a-text-normal']").Attributes["href"].Value;
                            bookslist.Add(book);
                        }
                    }
                }
            }
            return bookslist;
        }
    }
}
