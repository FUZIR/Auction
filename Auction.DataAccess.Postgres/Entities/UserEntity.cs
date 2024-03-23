namespace Auction.DataAccess.Postgres.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty ;
        public string Nickname { get; set; } = string.Empty;

        public List<LotEntity> CreatedLots { get; set; } = new List<LotEntity>();
        public List<LotEntity> BoughtLots { get; set; } = new List<LotEntity>();
        public List<BidEntity> Bids { get; set; } = new List<BidEntity>();
    }
}
