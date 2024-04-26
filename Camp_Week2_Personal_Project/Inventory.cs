namespace Camp_Week2_Personal_Project
{
    public class Inventory
    {
        public List<Item> items;
        public PlayerState playerState;

        public Inventory(PlayerState playerState)
        {
            items = new List<Item>();
            this.playerState = playerState;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public int DesplayInventory()
        {

            int input;
            RefreshInventory();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            input = int.Parse(Console.ReadLine());

            return input;
        }
        public void RefreshInventory()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("인벤토리");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            if (items.Count == 0)
            {
                Console.WriteLine("인벤토리에 아무것도 없군요...");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (playerState.GetEquippedItems().Contains(items[i]))
                    {
                        Console.WriteLine($"- [E] {items[i].DisplayItem()}");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"- {items[i].DisplayItem()}");
                        Console.WriteLine();
                    }

                }
            }
        }
    }
}
