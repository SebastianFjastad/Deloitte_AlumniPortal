using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using AlumniPortal.Models;

namespace AlumniPortal.Utilities
{
    public static class RSSReader
    {
        public static IEnumerable<RSSItem> GetRSSFeed(string url)
        {
            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.Encoding = Encoding.UTF8;

            var xmlData = client.DownloadString(url);

            XDocument xml = XDocument.Parse(xmlData);

            Regex rImgUrl = new Regex("<img[^>]+src=\"(.*?)\"");
            Match m = rImgUrl.Match(xml.ToString());

            Console.Write(m.Groups[1]);

            var items = (from post in xml.Descendants("item")
                             let imgurl = rImgUrl.Match(post.ToString()).Groups[1].ToString()
                             select new RSSItem
                             {
                                 Title = ((string)post.Element("title")),
                                 Link = ((string)post.Element("link")),
                                 Description = ((string)post.Element("description")),
                                 PubDate = ((string)post.Element("pubDate")),
                                 ImageUrl = imgurl == string.Empty ? "http://i.imgur.com/R6k0ky5.png" : imgurl
                             });

            return items;
        }
    }
}