using System;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GalgelegTUI
{
    public class RESTConnector
    {
        private string BaseUrl;
        private HttpClient Client;

        public RESTConnector()
        {
            /* Prepares a client with our baseurl for the REST service */
            BaseUrl = "http://ubuntu4.javabog.dk:4176/HangAnimalsREST/webresources/";
            //BaseUrl = "http://ubuntu4.javabog.dk:4176/HangAnimalsREST2/webresources/"; //Hvis vi nu skulle skifte addresse igen.
            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<T> RestPOST<T>(string urlParams, Dictionary<string, string> values)
        {
            HttpContent content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await Client.PostAsync(urlParams, content);

            try
            {
                return await ParseJson<T>(response);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<T> RestGET<T>(string urlParams)
        {

            /* Gets the response from the complete url */
            HttpResponseMessage response = await Client.GetAsync(urlParams);
            try
            {
                return await ParseJson<T>(response);
            } catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<T> RestPUT<T>(string urlParams, Dictionary<string, string> values)
        {
            HttpContent content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await Client.PutAsync(urlParams, content);

            try
            {
                return await ParseJson<T>(response);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async void LeaveRoom(string urlParams, Dictionary<string, string> values)
        {
            HttpContent content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await Client.PostAsync(urlParams, content);
        }

        private async Task<T> ParseJson<T>(HttpResponseMessage response)
        {
            /* Makes sure the response has to have a success status code, other wise throw exception */
            response.EnsureSuccessStatusCode();


            if (response.IsSuccessStatusCode)
            {
                /* Parses the response to string */
                var dataObjects = await response.Content.ReadAsStringAsync();


                /* Returns the new Task which */
                return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(dataObjects));
            }
            else
            {
                throw new Exception("Der skete en fejl da Json skulle parses.");
            }
        }
    }
}
