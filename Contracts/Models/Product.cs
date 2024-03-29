﻿using System.Collections.Generic;

namespace Contracts.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Details { get; set; }
        public List<Order> Orders { get; set; }
    }
}
