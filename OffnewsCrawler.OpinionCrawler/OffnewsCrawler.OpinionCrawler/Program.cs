using CsvHelper;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OffnewsCrawler.OpinionCrawler
{
    class OffnewsCrawler
    {
        static ILog _logger = LogManager.GetLogger("logger");
        public static ILog Logger
        {
            get { return OffnewsCrawler._logger; }
        }
        static OffnewsCrawler()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        static void Main(string[] args)
        {
            //int testOpinion = 500000;
            //OpinionItem item = CrawlSingleOpinion(testOpinion);
            //return;

            int from = 1;
            if (args.Length >= 1)
            {
                from = int.Parse(args[0]);
            }
            Logger.InfoFormat("from - {0}", from);

            int to = 100;
            if (args.Length >= 2)
            {
                to = int.Parse(args[1]);
            }
            Logger.InfoFormat("to - {0}", to);

            int delayInMs = 10;
            if (args.Length >= 3)
            {
                delayInMs = int.Parse(args[2]);
            }

            Logger.InfoFormat("delay in ms - {0}", delayInMs);

            int itemsPerFile = 10;
            if (args.Length >= 4)
            {
                itemsPerFile = int.Parse(args[3]);
            }
            Logger.InfoFormat("items per file - {0}", itemsPerFile);

            Logger.Info("Start...");
            DateTime start = DateTime.Now;
            CrawlOpinionsAndSaveToFile(from, to, 10, itemsPerFile);
            DateTime end = DateTime.Now;

            Logger.InfoFormat("End. Total time {0}", end-start);

            //test
            //int testSignalNumber = 2286;
            //Signal testSignal = CrawlSingleSignalFromCallSofiaBg(testSignalNumber);
        }

        private static List<OpinionItem> CrawlOpinions(int fromSignalId, int toSignalId, int timeBetweenHttpRequestsInMiliseconds)
        {
            List<OpinionItem> signals = new List<OpinionItem>();
            for (int signalId = fromSignalId; signalId <= toSignalId; signalId++)
            {
                try
                {
                    //Console.Write("Downloading signal [{0}]...", signalId);
                    OpinionItem signal = CrawlSingleOpinion(signalId);
                    if (signal != null)
                    {
                        signals.Add(signal);
                        Logger.InfoFormat("{0} - OK!", signalId);
                    }
                    Thread.Sleep(timeBetweenHttpRequestsInMiliseconds);
                }
                catch (NotFoundOpinionException se)
                {
                    Logger.InfoFormat("{0} - Not found!", signalId);
                }
                catch (Exception e)
                {
                    Logger.ErrorFormat("{0} - FAILED! - Exception :{1}", signalId, e.Message);
                }
            }

            return signals;
        }

        private static void CrawlOpinionsAndSaveToFile(int fromSignalId, int toSignalId, int timeBetweenHttpRequestsInMiliseconds, int itemsPerFile = 10000)
        {
            List<OpinionItem> signals = new List<OpinionItem>();
            int i = 1;
            
            int itemsChecked = 0;
            int successfullyCrawled = 0;
            JsonSerializer serializer = new JsonSerializer();

            string outputFileName = string.Empty;
            TextWriter textWriter = null;

            for (int signalId = fromSignalId; signalId <= toSignalId; signalId++)
            {
                try
                {
                    if(textWriter == null){
                        outputFileName = string.Format("data\\offnews-opinions-{0}.json", signalId);
                        textWriter = File.AppendText(outputFileName);
                        Logger.InfoFormat("Output file - {0}", outputFileName);
                    }
                    //Console.Write("Downloading signal [{0}]...", signalId);
                    OpinionItem opinionItem = CrawlSingleOpinion(signalId);
                    if (opinionItem != null)
                    {
                        serializer.Serialize(textWriter, opinionItem);
                        textWriter.WriteLine();
                        Logger.InfoFormat("{0} - OK!", signalId);
                        successfullyCrawled++;
                    }
                    Thread.Sleep(timeBetweenHttpRequestsInMiliseconds);
                }
                catch (NotFoundOpinionException se)
                {
                    Logger.InfoFormat("{0} - Not found!", signalId);
                }
                catch (Exception e)
                {
                    Logger.ErrorFormat("{0} - FAILED! - Exception :{1}", signalId, e.Message);
                }

                itemsChecked++;

                if(itemsChecked%itemsPerFile == 0){
                    if(textWriter!=null){
                        textWriter.Close();
                        textWriter = null;
                        Logger.InfoFormat("Successfull crawled {0}/{1}", successfullyCrawled, itemsChecked);
                    }
                }
            }
        }

        private static OpinionItem CrawlSingleOpinion(int signalId)
        {
            string itemUrl = string.Format("http://offnews.bg/requests.php?act=opinion&opid={0}", signalId);

            //download page html content
            WebClient webClient = new WebClient();
            string data = webClient.DownloadString(itemUrl);

            if (data == "false")
            {
                Logger.InfoFormat("{0} - Not found!", signalId);
                return null;
            }

            OpinionItem opinion = null;
            try
            {
                opinion = JsonConvert.DeserializeObject<OpinionItem>(data);
            }
            catch (Exception e)
            {
                throw new NotFoundOpinionException(signalId);
            }

            return opinion;
        }
    }
}
