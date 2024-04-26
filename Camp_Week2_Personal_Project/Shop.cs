namespace Camp_Week2_Personal_Project
{
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
            string userInput = Console.ReadLine();
            int input;
            if (int.TryParse(userInput,out input))
            {
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
            string userInput = Console.ReadLine();
            if(int.TryParse(userInput,out input))
            {
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
            string userInput = Console.ReadLine();
            if(int.TryParse(userInput, out input))
            {
                SellItem(input, inventoryProducts);
            }
            

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
}
