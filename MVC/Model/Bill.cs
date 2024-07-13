using System;

namespace Model
{
    public class Bill
    {
        public int BillId { get; set; }
        public int Customer_id { get; set; }
        public string? Description { get; set; }
        public string? Billstatus { get; set; } = "Pending";
        public DateTime Payment_date { get; set; } = DateTime.Now;
        public decimal Total_amount { get; set; }
    }
}