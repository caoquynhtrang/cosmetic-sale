using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpAssignment.Models
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Usage { get; set; }
        public int CateID { get; set; }
        public int StatusID { get; set; }
        public string ImageLink { get; set; }

        public ProductDTO(string name, int quantity, double price, string usage,
            int cateID, int statusID, string imageLink)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Usage = usage;
            CateID = cateID;
            StatusID = statusID;
            ImageLink = imageLink;
        }

        public ProductDTO(int iD, string name, int quantity, double price, string usage,
            int cateID, int statusID, string imageLink)
        {
            ID = iD;
            Name = name;
            Quantity = quantity;
            Price = price;
            Usage = usage;
            CateID = cateID;
            StatusID = statusID;
            ImageLink = imageLink;
        }
    }
}