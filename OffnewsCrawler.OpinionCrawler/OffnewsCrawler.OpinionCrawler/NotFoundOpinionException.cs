using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OffnewsCrawler.OpinionCrawler
{
    public class NotFoundOpinionException : Exception
    {
        public int OpinionId { get; set; }

        public NotFoundOpinionException(int signalId)
        {
            this.OpinionId = signalId;
        }
    }
}
