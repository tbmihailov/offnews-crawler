using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OffnewsCrawler.Data
{
    public class Opinion
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string ActId { get; set; }
        public string Name { get; set; }
        public string OpinionContent { get; set; }
        public DateTime Date { get; set; }
        public string Clientid { get; set; }
        public string Modul { get; set; }
        public int Up { get; set; }
        public int Down { get; set; }
        public string Email { get; set; }
        public string Show { get; set; }
        public string UserAgent { get; set; }
        public string Status { get; set; }
        public string Uhash { get; set; }
    }
}
