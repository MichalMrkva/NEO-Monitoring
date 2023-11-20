using Android.Widget;
using NEOMonitoring.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace NEOMonitoring.API
{
    public class NEOMAPI
    {
        private static HttpClient client = new HttpClient();
        private static string _APIKey;
        public static string APIKey
        { 
            get
            {
                if (_APIKey != null)
                    return _APIKey;
                else
                {
                    _APIKey = Preferences.Get("APIKey", null);
                    return _APIKey;
                }
            } 
            set
            { 
                _APIKey = value;
            }
        }
        public static void LastLoadSet(DateTime date, string json)
        {
            Preferences.Set("LastLoadContent", json);
            Preferences.Set("lastLoadDate", date);
        }
        public static string[] LastLoadGet()
        {
            return new string[] { Preferences.Get("LastLoadContent", null), Preferences.Get("lastLoadDate", null)};

        }
        public static NearEarthObjects GetNEO(DateTime fromDate, DateTime toDate)
        {
            Uri endpoint = new Uri($"https://api.nasa.gov/neo/rest/v1/feed?start_date={fromDate.ToString("yyyy-MM-dd")}&end_date={toDate.ToString("yyyy-MM-dd")}&api_key={APIKey}");
            var result = client.GetAsync(endpoint).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                LastLoadSet(fromDate, json);
                NearEarthObjects neo = JsonConvert.DeserializeObject<NearEarthObjects>(json);
                return neo;
            }
            if (result.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                Toast.MakeText(Android.App.Application.Context, "Server is down", ToastLength.Short).Show();
                return null;
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, "Unknown error", ToastLength.Short).Show();
                return null;
            }
            
        }
        public static List<CloseApproachData> GetCloseApproachData(string url)
        {
            url = url.Replace("http","https");
            Uri endpoint = new Uri(url);
            var result = client.GetAsync(endpoint).Result;
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                NearEarthObject neo = JsonConvert.DeserializeObject<NearEarthObject>(json);
                return neo.Close_approach_data;
            }
            return null;
        }
        public static HttpStatusCode KeyStatus(string key)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri endpoint = new Uri($"https://api.nasa.gov/neo/rest/v1/feed?start_date=2015-09-07&end_date=2015-09-08&api_key={key}");
                var result = client.GetAsync(endpoint).Result;
                return result.StatusCode;
            }
        }
    }
}
