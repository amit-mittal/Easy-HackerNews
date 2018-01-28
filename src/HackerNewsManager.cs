using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyHackerNews
{
    internal class HackerNewsManager
    {
        private static string ApiUrl = "https://node-hnapi.herokuapp.com/news";

        public List<HackerNews> DoWork()
        {
            string html = string.Empty;
            string url = ApiUrl;
            List<HackerNews> newsList = new List<HackerNews>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Console.WriteLine(html);

            var items = JsonConvert.DeserializeObject<JArray>(html);
            foreach (var item in items)
            {
                HackerNews news = new HackerNews();
                news.title = (string) item["title"];
                news.url = (string) item["url"];
                news.points = 0; //(item["points"] == null ? 0 : (int) item["points"])
                news.comments_count = 55;

                newsList.Add(news);
            }

            return newsList;
        }
    }

    /// <summary>
    /// "id":16245142,
    /// "title":"An Italian Song That Sounds Like English But Is Nonsense",
    /// "points":213,
    /// "user":"idiocratic",
    /// "time":1517035111,
    /// "time_ago":"7 hours ago",
    /// "comments_count":99,
    /// "type":"link",
    /// "url":"https://www.atlasobscura.com/articles/deep-roots-italian-song-sounds-like-english-american-medieval-comedy-nonsense",
    /// "domain":"atlasobscura.com"
    /// </summary>
    public class HackerNews
    {
        public int id;

        public string title;

        public int points;

        public string user;

        public long time;

        public string time_ago;

        public int comments_count;

        public string type;

        public string url;

        public string domain;
    }
}
