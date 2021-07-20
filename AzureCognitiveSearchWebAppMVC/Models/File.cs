using Azure.Search.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureCognitiveSearchWebAppMVC.Models
{
    public class File
    {
        [SearchableField]
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
