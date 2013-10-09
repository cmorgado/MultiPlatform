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

        private async Task<Models.REST.RESTResult<T>> DoHttpGet<T>(StringBuilder Url)
        {
            var r = new Models.REST.RESTResult<T>();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync(Url.ToString());
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                r.Error = new Models.REST.Error { HasError = false };
                r.DATA = content.FromJson<T>();
            }
            catch (HttpRequestException hex)
            {
                r.Error.HasError = true;
                r.Error.Message = string.Format("http error: {0}", hex.Message);
                r.Error.ErrorCode = response.StatusCode.ToString();

            }
            catch (Exception ex)
            {
                r.Error.HasError = true;
                r.Error.Message = string.Format("http error: {0}", ex.Message);

            }
            return r;
        }

        private async Task<Models.REST.RESTResult<T>> DoHttpDelete<T>(StringBuilder Url)
        {
            var r = new Models.REST.RESTResult<T>();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, new Uri(Url.ToString(), UriKind.RelativeOrAbsolute));
                response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                r.Error = new Models.REST.Error { HasError = false };
                r.DATA = content.FromJson<T>();
            }
            catch (HttpRequestException hex)
            {
                r.Error.HasError = true;
                r.Error.Message = string.Format("http error: {0}", hex.Message);
                r.Error.ErrorCode = response.StatusCode.ToString();


            }
            catch (Exception ex)
            {
                r.Error.HasError = true;
                r.Error.Message = string.Format("http error: {0}", ex.Message);

            }
            return r;

        }

        private async Task<Models.REST.RESTResult<T>> DoHttpPut<T>(StringBuilder Url)
        {
            var r = new Models.REST.RESTResult<T>();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, new Uri(Url.ToString(), UriKind.RelativeOrAbsolute));
                response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                r.Error = new Models.REST.Error { HasError = false };
                r.DATA = content.FromJson<T>();
            }
            catch (HttpRequestException hex)
            {
                r.Error.HasError = true;
                r.Error.Message = string.Format("http error: {0}", hex.Message);
                r.Error.ErrorCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                r.Error.HasError = true;
                r.Error.Message = string.Format("http error: {0}", ex.Message);

            }
            return r;

        }


        private async Task<Models.REST.RESTResult<T>> DoHttpPost<T>(StringBuilder Url, Object data)
        {
            var r = new Models.REST.RESTResult<T>();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(Url.ToString(), UriKind.RelativeOrAbsolute));
                request.Content = new StreamContent(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data.ToJson())));
                response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                r.Error = new Models.REST.Error { HasError = false };
                r.DATA = content.FromJson<T>();
            }
            catch (HttpRequestException hex)
            {
                r.Error.HasError = true;
                r.Error.Message = string.Format("http error: {0}", hex.Message);
                r.Error.ErrorCode = response.StatusCode.ToString();

            }
            catch (Exception ex)
            {
                r.Error.HasError = true;
                r.Error.Message = string.Format("http error: {0}", ex.Message);

            }
            return r;

        }

    }
}
