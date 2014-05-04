using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient_.HealthPService;

namespace WebClient_.Models.RequestDatabase
{
    public class AdModel
    {
        private static HealthPService.IHPService mService = new HPServiceClient();

        public static List<Dictionary<string, string>> SearchAd(string textSearch)
        {
            string result = mService.SearchAd(textSearch);
            List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar os anuncios que recebeu para uma lista, com um dicionario la dentro

            return resultList;
        }
    }
}