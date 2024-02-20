using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intershipJenkins.IntegrationTests
{
    public class BaseControllerIntergerationtest : IClassFixture<WebApplicationFactory<Program>>
    {   
        protected readonly HttpClient _httpClient;
        public BaseControllerIntergerationtest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }
    }
}
