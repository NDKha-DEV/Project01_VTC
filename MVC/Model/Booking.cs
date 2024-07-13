// using System;

// namespace Model
// {
//     public class Booking
//     {
//         public int ID { get; set; }
//         public int RoomID { get; set; }
//         public int CustomerID { get; set; }
//         public List<Room>? rooms { get; set; }
//         // Constructor
//         public Booking()
//         {
//             ID = BookingID;
//             RoomID = roomID;
//             CustomerID = customerID;
//             CheckIn_Date = ;
//             CheckOut_Date = ;
            
            
//         }
//         // Override ToString method to provide custom string representation
//         // public override string ToString()
//         // {
//         //     return $"Booking ID: {ID}, Room ID: {RoomID}, Customer ID: {CustomerID}, Check_in Date: {CheckIn_Date}, Check_out Date: {CheckOut_Date}, Total amount: {Total_amount}";
//         // }
//     }
// }
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