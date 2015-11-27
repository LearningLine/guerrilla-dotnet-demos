using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace PLINQ
{
    public class EnumerableBuffer<T> : IEnumerable<T>
    {
        private List<T> items;

        public EnumerableBuffer(IEnumerable<T> toBuffer)
        {
            items = toBuffer.ToList();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            OddEven();
            //HappySites();
        }

        private static void OddEven()
        {
            IEnumerable<int> numbers =
                   new EnumerableBuffer<int>( Numbers(50000000) );
               
            Console.Write("Filtering..");
            Stopwatch timer = Stopwatch.StartNew();

            var evenNumbers = (from number in numbers
                               where number % 2 == 0
                               select number).ToArray();

            Console.WriteLine(timer.Elapsed);
        }


        private static IEnumerable<int> Numbers(int nNumbers)
        {
            Random rnd = new Random();

            for (int i = 0; i < nNumbers; i++)
            {
                yield return rnd.Next();
            }
        }

        private static void HappySites()
        {
            Stopwatch timer = Stopwatch.StartNew();

            var happySites =
                from site in urls
                let content = GetContent(site)
                where content.ToLower().Contains("happy")
                select site;


            int nHappySites = 0;
            foreach (string site in happySites)
            {
                Console.WriteLine(site);
                nHappySites++;
            }

            Console.WriteLine(timer.Elapsed);
            Console.WriteLine("Total {0}", nHappySites);
        }

        private static string GetContent(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = 8000;
                WebResponse response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException error)
            {
               // Console.WriteLine("{0} : {1}", url, error.Message);
                // Obviously not a happy site

                return "";
            }
        }
        public static void SpellCheck()
        {
            const string WORD_FILE = "AllWords.txt";
            const int DOCUMENT_WORD_COUNT = 9000000;
            const int NUMBER_OF_BAD_WORDS = 5;

            var words = LoadWords(WORD_FILE);
            var wordsArray = words.ToArray();



            var rnd = new Random();
            string[] document = Enumerable.Range(1, DOCUMENT_WORD_COUNT)
                                .Select(_ => wordsArray[rnd.Next(words.Count)])
                                .ToArray();

            for (int nBadWord = 0; nBadWord < NUMBER_OF_BAD_WORDS; nBadWord++)
            {
                document[rnd.Next(0, DOCUMENT_WORD_COUNT)] = "adsa";
            }


            Stopwatch timer = Stopwatch.StartNew();
            var badWords = document
                .Select((word, index) => new { Word = word, Index = index })
                .Where(wi => !words.Contains(wi.Word))
                .OrderBy(wi => wi.Index)
                .ToList();

            Console.WriteLine("Took {0}", timer.Elapsed);
            foreach (var badWord in badWords)
            {
                Console.WriteLine(badWord);
            }
        }

        private static HashSet<string> LoadWords(string wordFile)
        {
            HashSet<string> words;

            if (!File.Exists(wordFile))
            {
                Console.Write("Downloading...");
                var client = new WebClient();

                client.DownloadFile("http://www.albahari.com/ispell/allwords.txt", wordFile);
                Console.WriteLine("Done");
            }

            words = new HashSet<string>(File.ReadLines(wordFile));
            return words;
        }


        #region urls
        static string[] urls = new string[]
            {
               "https://login.yahoo.com",
               "http://news.bbc.co.uk",
               "http://www.bbc.co.uk",
               "http://my.yahoo.com",
               "http://toolbar.netcraft.com",
               "http://fr.yahoo.com",
               "http://de.yahoo.com",
               "http://sports.yahoo.com",
               "http://www.theregister.co.uk",
               "http://viewmorepics.myspace.com",
               "http://uk.yahoo.com",
               "http://www.guardian.co.uk",
               "http://www.animefreak.tv",
               "http://att.my.yahoo.com",
               "http://messaging.myspace.com",
               "http://start.ubuntu.com",
               "http://it.yahoo.com",
               "http://fr.news.yahoo.com",
               "http://ubuntuforums.org",
               "http://www.clicktelligence.co.uk",
               "http://de.partypoker.com",
               "http://eu1.badoo.com",
               "http://www.mangareader.net",
               "http://de.search.yahoo.com",
               "http://www.racingpost.com",
               "http://es.yahoo.com",
               "http://www.booking.com",
               "http://www.last.fm",
               "http://uk.search.yahoo.com",
               "http://www.partypoker.it",
               "http://cm.my.yahoo.com",
               "http://searchservice.myspace.com",
               "http://www.partypoker.com",
               "http://fr.search.yahoo.com",
               "http://uptime.netcraft.com",
               "http://profile.myspace.com",
               "http://login.yahoo.com",
               "http://de.news.yahoo.com",
               "http://profiles.yahoo.com",
               "http://de.docs.yahoo.com",
               "http://www.ryanair.com",
               "http://tempsreel.nouvelobs.com",
               "http://planetsuzy.org",
               "http://uk.news.yahoo.com",
               "http://www.ubuntu.com",
               "http://linksave.in",
               "http://news.netcraft.com",
               "http://www.viadeo.com",
               "http://www.ft.com",
               "http://www.virginmedia.com            ",
               "http://pulse.yahoo.com                ",
               "http://www.rightmove.co.uk            ",
               "http://www.hotukdeals.com             ",
               "http://www.ciao.it                    ",
               "http://www.boerse.bz                  ",
               "http://forums.theregister.co.uk       ",
               "https://help.ubuntu.com               ",
               "http://www.play.com                   ",
               "http://www.relink.us                  ",
               "http://fr.partypoker.com              ",
               "http://id.yahoo.com                   ",
               "https://www.hsbc.co.uk                ",
               "http://www.theinquirer.net            ",
               "http://fr.sports.yahoo.com            ",
               "https://online.lloydstsb.co.uk        ",
               "http://webranker.justanalytics.co.uk  ",
               "http://vids.myspace.com               ",
               "http://www.runescape.com              ",
               "http://it.finance.yahoo.com           ",
               "http://it.eurosport.yahoo.com         ",
               "http://forums.moneysavingexpert.com   ",
               "http://www.lloydstsb.com              ",
               "http://www.wowwiki.com                ",
               "http://it.search.yahoo.com            ",
               "http://www.easyjet.com                ",
               "http://home.bt.yahoo.com              ",
               "http://g.sports.yahoo.com             ",
               "http://www.orange.co.uk               ",
               "https://ibank.barclays.co.uk          ",
               "http://www.lastfm.de                  ",
               "http://news.sky.com                   ",
               "http://uk.eurosport.yahoo.com         ",
               "http://www.talktalk.co.uk             ",
               "http://www.skysports.com              ",
               "http://www.elmundodeportivo.es        ",
               "https://www.nwolb.com                 ",
               "http://www.digitalspy.co.uk           ",
               "https://olb2.nationet.com             ",
               "http://de.eurosport.yahoo.com         ",
               "https://www.majesticseo.com           ",
               "http://www.channel4.com               ",
               "http://mail.yahoo.com                 ",
               "http://www.hsbc.co.uk                 ",
            };
        #endregion
    }
}
