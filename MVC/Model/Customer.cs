// using System;

// namespace Model
// {
//     public class Customer
//     {
//         public int CustomerID { get; set; }
//         public string CustomerName { get; set; }
//         //public string GroupName { get; set; }
//         public string Address { get; set; }
//         public string PhoneNumber { get; set; }
//         public string Email { get; set; }
//         //public string NumberOfCustomers { get; set; }

//         // Constructor
//         public Customer()
//         {
//             //CustomerID = 0;
//             CustomerName = "";
//             //GroupName = "";
//             Address = "";
//             PhoneNumber = "";
//             Email = "";
//             //NumberOfCustomers = "";
//         }

//         // Override ToString method to provide custom string representation
//         // public override string ToString()
//         // {
//         //     return $"Customer ID: {CustomerID}, Customer Name: {CustomerName}, Group Name: {GroupName}, Address: {Address}, Phone Number: {PhoneNumber}, Email: {Email}, Number of Customers: {NumberOfCustomers}";
//         // }
//     }
// }
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    // Model Customer
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfCustomers { get; set; }
    }
}
