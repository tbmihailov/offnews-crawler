using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OffnewsCrawler.OpinionCrawler
{
    public class OpinionItem
    {
        public int id { get; set; }
        public string ip_address { get; set; }
        public string act_id { get; set; }
        public string name { get; set; }
        public string opinion { get; set; }
        public DateTime date { get; set; }
        public string clientid { get; set; }
        public string modul { get; set; }
        public int up { get; set; }
        public int down { get; set; }
        public string email { get; set; }
        public string show { get; set; }
        public string user_agent { get; set; }
        public string status { get; set; }
        public string uhash { get; set; }
    }
}
