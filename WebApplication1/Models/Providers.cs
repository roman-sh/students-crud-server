using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Providers
    {
        private string providersFilePath = System.Configuration.ConfigurationManager.AppSettings["ProvidersFilePath"];
        private IEnumerable<Provider> _providers;
        public Providers(string providersFilePath = null)
        {
            if (providersFilePath != null)
                this.providersFilePath = providersFilePath;

            _providers = JsonConvert.DeserializeObject<Provider[]>(File.ReadAllText(this.providersFilePath));
        }
        public IEnumerable<Provider> ProvidersList { get => _providers; }
    }
}