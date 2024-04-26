using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

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
            Console.WriteLine("4. 휴식");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("원하는 행동을 입력하세요 : ");
            
            string userInput = Console.ReadLine();
            int inputNum;
            if(int.TryParse(userInput, out inputNum))
            {
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
                else if (inputNum == 4)
                {
                    StartRest();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다!");
                }
            }

            
        }

        static void StartRest()
        {
            Console.Clear();
            Console.WriteLine($"500G 를 내면 체력을 회복할 수 있습니다 (보유 골드 : {playerState.Gold}G)");
            Console.WriteLine();
            Console.WriteLine("1. 휴식");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하는 행동을 입력해 주세요");
            string userInput = Console.ReadLine();
            int input;
            if( int.TryParse(userInput, out input))
            {
                if (input == 1)
                {
                    if (playerState.Gold < 500)
                    {
                        Console.WriteLine("보유금이 부족해서 휴식을 취하지 못했습니다.");
                        Thread.Sleep(1000);
                        return;
                    }
                    else
                    {
                        Rest();
                    }

                }
                else if (input == 0)
                {
                    StartGame();
                }

                else
                {
                    Console.WriteLine("잘못된 접근입니다.");
                }
            }
            

        }
        static void Rest()
        {
            playerState.Hp = playerState.MaxHp;
            playerState.Gold -= 500;
            Console.WriteLine("휴식을 취해서 HP가 회복되었습니다.");
            Thread.Sleep(1000);
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
            string userInput = Console.ReadLine();
            if( int.TryParse(userInput, out input))
            {
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
        }

        static void EquipManage()
        {
            InventoryRefesh();
            string userInput = Console.ReadLine();
            int input;
            int index = 0;
            if(int.TryParse(userInput, out input))
            {
                if (input == 0)
                {
                    return;
                }

                index = input - 1;
                if (index < 0 || index >= playerInventory.items.Count)
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(1000);
                    EquipManage();
                }
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

    public enum ItemType
    {
        Armor,
        Wepon
    }
}
