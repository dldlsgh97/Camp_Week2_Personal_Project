namespace Camp_Week2_Personal_Project
{
    public class ShopProduct : Item
    {
        public int Price;
        public Item Item;
        public bool IsBuy;
        public ShopProduct(Item item, int price, bool isBuy = false) : base(item.Name, item.Stat, item.Type, item.Description)
        {
            Item = item;
            Price = price;
            IsBuy = isBuy;
        }

        public string ProductsName()
        {
            if (IsBuy)
            {
                return $"{DisplayItem()} | 구매 완료";
            }
            else
            {
                return $"{DisplayItem()} - 가격 : {Price}G";
            }

        }

    }
}
