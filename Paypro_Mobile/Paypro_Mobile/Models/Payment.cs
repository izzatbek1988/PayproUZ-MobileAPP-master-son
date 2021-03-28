using System;
using System.Collections.Generic;
using System.Text;

namespace Paypro_Mobile.Models
{
    public class Payment
    {
        public int OpID { get; set; }
        public string AccountNumber { get; set; }
        public decimal Money { get; set; }
        public string Provider = String.Empty;
        public string Region = String.Empty;
        public string Type = String.Empty;
        public string cityID = String.Empty;
        public string countyID = String.Empty;

    }
}
