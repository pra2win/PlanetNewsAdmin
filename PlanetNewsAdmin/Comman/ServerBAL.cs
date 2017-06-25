﻿using PlanetNewsAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using PlanetNewsAdmin.Models.Poco;

namespace PlanetNewsAdmin.Comman
{
    public class ServerBAL
    {
        private static string newsAddUrl = ConfigurationManager.AppSettings["baseUrl"].ToString();
        private static Uri baseUri = new Uri(newsAddUrl);

        public static UserDetailResponseModel AuthenticateUser(LoginReq req)
        {
            UserDetailResponseModel resp = new UserDetailResponseModel();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(req));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync("api/Account/AuthenticateUser", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var value = response.Content.ReadAsStringAsync();

                    resp = JsonConvert.DeserializeObject<UserDetailResponseModel>(value.Result);
                }
                return resp;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static NewsDetail AddNews(AddNewsRequestModel request)
        {
            NewsDetail resp = new NewsDetail();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(request));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync("api/news/addNews", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var value = response.Content.ReadAsStringAsync();

                    resp = JsonConvert.DeserializeObject<NewsDetail>(value.Result);
                }
                return resp;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}