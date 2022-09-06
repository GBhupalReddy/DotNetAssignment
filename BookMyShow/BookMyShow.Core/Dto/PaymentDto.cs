namespace BookMyShow.Core.Dto
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal DicountCoupon { get; set; }
        public int RemoteTransactionId { get; set; }
        public int PeyementMethod { get; set; }
        public int BookingId { get; set; }
    }
}
