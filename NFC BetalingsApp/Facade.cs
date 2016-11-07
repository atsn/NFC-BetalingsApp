using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tilbudsapp;


namespace NFC_BetalingsApp
{
    class Facade
    {

        private const string ServerUrl = "https://fredagsbarchip.azurewebsites.net";
        //  private const string ServerUrl = "http://localhost:4722/";
        private const string ApiBaseUrl = "/api/";
        private static string token;

        public static async Task<IEnumerable<T>> GetListAsync<T>(T obj) where T : IWebUri
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.GetAsync(ApiBaseUrl + obj.ResourceUri);
                    if (!response.IsSuccessStatusCode) // Hvis der er fejl, så smid Exception, ellers fortsæt
                        throw new HttpErrorException("HTTP Fejl\n" + obj.VerboseName + ": " + response.ReasonPhrase);
                    var listOfObjects = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                    return listOfObjects;
                }
                catch (Exception ex)
                {
                    throw new ServerErrorException("Server Fejl\n" + ex.Message);
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(T obj, string id) where T : IWebUri, new()
        {
            T result = new T();
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.GetAsync(ApiBaseUrl + result.ResourceUri + "/" + id);
                    if (!response.IsSuccessStatusCode) // Hvis der er fejl, så smid Exception, ellers fortsæt
                        throw new HttpErrorException("HTTP Fejl\n" + obj.VerboseName + ": " + response.ReasonPhrase);
                    result = response.Content.ReadAsAsync<T>().Result;
                    return result;
                }
                catch (Exception ex)
                {
                    throw new ServerErrorException("Server Fejl\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="statueId"></param>
        /// <returns></returns>


        /// <summary>
        /// Sender et objekt til webservicen, serialiseret som JSON
        /// </summary>
        /// <typeparam name="T">Objekt Type</typeparam>
        /// <param name="obj">Objekt som skal sendes</param>
        /// <returns></returns>
        public static async Task<string> PostAsync<T>(T obj) where T : IWebUri
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.PostAsJsonAsync(ApiBaseUrl + obj.ResourceUri, obj);
                    if (!response.IsSuccessStatusCode) // Hvis der er fejl, så smid Exception, ellers fortsæt
                        throw new HttpErrorException("HTTP Fejl\n" + obj.VerboseName + ": " + response.ReasonPhrase);
                    return "Success: " + obj.VerboseName + " Oprettet";
                }
                catch (Exception ex)
                {
                    throw new ServerErrorException("Server Fejl\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<string> PutAsync<T>(T obj, string id) where T : IWebUri
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.PutAsJsonAsync(ApiBaseUrl + obj.ResourceUri + "/" + id, obj);
                    if (!response.IsSuccessStatusCode) // Hvis der er fejl, så smid Exception, ellers fortsæt
                        throw new HttpErrorException("HTTP Fejl\n" + obj.VerboseName + ": " + response.ReasonPhrase);
                    return "Success: " + obj.VerboseName + " Opdateret";
                }
                catch (Exception ex)
                {
                    throw new ServerErrorException("Server Fejl\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<string> DeleteAsync<T>(T obj, string id) where T : IWebUri
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.DeleteAsync(ApiBaseUrl + obj.ResourceUri + "/" + id);
                    if (!response.IsSuccessStatusCode) // Hvis der er fejl, så smid Exception, ellers fortsæt
                        throw new HttpErrorException("HTTP Fejl\n" + obj.VerboseName + ": " + response.ReasonPhrase);
                    return "Success: " + obj.VerboseName + " Slettet";
                }
                catch (Exception ex)
                {
                    throw new ServerErrorException("Server Fejl\n" + ex.Message);
                }
            }
        }

        public static async Task<string> DeleteWithIntAsync<T>(T obj, int id) where T : IWebUri
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.DeleteAsync(ApiBaseUrl + obj.ResourceUri + "/" + id);
                    if (!response.IsSuccessStatusCode) // Hvis der er fejl, så smid Exception, ellers fortsæt
                        throw new HttpErrorException("HTTP Fejl\n" + obj.VerboseName + ": " + response.ReasonPhrase);
                    return "Success: " + obj.VerboseName + " Slettet";
                }
                catch (Exception ex)
                {
                    throw new ServerErrorException("Server Fejl\n" + ex.Message);
                }
            }
        }

        public static async Task<string> login(string username, string password)
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                try
                {
                    var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ),
                            new KeyValuePair<string, string>( "userName", username ),
                            new KeyValuePair<string, string> ( "password", password )
                        };
                    var content = new FormUrlEncodedContent(pairs);
                    var response = await client.PostAsync("/token", content);
                    if (!response.IsSuccessStatusCode) // Hvis der er fejl, så smid Exception, ellers fortsæt
                        throw new HttpErrorException("HTTP Fejl\n" + ": " + response.ReasonPhrase);
                    string tokensplit = await response.Content.ReadAsStringAsync();

                    var splittettokenrewuest = tokensplit.Split('"');

                    token = splittettokenrewuest[3];
                    return "Success: " + "Login" + " Sucsess";
                }
                catch (Exception ex)
                {
                    throw new ServerErrorException("Server Fejl\n" + ex.Message);
                }
            }
        }


    }
}
