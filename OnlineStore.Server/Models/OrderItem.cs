﻿using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ItemId { get; set; }

        public int ItemsCount { get; set; }

        public double ItemPrice { get; set; }

        public Order? Order { get; set; }

        public Item? Item { get; set; }
    }
}
