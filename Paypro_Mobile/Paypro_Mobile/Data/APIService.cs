using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Paypro_Mobile.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paypro_Mobile.Data
{
    class APIService
    {
        private static string APIURL = "https://paypro.uz/mAPI/";
        private static string CCAPIURL = "https://paypro.uz/CCAPI";

        public static decimal GetCurrencyRate()
        {
            try
            {
                var client = new RestClient(APIURL);

                var request = new RestRequest("GetSettings/", Method.POST);
                var response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return 0;

                var obj = JObject.Parse(response.Content);

                decimal currencyRate = Convert.ToDecimal(obj["currencyRate"]);
                return currencyRate;
            }
            catch
            {
                return 0;
            }
        }

        public static int GetMinMoney()
        {
            try
            {
                var client = new RestClient(APIURL);

                var request = new RestRequest("GetSettings/", Method.POST);
                var response = client.Execute(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return 0;
                var obj = JObject.Parse(response.Content);

                int minMoney = Convert.ToInt32(obj["minMoney"]);
                return minMoney;
            }
            catch
            {
                return 0;
            }
        }

        public static UpdateProfileValidation UpdateProfile(string email, string phoneNumber)
        {
            try
            {
                var client = new RestClient(APIURL);

                var request = new RestRequest("UpdateProfile/", Method.POST);
                request.AddParameter("username", StaticVariables.loggedUser.Username);
                request.AddParameter("passwordSha1", StaticVariables.loggedUser.PasswordSha1);
                request.AddParameter("email", email);
                request.AddParameter("phoneNumber", phoneNumber);
                var response = client.Execute(request);
                string err = response.Content;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return new UpdateProfileValidation { Information = err, Successful = false };
                }
                return new UpdateProfileValidation { Information = "Success", Successful = true };
            }
            catch
            {
                return new UpdateProfileValidation { Information = "Error", Successful = false };
            }
        }

        public static List<ISSProviders> GetISSProviders()
        {
            try
            {
                var client = new RestClient(APIURL);

                var request = new RestRequest("GetProviders/", Method.POST);
                var response = client.Execute(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;
                var data  = JsonConvert.DeserializeObject<List<ISSProviders>>(response.Content);
                return data;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }


        public static List<City> GetCities(int? operationID)
        {
            
            try
            {
                var client = new RestClient(APIURL);

                var request = new RestRequest("CityCounty/", Method.POST);
                if(operationID.HasValue)
                    request.AddParameter("operationID", operationID);
                request.AddParameter("tip", "getCity");

                var response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                var data = JsonConvert.DeserializeObject<List<City>>(response.Content);
                return data;
            }
            catch
            {
                return null;
            }
        }

        public static List<County> GetCounties(int? operationID, int cityID)
        {

            try
            {
                var client = new RestClient(APIURL);

                var request = new RestRequest("CityCounty/", Method.POST);
                if (operationID.HasValue)
                    request.AddParameter("operationID", operationID);

                request.AddParameter("tip", "getCounty");
                request.AddParameter("cityID", cityID);

                var response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                var data = JsonConvert.DeserializeObject<List<County>>(response.Content);
                return data;
            }
            catch
            {
                return null;
            }
        }


        public static bool  Login(User user)
        {

            try
            {
                var client = new RestClient(APIURL);

                var request = new RestRequest("Login/", Method.POST);

                request.AddParameter("username", user.Username);
                request.AddParameter("passwordsha1", user.PasswordSha1);

                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    StaticVariables.loggedUser = JsonConvert.DeserializeObject<User>(response.Content);
                    StaticVariables.loggedUser.PasswordSha1 = user.PasswordSha1;
                    StaticVariables.isLogged = true;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static List<PastOrders> GetPastOrders()
        {
            try
            {
                var client = new RestClient(APIURL);

                var request = new RestRequest("GetPastOrders/", Method.POST);
                request.AddParameter("username", StaticVariables.loggedUser.Username.ToLower());
                request.AddParameter("passwordsha1", StaticVariables.loggedUser.PasswordSha1);

                var response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                var data = JsonConvert.DeserializeObject<List<PastOrders>>(response.Content);
                
                return data.OrderByDescending(x=>x.datetime).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }


        public static CreditCardInfoModel GetCreditCardInfo(string cardNumber)
        {
            try
            {
                var client = new RestClient(CCAPIURL);

                var request = new RestRequest("getInfo/", Method.POST);
                request.AddParameter("cardNumber", cardNumber);

                var response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                var data = JsonConvert.DeserializeObject<CreditCardInfoModel>(response.Content);

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }



    }
}
