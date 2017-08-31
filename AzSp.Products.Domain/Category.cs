using System;

namespace AzSp.Products.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Byte[] Picture { get; set; }

    }
}