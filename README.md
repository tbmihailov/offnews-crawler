# offnews-crawler
Offnews.bg opinions crawler collects data from the Offnews.bg service described in https://fen4o.com/blog/2015/09/12/offnews-privacy-failure.html.

How it works
------------
The crawler download opinion by {id} - http://offnews.bg/requests.php?act=opinion&opid={id}. It iterates over all opinions from m to n and tries to download info about them. However some opinions behind specific id-s is missing for an unknown reasons.
Downloaded data is saved in separate .json files with a specific name "data\\offnews-opinions-[first_opinion_in_file_id].json" with [itemsPerFile - default 10000] opinions per file.

How to use
----------
To run the crawler you need to build from source and run the OffnewsCrawler.OpinionCrawler.exe with parameters as shown below:

OffnewsCrawler.OpinionCrawler [fromOpinionId] [toOpinionId] [delayInMs] [itemsPerFile - default 10000]
OffnewsCrawler.OpinionCrawler 1 510000 1 10000

