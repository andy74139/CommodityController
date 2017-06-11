namespace CommodityService
{
    public class Commodity
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Revenue { get; set; }
        public int SellPrice { get; set; }

        public Commodity()
        {
        }

        public Commodity(int id, int cost, int revenue, int sellPrice)
        {
            Id = id;
            Cost = cost;
            Revenue = revenue;
            SellPrice = sellPrice;
        }
    }
}