using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CACWeb.Models
{
    [Serializable]
    public class JsonResponse
    {
        public string status { get; set; }
        public string content { get; set; }
    }
}