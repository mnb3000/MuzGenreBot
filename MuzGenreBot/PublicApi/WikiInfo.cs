using Genbox.Wikipedia;
using Genbox.Wikipedia.Objects;
using Genbox.Wikipedia.Enums;
using Genbox.Wikipedia.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WikiDotNet;

namespace MuzGenreBot.PublicApi
{
    class Program1
    {
        static public string WikiInfo(string find)
        {
            WikiSearchSettings searchSettings = new WikiSearchSettings
            { RequestId = "Request ID", ResultLimit = 2, ResultOffset = 1, Language = "en" };

            WikiSearchResponse response = WikiSearcher.Search(find, searchSettings);

            WikiSearchResult result = response.Query.SearchResults[0];
            string res = $"{result.Title} \n{result.Preview}...\n\tAt {result.Url(searchSettings.Language)}\n\tLast edited at {result.LastEdited}\n";
            //res 
            return res;
        }

        static public string Wiki(string find)
        {
            WikipediaClient client = new WikipediaClient();
            client.Limit = 1;

            QueryResult results = client.Search(find);

            Console.WriteLine("Found " + results.Search.Count + " English results:");

            results.Search.FirstOrDefault();

            string res = null;

            foreach (Search s in results.Search)
            {
                res = $" ]] {s.TitleSnippet} [[ {s.Snippet} ";
            }

            return res;
        }
    }
}
