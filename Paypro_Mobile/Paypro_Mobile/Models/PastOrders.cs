using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Paypro_Mobile.Models
{
   public class PastOrders
   {
        private string _customerAmountUZS;
        public DateTime datetime { get; set; }
        public string paymentType { get; set; }
        public string accountNumber { get; set; }

        public Color BGColor { get; set; }
        public string customerAmountUZS
        {

            get { return _customerAmountUZS.Contains(".") ? _customerAmountUZS.Substring(0,_customerAmountUZS.IndexOf(".")) + " UZS " : _customerAmountUZS + " UZS"; }
            set { _customerAmountUZS = value; } 
        }

        private bool _isPaid;
        public bool isPaid 
        {
            get { return _isPaid; }
            set {
                    _isPaid = value;
                    if(value)
                    {
                        BGColor = StaticVariables.PaidColor;
                    }
                    else
                    {
                        BGColor = StaticVariables.NotPaidColor;
                }
                        
                }
                
        }
    }
}
