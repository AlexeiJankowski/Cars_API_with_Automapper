using System.Collections.Generic;
using System.Diagnostics;
using HtmlAgilityPack;

namespace BMW_API
{
    public class Parser 
    {
        public HtmlNodeCollection ParseCarList(string url = @"https://en.wikipedia.org/wiki/List_of_BMW_vehicles") 
        {            
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);

            return htmlDoc.DocumentNode.SelectNodes("//table[contains(@class, 'wikitable')]//tbody//tr//td");
        }              
    }
}