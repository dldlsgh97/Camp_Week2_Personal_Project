namespace Camp_Week2_Personal_Project
{
    internal class Program
    {
        static Inventory playerInventory;
        static List<Item> equippedItems = new List<Item>();

        static PlayerState playerState;
        static string PlayerName = "";
        static string PlayerClass = "전사";


        static void Main(string[] args)
        {
            equippedItems.Clear();
            Console.Write("모험가님의 이름을 알려주세요! :");
            PlayerName = Console.ReadLine();
            Console.WriteLine($"{PlayerName} 님 이시군요!");
            playerState = new PlayerState(1, PlayerName, PlayerClass, 10, 5, 100, 1500);

            playerInventory = new Inventory(playerState);

            Item testItem1 = new Item("무쇠갑옷",5,ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.");
            Item testItem2 = new Item("스파르타의 창", 7, ItemType.Wepon, "스파르타의 전사들이 사용했다는 전설의 창입니다.");
            Item testItem3 = new Item("낡은 검", 2, ItemType.Wepon, "쉽게 볼 수 있는 낡은 검 입니다.");
            playerInventory.AddItem(testItem1);
            playerInventory.AddItem(testItem2);
            playerInventory.AddItem(testItem3);

            

            while (true)
            {
                StartGame();
            }

        }

        static void StartGame()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("반갑습니다 모험자님!");
            Console.WriteLine();
            Console.WriteLine("이제부터 무엇을 할까요?");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 상태 확인");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 이동");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("원하는 행동을 입력하세요 : ");
            int inputNum = int.Parse(Console.ReadLine());
            if (inputNum == 1)
            {
                //플레이어 상태창으로 이동
                switch (playerState.DisplayPlayerState())
                {
                    case 0:
                        StartGame();
                        break;
                }

            }
            else if (inputNum == 2)
            {
                switch (playerInventory.DesplayInventory())
                {
                    case 1:
                        //장착관리 페이지로 이동
                        EquipManage();
                        break;
                    case 2:
                        StartGame();
                        break;
                }
            }
            else if (inputNum == 3)
            {
                //플레이어가 이동할수 있는 선택지 선택창으로 이동
                Move();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다!");
            }
        }



        static void Move()
        {
            Shop shop = new Shop(playerState);
            int input;
            Console.Clear();
            Console.WriteLine("현재 모험가님이 이동할 수 있는곳은 아래의 장소입니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상점");
            Console.WriteLine("2. 사냥 필드");
            Console.WriteLine();
            Console.WriteLine("원하는 장소를 입력해주세요");
            input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                Console.WriteLine("상점 이동");
                shop.DisplayProducts();
            }
            else if (input == 2)
            {
                Console.WriteLine("사냥 필드 이동");
            }
            else { Console.WriteLine("잘못된 입력입니다"); }
        }

        static void EquipManage()
        {
            InventoryRefesh();
            
            int input = int.Parse(Console.ReadLine());
            if(input == 0)
            {
                return;
            }

            int index = input - 1;
            if(index < 0 || index >= playerInventory.items.Count)
            {
                Console.WriteLine("잘못된 입력입니다");
                Thread.Sleep(1000);
                EquipManage();
            }
            
            Item selectItem = playerInventory.items[index];

            bool isEquipped = playerState.IsEquipped(selectItem);

            if(!isEquipped)
            {
                playerState.EquipItem(selectItem);                
            }
            else
            {
                playerState.UnEquipItem(selectItem);
            }
            EquipManage();


        }

        static void InventoryRefesh()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("장착 관리");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                if (playerState.GetEquippedItems().Contains(playerInventory.items[i]))
                {
                    Console.WriteLine($"[E] {i + 1}. {playerInventory.items[i].DisplayItem()}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"{i + 1}. {playerInventory.items[i].DisplayItem()}");
                    Console.WriteLine();
                }

                
            }
            Console.WriteLine();
            Console.WriteLine("장착할 아이템을 선택하세요 :");
            Console.WriteLine("0: 나가기");

        }

    }
    public class PlayerState
    {
        private List<Item> equippedItems;

        public int Level { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int MaxHp { get; set; }
        public int Gold { get; set; }


        public PlayerState(int level, string name, string className, int attack, int defense, int maxHp, int gold)
        {
            Level = level;
            Name = name;
            Class = className;
            Attack = attack;
            Defense = defense;
            MaxHp = maxHp;
            Gold = gold;
            equippedItems = new List<Item>();
        }
        public int DisplayPlayerState()
        {
            int input;
            RefreshPlayerState();
            input = int.Parse(Console.ReadLine());
            return input;
        }

        public void RefreshPlayerState()
        {
            int weponAttack = 0;
            int weponDefense = 0;

            foreach(var item in equippedItems)
            {
                if(item.Type == ItemType.Wepon)
                {
                    weponAttack += item.Stat;
                }
                else if(item.Type == ItemType.Armor)
                {
                    weponDefense += item.Stat;
                }
            }
            if(equippedItems.Count == 0)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("상태창");
                Console.WriteLine();
                Console.WriteLine($"LV : {Level}");
                Console.WriteLine($"{Name} ({Class})");
                Console.WriteLine($"공격력 : {Attack}");
                Console.WriteLine($"방어력 : {Defense}");
                Console.WriteLine($"체  력 : {MaxHp}");
                Console.WriteLine($" Gold  : {Gold} G");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
            }
            else
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("상태창");
                Console.WriteLine();
                Console.WriteLine($"LV : {Level}");
                Console.WriteLine($"{Name} ({Class})");
                Console.WriteLine($"공격력 : {Attack+ weponAttack} (+{weponAttack})");
                Console.WriteLine($"방어력 : {Defense + weponDefense} (+{weponDefense})");
                Console.WriteLine($"체  력 : {MaxHp}");
                Console.WriteLine($" Gold  : {Gold} G");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");

            }
            
            
        }

        public void EquipItem(Item item)
        {
            equippedItems.Add(item);
        }

        public void UnEquipItem(Item item)
        {
            equippedItems.Remove(item);
        }
        public bool IsEquipped(Item item)
        {
            return equippedItems.Contains(item);
        }
        public List<Item> GetEquippedItems()
        {
            return equippedItems;
        }
    }

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
            //아이템 삭제
            //판매 OR 삭제
        }

        public int DesplayInventory()
        {
            
            int input;
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

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            input = int.Parse(Console.ReadLine());

            return input;
        }
    }

    public enum ItemType
    {
        Armor,
        Wepon
    }

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

    public class Shop
    {
        PlayerState playerState;
        List<Item> product; 
        public Shop(PlayerState playerState)
        {
            this.playerState = playerState;
            product = new List<Item>();
        }

        public void AddProduct(Item item, int price)
        {
            product.Add(new ShopProduct(item,price));
        }

        public void DisplayProducts()
        {
            int input;
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("상점");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{playerState.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            
            Console.WriteLine("목록 출력");
            Console.WriteLine("목록 출력");
            Console.WriteLine("목록 출력");
            Console.WriteLine("목록 출력");
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하는 행동을 입력해주세요");
            input = int.Parse(Console.ReadLine());
            if(input == 1)
            {
                //구매 로직
            }
            else if(input == 2)
            {
                //판매 로직
            }
            else if(input == 0)
            {
                //이전 화면으로 이동
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
            }

        }
    }

    public class ShopProduct : Item
    {
        public int Price;
        public ShopProduct(Item item, int price) : base(item.Name, item.Stat, item.Type, item.Description)
        {
            Price = price;
        }

        public string ProductsName()
        {
            return $"{Name} - 가격 : {Price}G";
        } 
    }
}
