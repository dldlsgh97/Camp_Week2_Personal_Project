namespace Camp_Week2_Personal_Project
{
    internal class Program
    {
        static int Level = 1;
        static string Name = "name";
        static string Class = "전사";
        static int Attack = 10;
        static int Defense = 0;
        static int MaxHp = 100;
        static int Gold = 0;

        static void Main(string[] args)
        {
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
                PlayerState();
            }
            else if (inputNum == 2)
            {
                PlayerInventory();
            }
            else if (inputNum == 3)
            {
                Move();
            }
        }

        static void PlayerState()
        {
            int input;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("상태창");
            Console.WriteLine($"LV : {Level}");
            Console.WriteLine($"{Name} ({Class})");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체  력 : {MaxHp}");
            Console.WriteLine($" Gold  : {Gold}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            input = int.Parse( Console.ReadLine());
            if (input == 0 )
            {
                StartGame();
            }
            else
            {
                Console.WriteLine("Error");
                PlayerState();
            }
        }
        static void PlayerInventory()
        {
            Console.WriteLine("인벤토리창 출력");
        }

        static void Move()
        {
            Console.WriteLine("이동 로직 실행");
        }



    }
}
