﻿using App.Domain.Entities.Common;

namespace App.Domain.Entities
{
    public class Product : BaseEntity<long>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public int QuantityInStock { get; set; }
    }
}
