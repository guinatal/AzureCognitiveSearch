using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using AzureCognitiveSearchWebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCognitiveSearchWebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            string serviceName = Configuration["azure_cognitive:service_name"];
            string endpoint = $"https://{serviceName}.search.windows.net";
            string indexName = Configuration["azure_cognitive:index_name"];
            string apiKey = Configuration["azure_cognitive:api_key"];

            Uri serviceEndpoint = new(endpoint);
            AzureKeyCredential credential = new(apiKey);

            SearchClient searchClient = new(serviceEndpoint, indexName, credential);

            SearchOptions searchOptions = new();
            searchOptions.Select.Add("content");

            Response<SearchResults<File>> response = await searchClient.SearchAsync<File>("Broadway", searchOptions);

             return View(response.Value.GetResults());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
