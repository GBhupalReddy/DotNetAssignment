﻿using System.ComponentModel.DataAnnotations;

namespace BookMyShow.ViewModel
{
    public  class PaymentVm
    {
       
       [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public int RemoteTransactionId { get; set; }
        [Required]
        public int PeyementMethod { get; set; }
        [Required]
        public int BookingId { get; set; }

       
    }
}
