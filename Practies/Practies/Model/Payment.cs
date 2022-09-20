using System;
using System.Collections.Generic;

namespace Practies.Model
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal DicountCoupon { get; set; }
        public int RemoteTransactionId { get; set; }
        public int PeyementMethod { get; set; }
        public int BookingId { get; set; }

        public virtual Booking Booking { get; set; } = null!;
    }
}
