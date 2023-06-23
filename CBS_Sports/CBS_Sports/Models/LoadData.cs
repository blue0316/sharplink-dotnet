using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
using System.Security.Policy;

namespace CBS_Sports.Models
{
    public class LoadData
    {
        public static readonly HttpClient _httpClient = new HttpClient();

        public LoadData() 
        {
            _httpClient.BaseAddress = new Uri("https://api.cbssports.com/fantasy/players/list?version=3.0&SPORT=basketball&response_format=JSON");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
        }

        public static async Task onGet(IServiceProvider services)
        {
            string strresponse = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.cbssports.com/fantasy/players/list?version=3.0&SPORT=basketball&response_format=JSON");

                httpWebRequest.Method = "GET";

                using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var bufferSize = 2048;
                        var buffer = new char[bufferSize];
                        var result = new List<char>();
                        List<string> strList = new List<string>();

                        try
                        {
                            var readBytes = 0;
                            while ((readBytes = streamReader.Read(buffer, 0, 2048)) != 0)
                            {
                                var bufferTemp = new char[readBytes];
                                for (int i = 0; i < readBytes; i++)
                                {
                                    bufferTemp[i] = buffer[i];
                                    result.Add(buffer[i]);
                                }

                                string str = new string(bufferTemp);
                                strList.Add(str);
                                strresponse += str;

                                if (str.Contains("}}")) break;
                            }
                        }
                        catch (IOException ex)
                        {
                            if (!ex.Message.StartsWith("The response ended prematurely"))
                            {
                                throw;
                            }
                        }
                    }

                    if (httpResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(string.Format("Server error (HTTP {0}: {1}).", httpResponse.StatusCode, httpResponse.StatusDescription));
                    }

                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }

            PlayerContext _pContext = new PlayerContext()
        }

    }
}
