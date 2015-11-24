using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCaching
{
    class Program
    {
        static void Main(string[] args)
        {
            WebCache cache = new WebCache();

            for (int nTask = 0; nTask < 5; nTask++)
            {
                Task.Factory.StartNew(() =>
                {
                    SimulateUser(cache);
                });
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        public static void SimulateUser(WebCache cache)
        {

            #region urls
            string[] urls = new string[]
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

            Random rnd = new Random();
            while (true)
            {
                int nUrl = rnd.Next(urls.Length);
                string page = cache.GetPage(urls[nUrl].Trim());


                Console.Write(".");
            }

        }
    }
}
