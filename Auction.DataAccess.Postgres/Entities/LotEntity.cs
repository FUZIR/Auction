﻿using System.Text.Json.Serialization;
using Auction.DataAccess.Postgres.Enums;

namespace Auction.DataAccess.Postgres.Entities
{
    public class LotEntity
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StartingPrice { get; set; }
        public decimal? CurrentPrice { get; set; } = null;
        public decimal? BuyPrice { get; set; } = null;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public Guid CreatorId { get; set; }
        public UserEntity Creator { get; set; }
        public Guid? BuyerId { get; set; } = null;
        public UserEntity Buyer { get; set; }
    }
}
