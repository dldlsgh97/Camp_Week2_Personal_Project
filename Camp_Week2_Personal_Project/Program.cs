namespace Camp_Week2_Personal_Project
{
    internal class Program
    {
        static Inventory playerInventory = new Inventory(10);

        static PlayerState playerState;
        static string PlayerName = "";
        static string PlayerClass = "";


        static void Main(string[] args)
        {
            Console.Write("모험가님의 이름을 알려주세요! :");
            PlayerName = Console.ReadLine();
            Console.WriteLine($"{PlayerName} 님 이시군요! 그럼 직업을 알려주세요!");
            PlayerClass = Console.ReadLine();
            playerState = new PlayerState(1, PlayerName, PlayerClass, 10, 5, 100, 0);
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
            int inputNum =int.Parse(Console.ReadLine());
            if(inputNum == 1 )
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
            }
            else if(input == 2)
            {
                Console.WriteLine("사냥 필드 이동");
            }
            else { Console.WriteLine("잘못된 입력입니다"); }
        }



    }
    public class PlayerState
    {
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
        }
        public int DisplayPlayerState()
        {
            int input;
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("상태창");
            Console.WriteLine();
            Console.WriteLine($"LV : {Level}");
            Console.WriteLine($"{Name} ({Class})");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체  력 : {MaxHp}");
            Console.WriteLine($" Gold  : {Gold}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            input = int.Parse(Console.ReadLine());
            return input;

        }
    }

    public class Inventory
    {
        private string[] _items;
        private int _itemsAmount;
        private int _itemCount;

        public Inventory(int _itemsAmount)
        {
            _items = new string[_itemsAmount];
            _itemCount = 0;
        }

        public void AddItem(string item)
        {
            if(_itemCount < _itemsAmount)
            {
                _items[_itemCount] = item;
                _itemCount++;
            }
            else
            {
                Console.WriteLine("인벤토리가 가득 찼습니다.");
            }
        }

        public void RemoveItem(string item)
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
            if(_itemCount == 0)
            {
                Console.WriteLine("인벤토리에 아무것도 없군요...");
            }
            else
            {
                for (int i = 0; i < _itemCount; i++)
                {
                    Console.WriteLine($"{i}. {_items[i]}");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            input = int.Parse( Console.ReadLine() );
            
            return input;
        }
    }
}
