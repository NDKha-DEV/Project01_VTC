// using System;

// namespace Model
// {
//     public class Room
//     {
//         public int ID { get; set; }
//         public string Name { get; set;}
//         public string Type { get; set;}
//         public int capacity { get; set; }
//         public decimal PricePerNight { get; set;}
//         public string Status { get; set;}
//         public DateTime? CheckInDate { get; set; }
//         public DateTime? CheckOutDate { get; set; }
//         //public decimal PricePerNight { get; set; }
//         public decimal Caculate_TotalPrice()
//         {
//             if (CheckInDate.HasValue && CheckOutDate.HasValue)
//             {
//                 int numberOfNights = (int)(CheckOutDate.Value - CheckInDate.Value).TotalDays;
//                 return numberOfNights * PricePerNight;
//             }
//             else 
//             {
//                 return 0;
//             }
//         }
//         // Constructor
//         public Room()
//         {
            
//             Name = "";
//             Type = "";
//             Status = "";
//         }
//         // Override ToString method to provide custom string representation
//         // public override string ToString()
//         // {
//         //     return $"Room ID: {ID}, Room Name: {Name}, Room Type: {Type}, Room Price: {Price}, Room Status: {Status}";
//         // }
//     }
// }
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Model
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
