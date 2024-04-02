namespace Auction.DataAccess.Postgres.Entities
{
    public class BidEntity
    {
        public Guid Id { get; set; }
        public Guid LotId { get; set; }
        public LotEntity Lot { get; set; } 
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public decimal Bid {  get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
