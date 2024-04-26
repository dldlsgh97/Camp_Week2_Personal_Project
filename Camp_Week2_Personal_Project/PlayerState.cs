namespace Camp_Week2_Personal_Project
{
    public class PlayerState
    {
        private List<Item> equippedItems;

        public int Level { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public float Attack { get; set; }
        public int Defense { get; set; }
        public int MaxHp { get; }
        public int Gold { get; set; }
        public int Hp;


        public PlayerState(int level, string name, string className, float attack, int defense, int maxHp, int gold)
        {
            Level = level;
            Name = name;
            Class = className;
            Attack = attack;
            Defense = defense;
            MaxHp = maxHp;
            Gold = gold;
            Hp = MaxHp;
            equippedItems = new List<Item>();
        }
        public void LevelUp(int cleartime)
        {
            if(Level == 1 && cleartime ==1)
            {
                Level++;
                Attack += 0.5f;
                Defense += 1;
                Console.WriteLine($"플레이어의 레벨이 {Level - 1}에서 {Level}으로 레벨업 했습니다!");
            }
            else if(Level == 2 && cleartime == 3)
            {
                Level++;
                Attack += 0.5f;
                Defense += 1;
                Console.WriteLine($"플레이어의 레벨이 {Level - 1}에서 {Level}으로 레벨업 했습니다!");
            }
            else if( Level == 3 && cleartime == 6)
            {
                Level++;
                Attack += 0.5f;
                Defense += 1;
                Console.WriteLine($"플레이어의 레벨이 {Level - 1}에서 {Level}으로 레벨업 했습니다!");
            }
            else if(Level == 4 && cleartime == 10)
            {
                Level++;
                Attack += 0.5f;
                Defense += 1;
                Console.WriteLine($"플레이어의 레벨이 {Level - 1}에서 {Level}으로 레벨업 했습니다!");
            }
            
        }
        public int DisplayPlayerState()
        {
            RefreshPlayerState();
            string userInput = Console.ReadLine();
            int input;
            if(int.TryParse(userInput, out input))
            {
                return input;
            }
            return -1;
            
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
                Console.WriteLine($"체  력 : {Hp}");
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
}
