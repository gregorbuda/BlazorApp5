using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp5.Shared.Models.AzureVideoIndexer.ListEmotionsSentiments
{    
    public class Sentiments
    {
        public string sentimentKey { get; set; }
        public decimal seenDurationRatio { get; set; }
    }

    public class Emotions
    {
        public string type { get; set; }
        public decimal seenDurationRatio { get; set; }
    }

    public class SummarizedInsights
    {
       public List<Sentiments> sentiments { get; set; }
       public List<Emotions> emotions { get; set; }
    }
    public class ListEmotionsSemtiments
    {
        public SummarizedInsights summarizedInsights { get; set; }
        public string name { get; set; }
    }
}
