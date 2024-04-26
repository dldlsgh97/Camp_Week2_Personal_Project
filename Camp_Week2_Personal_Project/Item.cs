namespace Camp_Week2_Personal_Project
{
    public class Item
    {
        public string Name;
        public string Description;
        public ItemType Type;
        public int Stat;

        public Item(string name, int stat, ItemType type, string description)
        {
            Name = name;
            Description = description;
            Type = type;
            Stat = stat;
        }

        public string DisplayItem()
        {
            string itemType;
            switch (Type)
            {
                case ItemType.Armor:
                    itemType = "방어력 +";
                    break;
                case ItemType.Wepon:
                    itemType = "공격력 +";
                    break;
                default:
                    itemType = "";
                    break;
            }
            return $"{Name} | {itemType}{Stat} | {Description}";
        }
    }
}
