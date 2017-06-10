namespace CommodityController
{
    public class Commodity
    {
        public int Id;
        public int Cost;
        public int Revenue;
        public int SellPrice;

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