using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp5.Shared.Models.AzureVideoIndexer.UploadVideo
{
    public class UploadVideoModel
    {
        public string Name { get; set; }

        public string Languaje { get; set; }

        public string VideoUrl { get; set; }

        public bool SendSucccesEmail { get; set; }
    }
}
