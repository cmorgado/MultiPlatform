using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using MultiPlatform.Domain.Code;
using System.Threading.Tasks;

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
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(Uri.EscapeUriString(Url.ToString()), UriKind.RelativeOrAbsolute));

                response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(content);
                    r.DATA = content.FromJson<T>();
                }
                else
                {
                    r.Error.HasError = true;
                    r.Error.Message = string.Format("http error: {0}", response.StatusCode.ToString());
                    r.Error.ErrorCode = response.StatusCode.ToString();
                }
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


        /// <summary>
        /// Post JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<Models.REST.RESTResult<T>> DoHttpPostJson<T>(StringBuilder Url, Object data)
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

        /// <summary>
        /// POST FORM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<Models.REST.RESTResult<T>> DoHttpPostForm<T>(StringBuilder Url, List<KeyValuePair<string, string>> data)
        {
            var r = new Models.REST.RESTResult<T>();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {

                response = await client.PostAsync(Url.ToString(), new FormUrlEncodedContent(data));
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    r.Error = new Models.REST.Error { HasError = false };
                    r.DATA = content.FromJson<T>();
                }
                else
                {
                    r.Error.HasError = true;
                    r.Error.Message = string.Format("http error: {0}", response.StatusCode.ToString());
                    r.Error.ErrorCode = response.StatusCode.ToString();
                }
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
