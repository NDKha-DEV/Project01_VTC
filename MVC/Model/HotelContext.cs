// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// namespace Model
// {
//     public class HotelContext : DbContext
//     {
//         public DbSet<Room> Rooms { get; set; }
//         public DbSet<Customer> Customers { get; set; }
//         public DbSet<Bill> Bills { get; set; }
//         public DbSet<Staff> Staffs { get; set;}
//         public DbSet<Booking> Bookings { get; set; }
//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             var serverVersion = new MySqlServerVersion(new Version(8, 0, 25)); 
//             optionsBuilder.UseMySql("Server=localhost;Database=your_database_name;User=root;Password=your_password", serverVersion);
//         }
//     }
// }