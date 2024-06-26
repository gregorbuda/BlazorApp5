﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp5.Shared
{
    public class AzureConfiguration
    {
        public VideoIndexerConfiguration VideoIndexerConfiguration { get; set; }
    }

    public class VideoIndexerConfiguration
    {
        public string BaseAPIUrl { get; set; }

        public string SubscriptionKey { get; set; }
        public string Location { get; set; }
        public string AccountId { get; set; }
    }
}
