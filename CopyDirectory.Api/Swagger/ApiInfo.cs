using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopyDirectory.Api.Swagger
{
    public class ApiInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ApiVersion ApiVersion { get; set; }

        public ApiInfo(string title, string description, ApiVersion apiVersion)
        {
            Title = title;
            Description = description;
            ApiVersion = apiVersion;
        }
    }
}
