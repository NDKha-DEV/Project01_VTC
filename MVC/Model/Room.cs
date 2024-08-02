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
