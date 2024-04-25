using System.Collections.Generic;

namespace Camp_Week2_Personal_Project
{
    internal class Program
    {
        static Inventory playerInventory;
        static List<Item> equippedItems = new List<Item>();
        static List<ShopProduct> products = new List<ShopProduct>();
        static PlayerState playerState;
        static string PlayerName = "";
        static string PlayerClass = "전사";
        static Shop shop;
        static Dungeon dungeon;


        static void Main(string[] args)
        {
            equippedItems.Clear();
            products.Clear();
            Console.Write("모험가님의 이름을 알려주세요! :");
            PlayerName = Console.ReadLine();
            Console.WriteLine($"{PlayerName} 님 이시군요!");
            playerState = new PlayerState(1, PlayerName, PlayerClass, 10, 5, 100, 1500);

            playerInventory = new Inventory(playerState);

            Item testItem1 = new Item("무쇠갑옷", 5, ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.");
            Item testItem2 = new Item("스파르타의 창", 7, ItemType.Wepon, "스파르타의 전사들이 사용했다는 전설의 창입니다.");
            Item testItem3 = new Item("낡은 검", 2, ItemType.Wepon, "쉽게 볼 수 있는 낡은 검 입니다.");
            playerInventory.AddItem(testItem1);
            playerInventory.AddItem(testItem2);
            playerInventory.AddItem(testItem3);

            shop = new Shop(playerState, playerInventory);
            shop.AddProduct(new ShopProduct(new Item("수련자 갑옷", 5, ItemType.Armor, "수련에 도움을 주는 갑옷입니다."), 1000));
            shop.AddProduct(new ShopProduct(new Item("무쇠갑옷", 9, ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다."), 1500));
            shop.AddProduct(new ShopProduct(new Item("스파르타의 갑옷", 15, ItemType.Armor, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."), 2000));
            shop.AddProduct(new ShopProduct(new Item("낡은 검", 2, ItemType.Wepon, "쉽게 볼 수 있는 낡은 검 입니다."), 600));
            shop.AddProduct(new ShopProduct(new Item("청동 도끼", 5, ItemType.Wepon, "어디선가 사용됐던거 같은 도끼입니다."), 1500));
            shop.AddProduct(new ShopProduct(new Item("스파르타의 창", 7, ItemType.Wepon, "스파르타의 전사들이 사용했다는 전설의 창입니다."), 2000));

            dungeon = new Dungeon(playerState);
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



        static public void Move()
        {

            int input;
            Console.Clear();
            Console.WriteLine("현재 모험가님이 이동할 수 있는곳은 아래의 장소입니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상점");
            Console.WriteLine("2. 사냥 필드");
            Console.WriteLine();
            Console.WriteLine("원하는 장소를 입력해주세요");
            Console.WriteLine("나가기 : 0");
            input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                shop.DisplayProducts();
            }
            else if (input == 2)
            {
                dungeon.DungeonEntrance();

                int i = int.Parse(Console.ReadLine());
                if (i == 0)
                {
                    StartGame();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                }

            }
            else { Console.WriteLine("잘못된 입력입니다"); }
        }

        static void EquipManage()
        {
            InventoryRefesh();

            int input = int.Parse(Console.ReadLine());
            if (input == 0)
            {
                return;
            }

            int index = input - 1;
            if (index < 0 || index >= playerInventory.items.Count)
            {
                Console.WriteLine("잘못된 입력입니다");
                Thread.Sleep(1000);
                EquipManage();
            }

            Item selectItem = playerInventory.items[index];

            bool isEquipped = playerState.IsEquipped(selectItem);

            if (!isEquipped)
            {
                playerState.EquippedItemCheck(selectItem);
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

            foreach (var item in equippedItems)
            {
                if (item.Type == ItemType.Wepon)
                {
                    weponAttack += item.Stat;
                }
                else if (item.Type == ItemType.Armor)
                {
                    weponDefense += item.Stat;
                }
            }
            if (equippedItems.Count == 0)
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
                Console.WriteLine($"공격력 : {Attack + weponAttack} (+{weponAttack})");
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

        public void EquippedItemCheck(Item item)
        {
            ItemType type = item.Type;
            if(equippedItems.Count == 0)
            {
                EquipItem(item);
            }
            else
            {
                for(int i = 0; i < equippedItems.Count;i++)
                {
                    if (item.Type == equippedItems[i].Type)
                    {
                        UnEquipItem(equippedItems[i]);
                        EquipItem(item);
                    }
                    else
                    {
                        EquipItem(item);
                    }
                }
            }
            
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
        List<ShopProduct> products;
        PlayerState playerState;
        Inventory playerInventory;
        public Shop(PlayerState playerState, Inventory playerInventory)
        {
            this.playerState = playerState;
            products = new List<ShopProduct>();
            this.playerInventory = playerInventory;

        }

        public void AddProduct(ShopProduct product)
        {
            products.Add(product);
        }

        public void DisplayProducts()
        {

            int input;
            RefreshShopProducts();
            if (products.Count == 0)
            {
                Console.WriteLine("현재 판매하고있는 물품이 없습니다.");
            }
            else
            {
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine(products[i].ProductsName());
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하는 행동을 입력해주세요");
            input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                //구매 로직
                BuyProducts();
            }
            else if (input == 2)
            {
                //판매 로직
                SellProduct();
            }
            else if (input == 0)
            {
                //이전 화면으로 이동

            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
            }

        }

        void RefreshShopProducts()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("상점");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{playerState.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

        }
        void BuyProducts()
        {
            int input;
            RefreshShopProducts();
            if (products.Count == 0)
            {
                Console.WriteLine("현재 판매하고있는 물품이 없습니다.");
            }
            else
            {
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {products[i].ProductsName()}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("구매하고 싶은 아이템을 선택하세요 :");
            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 0:
                    break;
                case 1:
                    BuyItem(input);
                    BuyProducts();
                    break;
                case 2:
                    BuyItem(input);
                    BuyProducts();
                    break;
                case 3:
                    BuyItem(input);
                    BuyProducts();
                    break;
                case 4:
                    BuyItem(input);
                    BuyProducts();
                    break;
                case 5:
                    BuyItem(input);
                    BuyProducts();
                    break;
                case 6:
                    BuyItem(input);
                    BuyProducts();
                    break;

            }
        }

        void BuyItem(int i)
        {
            if (products[i - 1].IsBuy == true)
            {
                Console.WriteLine("이미 구매한 아이템 입니다.");
                Thread.Sleep(1000);
            }
            else
            {
                if ((playerState.Gold - products[i - 1].Price) < 0)
                {
                    Console.WriteLine("Gold 가 부족합니다.");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("구매를 완료했습니다.");
                    Thread.Sleep(1000);
                    playerState.Gold -= products[i - 1].Price;
                    //구매 완료표시
                    playerInventory.AddItem(products[i - 1]);
                    products[i - 1].IsBuy = true;
                }
            }

        }

        void SellProduct()
        {
            int input;

            List<ShopProduct> inventoryProducts = new List<ShopProduct>();
            RefreshShopProducts();

            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                ItemToShopProduct(playerInventory.items[i], inventoryProducts);
            }


            if (inventoryProducts.Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < inventoryProducts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {inventoryProducts[i].DisplayItem()} | {inventoryProducts[i].Price} ");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("판매하고 싶은 아이템을 선택하세요 :");
            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            input = int.Parse(Console.ReadLine());
            SellItem(input, inventoryProducts);

        }

        void ItemToShopProduct(Item item, List<ShopProduct> inventoryProducts)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (item.Name == products[i].Name)
                {
                    inventoryProducts.Add(new ShopProduct(item, products[i].Price));
                }
            }
        }

        void SellItem(int input, List<ShopProduct> inventoryProducts)
        {
            if (input == 0)
            {
                DisplayProducts();
                return;
            }
            else if (input > inventoryProducts.Count || input < 0)
            {
                Console.WriteLine("잘못된 입력입니다");
                SellProduct();
            }
            else
            {
                playerState.Gold += (int)(inventoryProducts[input - 1].Price * 0.85f);
                playerInventory.RemoveItem(inventoryProducts[input - 1].Item);
                ChangeItemState(inventoryProducts[input - 1]);
                inventoryProducts.RemoveAt(input - 1);
                
                SellProduct();
            }
        }

        void ChangeItemState(ShopProduct soldItem)
        {
            Thread.Sleep(1000);
            for (int i = 0; i < products.Count; i++)
            {
                if (soldItem.Name == products[i].Name)
                {
                    products[i].IsBuy = false;
                }
            }
        }
    }

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
    public class Dungeon
    {
        PlayerState playerState;
        int difficulty;
        int recommend;
        int reward;
        public Dungeon(PlayerState playerState)
        {
            this.playerState = playerState;
        }
        public void DungeonEntrance()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("던전 입장");
            Console.WriteLine();
            Console.WriteLine("난이도를 선택해 주세요");
            Console.WriteLine();
            Console.WriteLine("1. 쉬움 | 권장 방어력 : 5");
            Console.WriteLine("2. 일반 | 권장 방어력 : 11");
            Console.WriteLine("3. 어려움 | 권장 방어력 : 17");
            Console.WriteLine();
            Console.WriteLine("원하는 행동을 입력하세요(나가기 : 0)");
            int input = int.Parse(Console.ReadLine());
            if(input == 0)
            {
                Program.Move();
            }
            else if(input == 1)
            {
                EnterDungeon(playerState.Defense, 1);
            }
            else if (input == 2)
            {
                EnterDungeon(playerState.Defense, 2);
            }
            else if (input == 3)
            {
                EnterDungeon(playerState.Defense, 3);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(100);
                DungeonEntrance();
            }
        }
        public void EnterDungeon(int playerDefenseStat, int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    recommend = 5;
                    reward = 1000;
                    StartDungeon(playerDefenseStat, recommend, reward);
                    break;
                case 2:
                    recommend = 11;
                    reward = 1700;
                    StartDungeon(playerDefenseStat, recommend, reward);
                    break;
                case 3:
                    recommend = 17;
                    reward = 2000;
                    StartDungeon(playerDefenseStat, recommend, reward);
                    break;
            }




            
        }

        void StartDungeon(int playerDefenseStat,int recommend, int reward)
        {

            Console.Clear();
            Console.WriteLine("던전에 입장했습니다.");
            if (playerDefenseStat < recommend)
            {
                Console.Clear();
                Console.WriteLine("플레이어의 방어력이 권장 방어력보다 낮습니다.");

                if (new Random().Next(1, 101) <= 40)
                {
                    Console.WriteLine("던전 클리어 실패!");

                    int damage = new Random().Next(20, 36) / 2;
                    Console.WriteLine($"체력이 {damage}만큼 감소합니다.");
                    playerState.MaxHp -= damage;
                    Console.WriteLine($"플레이어의 현재 체력 : {playerState.MaxHp}");
                    Console.WriteLine("아쉽지만 방어력을 올려서 도전해 보세요!");
                    Console.WriteLine();
                    Console.WriteLine("돌아가기 : 0");
                    int input = int.Parse(Console.ReadLine());
                    if (input == 0) { DungeonEntrance(); }
                    else { Console.WriteLine("잘못된 접근입니다"); }
                }
                else
                {
                    Console.WriteLine("던전 클리어 성공!");
                    Console.WriteLine("던전의 권장 방어력보다 플레이어의 방어력이 낮아 보상을 획득하지 못했습니다");
                    Console.WriteLine("아쉽지만 방어력을 올려서 도전해 보세요!");
                    Console.WriteLine();
                    Console.WriteLine("돌아가기 : 0");
                    int input = int.Parse(Console.ReadLine());
                    if (input == 0) { DungeonEntrance(); }
                    else { Console.WriteLine("잘못된 접근입니다"); }
                }
            }
            else
            {
                Console.WriteLine("던전 클리어 성공!");
                // 클리어 보상 계산
                int additionalReward = CalculateAdditionalReward(playerDefenseStat);
                int totalReward = reward + additionalReward;
                Console.WriteLine($"기본 보상: {reward} G");
                Console.WriteLine($"추가 보상: {additionalReward} G");
                Console.WriteLine($"총 보상: {totalReward} G");
                playerState.Gold += totalReward;
                Console.WriteLine();
                Console.WriteLine($"현재 보유하고있는 Gold : {playerState.Gold}G");
                Console.WriteLine();
                Console.WriteLine("돌아가기 : 0");
                int input = int.Parse( Console.ReadLine() );
                if (input == 0) { DungeonEntrance(); }
                else { Console.WriteLine("잘못된 접근입니다"); }

            }
        }

        int CalculateAdditionalReward(int playerDefense)
        {
            int minAdditionalReward = (int)(reward * 0.1);
            int maxAdditionalReward = (int)(reward * 0.2);
            int additionalReward = new Random().Next(minAdditionalReward, maxAdditionalReward + 1);

            // 플레이어 방어력에 따라 추가 보상 조절
            int diff = playerDefense - recommend;
            if (diff > 0)
            {
                // 플레이어 방어력이 권장 수준보다 높을 때
                additionalReward += (int)(additionalReward * (diff * 0.1));
            }
            else if (diff < 0)
            {
                // 플레이어 방어력이 권장 수준보다 낮을 때
                additionalReward -= (int)(additionalReward * (Math.Abs(diff) * 0.1));
            }

            return additionalReward;
        }
        }
}
