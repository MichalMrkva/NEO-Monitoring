using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace NEOMonitoring.API
{
    public class NEOMAPI
    {
        public static NearEarthObjects GetNEO(DateTime fromDate, DateTime toDate)
        { 
            using(HttpClient client = new HttpClient())
            {
                Uri endpoint = new Uri($"https://api.nasa.gov/neo/rest/v1/feed?start_date={fromDate.ToString("yyyy-MM-dd")}&end_date={toDate.ToString("yyyy-MM-dd")}&api_key=0KEz571aEJXT5Grv2TQhLvcpx9WkzsMmRLgzlS8i");
                var result = client.GetAsync(endpoint).Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var json = result.Content.ReadAsStringAsync().Result;
                    NearEarthObjects stations = JsonConvert.DeserializeObject<NearEarthObjects>(json);
                    return stations;
                }
                return null;
            }
        }
    }
}
