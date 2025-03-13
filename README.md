## 📗 : 프로젝트

- Name : C# Text - RPG
- Stack : C#
- Notion : [C# 개인 프로젝트](https://www.notion.so/C-48f9ee97fa024222bc1f4a7cdd087c7e?pvs=21)

---

## ⚙️ : 기능

---

- 필수 구현 기능
    - 기본 페이지 구현
        - [기본 페이지 구현](https://www.notion.so/0bea7a2f05b349f3ba84860a3d799462?pvs=21)
    - 인벤토리 구현
        - [아이템 정보 관리](https://www.notion.so/1e791b439fa7477187116cbd7fa415a9?pvs=21)
        - [인벤토리 세부기능(장착) 구현](https://www.notion.so/9dae5ff7cb184dc5b80bfa80d14adad9?pvs=21)
        - [장착한 아이템의 스텟을 플레이어의 스텟창에 출력](https://www.notion.so/4872e69db7b543aaaa86fa0730dbd491?pvs=21)
    - 상점기능 구현
        - [상점 페이지 구현](https://www.notion.so/185b10d6554e4f2ab36075576521ef77?pvs=21)
        - [플레이어의 구매로직 구현](https://www.notion.so/372a128fbe5b4cb5a08ac41c33e485ec?pvs=21)
    - 던전기능 구현
        - [던전 구현](https://www.notion.so/8db0974bb6aa4ccdb0da6fe623703017?pvs=21)
- 추가 구현 기능
    - 휴식 기능 구현
        - [휴식기능 구현](https://www.notion.so/5ec9ccf9b3664d68841bb0d4de8ea00d?pvs=21)
    - 플레이어 레벨업 기능 구현
        - [던전 클리어 횟수에 따른 레벨업 구현](https://www.notion.so/0069adaea7854176a84d856e8d133ca6?pvs=21)
    
    ## 💫  Troubleshooting
    
    ---
    
    <aside>
    ❓ 키 입력을 빈칸으로 입력 시 오류 발생
    
    ---
    
    📌 세부 내용
    
    - 해결전
    
    ```csharp
    int input = int.Parse(Console.ReadLine());
    return input;
    ```
    
    - input을 바로 Console.ReadLine()함수를 이용해서 string으로 받은 입력을 int로 무조건 바꿨었다.
    - 해결 후
    
    ```csharp
    string userInput = Console.ReadLine();
    int input;
    if(int.TryParse(userInput, out input))
    {
        return input;
    }
    ```
    
    - userInput 을 string형태로 받고 int.TryParse()를 이용해서 int로 변경할수 있을때 변경을 한뒤 이후 코드를 진행
    </aside>
    
    <aside>
    ❓ 클래스 코드분할
    
    ---
    
    📌 세부 내용
    
    - 해결 전
        - 이전에는 모든 클래스를 Program.cs파일에 모두 넣어놨었다
        - 이렇게 되면 코드가 많이 길어지고 클래스를 파악하기 힘들다.
    - 해결 후
        
        ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/355f39fe-2ef3-4d84-8da2-47ab7ff7b36f/78dca835-b861-44c3-8a8c-c61e0401ff06/Untitled.png)
        
        - 여러가지 클래스들을 각각의 파일로 분할해서 따로 저장한다.
        - 이렇게 되면 각각의 클래스를 구분하기 편해지고 관리도 쉬워진다.
    </aside>
    
    <aside>
    ❓ 장착된 아이템 판매시 증가된 스텟 복구
    
    ---
    
    📌 세부 내용
    
    - 인벤토리에 장착된 아이템을 판매시 아이템을 장착했을 때 증가된 스텟이 초기화 되지 않는 오류
    - 해결방법
        - SellItem
        
        ```csharp
        void SellItem(int input, List<ShopProduct> inventoryProducts)
        {
            //..생략
            else
            {
                playerState.Gold += (int)(inventoryProducts[input - 1].Price * 0.85f);
                playerInventory.RemoveItem(inventoryProducts[input - 1].Item);
                ChangeItemState(inventoryProducts[input - 1]);
                
                playerState.UnEquipItem(inventoryProducts[input - 1].Item);
               
                inventoryProducts.RemoveAt(input - 1);               
                SellProduct();
            }
        }
        ```
        
        - 아이템을 판매할때 로직에 playerState.UnEquipItem함수를 넣어서 장착을 해제한뒤 아이템을 판매
    </aside>
    

## 📝  프로젝트 진행중 아쉬웠던 부분

---

<aside>
❓ 게임 타이틀 제작

---

📌 세부 내용

- 게임의 타이틀의 디자인을 조금 신경써서 제작을 하지 못한것이 아쉽다
</aside>

<aside>
❓ 게임 데이터 저장

---

📌 세부 내용

- 게임의 데이터를 저장하고 다음에 다시 게임을 실행했을때 저장되어있는 게임의 데이터를 불러오는 기능을 제작하지 못한것이 아쉽다
- Json을 공부해서 다음 프로젝트를 진행할때 게임 데이터 저장기능을 구현해보고 싶다
</aside>
