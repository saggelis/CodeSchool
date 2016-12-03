using FlipWeen.MVC.Api;
using System;


namespace FlipWeen.MVC.Models
{
   
    public class OrderOptions : ApiModel
    {
        public long Amount { get; set; }
        
    }

    // class that contains the order results
    public class OrderResult
    {
        public long? OrderCode { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public DateTime TimeStamp { get; set; }
        public string RedirectUrl { get; set; }

    }

    public class TransactionResult:ApiModel
    {
        public long OrderId { get; set; }

        public string TransactionId { get; set; }

    }

}
