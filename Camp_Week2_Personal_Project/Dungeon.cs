namespace Camp_Week2_Personal_Project
{
    public class Dungeon
    {
        PlayerState playerState;
        int recommend;
        int reward;
        int clearTime = 0;
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
                    playerState.Hp -= damage;
                    Console.WriteLine($"플레이어의 현재 체력 : {playerState.Hp}");
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
                clearTime++;
                playerState.LevelUp(clearTime);
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
