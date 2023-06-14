using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp5.Shared.Models.AzureVideoIndexer.ListLanguajes
{
    public class Result2
    {
            public string name { get; set; }
            public string languageCode { get; set; }
            public bool isRightToLeft { get; set; }
            public bool isSourceLanguage { get; set; }
            public bool isAutoDetect { get; set; }
            public bool isSupportedForLanguageDataset { get; set; }
            public bool isSupportedForPronunciationDataset { get; set; }
            public bool isSupportedForCustomModels { get; set; }
            public bool isSupportedForTranslation { get; set; }       
    }

    public class ListLanguajesResponse
    {
        public List<Result2> results { get; set; }
    }
}
