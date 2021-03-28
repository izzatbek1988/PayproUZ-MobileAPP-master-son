using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paypro_Mobile.Models
{
    public class User
    {
        [JsonProperty("user_id")]
        public int UserID { get; set; }

        [JsonProperty("user_name")]
        public string Username { get; set; }

        [JsonProperty("user_pass")]
        public string PasswordSha1 { get; set; }

        [JsonProperty("user_mail")]
        public string EMail { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
