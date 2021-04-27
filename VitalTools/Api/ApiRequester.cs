using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VitalTools.Api
{
    public class ApiRequester
    {
        #region Properties
        
        private HttpClient _client; 

        #endregion

        #region Constructors

        public ApiRequester(string baseAdress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAdress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region CRUD Methods

        public TResult Get<TResult>(string uri)
        {
            using (HttpResponseMessage message = _client.GetAsync(uri).Result)
            {
                message.EnsureSuccessStatusCode();
                string json = message.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<TResult>(json);
            }
        }

        public void Add<TBody>(TBody body, string uri)
        {
            string json = JsonConvert.SerializeObject(body);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = _client.PostAsync(uri, content).Result;

            if (!message.IsSuccessStatusCode)
                throw new HttpRequestException();
        }

        public void Edit<TBody>(TBody body, string uri)
        {
            string json = JsonConvert.SerializeObject(body);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = _client.PutAsync(uri, content).Result;

            if (!message.IsSuccessStatusCode)
                throw new HttpRequestException();
        }

        public void Delete(int id, string uri)
        {
            HttpResponseMessage message = _client.DeleteAsync(uri + id).Result;
            message.EnsureSuccessStatusCode();
        } 
        
        #endregion
    }
}