using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MultiPlatform.Domain.Code;

namespace MultiPlatform.Domain.Services
{
    public class ServiceBroker
    {
        public string BaseUrl { get; set; }

        private HttpClient client = new HttpClient();

        /*Create your own requests*/

        /**/

        private async Task<T> DoHttpGet<T>(StringBuilder Url)
        {
            HttpResponseMessage response = await client.GetAsync(Url.ToString());
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);
        }

        private async Task<T> DoHttpDelete<T>(StringBuilder Url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, new Uri(Url.ToString(), UriKind.RelativeOrAbsolute));
            var result = await client.SendAsync(request);
            var content = await result.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);;

        }

        private async Task<T> DoHttpPut<T>(StringBuilder Url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, new Uri(Url.ToString(), UriKind.RelativeOrAbsolute));
            var result = await client.SendAsync(request);
            var content = await result.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content); ;

        }


        private async Task<T> DoHttpPost<T>(StringBuilder Url, Object data)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(Url.ToString(), UriKind.RelativeOrAbsolute));
            request.Content = new StreamContent(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data.ToJson())));
            var result = await client.SendAsync(request);
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content); ;

        }

    }
}
