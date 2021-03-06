﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipWeen.Common.Entities
{
   
    public class OrderOptions
    {
        public long Amount { get; set; }

        public string SourceCode { get; set; }


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

    public class TransactionResult
    {
        public long OrderId { get; set; }

        public string TransactionId { get; set; }
        
    }

}
