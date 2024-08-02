using System;
using System.Collections.Generic;

namespace Model
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public string? RoomType { get; set; }
        public int Customer_id { get; set; }
        // public int Bill_id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

    }
}
