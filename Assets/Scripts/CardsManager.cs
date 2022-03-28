using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public static CardsManager Instance { get; private set; }
   
    public Transform cardParent;
  
    public int counter = 0;
    private int randomNo = 0;
    private string cardName = string.Empty;
    int count = 0;

    private bool isPlayer = true;
    private bool onceCheck = false;
    public GameObject Card;
    private Sprite[] LoadSprites;

    private int playerTotal;
    private int playerThirdCard;

    private int bankerTotal;
    private int bankerThirdCard;

    List<int> CardsList = new List<int>();
    List<string> Card1 = new List<string>();
    List<string> Card2 = new List<string>();
    List<string> Card3 = new List<string>();
    List<string> Card4 = new List<string>();
    List<string> Card5 = new List<string>();
    List<string> Card6 = new List<string>();
    List<string> Card7 = new List<string>();
    List<string> Card8 = new List<string>();
    List<string> Card9 = new List<string>();
    List<string> Card10 = new List<string>();

    public List<int> Player = new List<int>();
    public List<int> Banker = new List<int>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        LoadAllResources();
        CardsAddToList();
        CardsMatchWithSprites();
        InvokeRepeating("createCards", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetCardNumber()
    {
        int generateNumber = Random.Range(1, CardsList.Count);
        int cardNumber = CardsList[generateNumber];
        return cardNumber;
    }

    private void CardsAddToList()
    {
        if (count < 4)
        {
            int a = 1;

            for (var i = 0; i < 80; i++)
            {
                if (a == 11)
                {
                    a = 1;
                }
                CardsList.Add(a);
                a++;
                count++;
            }
        }
    }

    public void CardsMatchWithSprites()
    {
        Card1.Add("clover_1");
        Card1.Add("dia_1");
        Card1.Add("heart_1");
        Card1.Add("spade_1");

        Card2.Add("clover_2");
        Card2.Add("dia_2");
        Card2.Add("heart_2");
        Card2.Add("spade_2");

        Card3.Add("clover_3");
        Card3.Add("dia_3");
        Card3.Add("heart_3");
        Card3.Add("spade_3");

        Card4.Add("clover_4");
        Card4.Add("dia_4");
        Card4.Add("heart_4");
        Card4.Add("spade_4");

        Card5.Add("clover_5");
        Card5.Add("dia_5");
        Card5.Add("heart_5");
        Card5.Add("spade_5");

        Card6.Add("clover_6");
        Card6.Add("dia_6");
        Card6.Add("heart_6");
        Card6.Add("spade_6");

        Card7.Add("clover_7");
        Card7.Add("dia_7");
        Card7.Add("heart_7");
        Card7.Add("spade_7");

        Card8.Add("clover_8");
        Card8.Add("dia_8");
        Card8.Add("heart_8");
        Card8.Add("spade_8");

        Card9.Add("clover_9");
        Card9.Add("dia_9");
        Card9.Add("heart_9");
        Card9.Add("spade_9");

        Card10.Add("clover_10");
        Card10.Add("dia_10");
        Card10.Add("heart_10");
        Card10.Add("spade_10");
        Card10.Add("clover_11");
        Card10.Add("clover_12");
        Card10.Add("clover_13");
        Card10.Add("dia_11");
        Card10.Add("dia_12");
        Card10.Add("dia_13");
        Card10.Add("heart_11");
        Card10.Add("heart_12");
        Card10.Add("heart_13");
        Card10.Add("spade_11");
        Card10.Add("spade_12");
        Card10.Add("spade_13");
    }

    private void ChooseCards(int CardNo)
    {
        
        switch (CardNo)
        {
            case 1:
                randomNo = Random.Range(0, Card1.Count - 1);
                cardName = Card1[randomNo];
                break;
            case 2:
                randomNo = Random.Range(0, Card2.Count - 1);
                cardName = Card2[randomNo];
                break;
            case 3:
                randomNo = Random.Range(0, Card3.Count - 1);
                cardName = Card3[randomNo];
                break;
            case 4:
                randomNo = Random.Range(0, Card4.Count - 1);
                cardName = Card4[randomNo];
                break;
            case 5:
                randomNo = Random.Range(0, Card5.Count - 1);
                cardName = Card5[randomNo];
                break;
            case 6:
                randomNo = Random.Range(0, Card6.Count - 1);
                cardName = Card6[randomNo];
                break;
            case 7:
                randomNo = Random.Range(0, Card7.Count - 1);
                cardName = Card7[randomNo];
                break;
            case 8:
                randomNo = Random.Range(0, Card8.Count - 1);
                cardName = Card8[randomNo];
                break;
            case 9:
                randomNo = Random.Range(0, Card9.Count - 1);
                cardName = Card9[randomNo];
                break;
            case 10:
                randomNo = Random.Range(0, Card10.Count - 1);
                cardName = Card9[randomNo];
                break;

            default:
                break;
        }
        
    }

    private void LoadAllResources()
    {
        LoadSprites = Resources.LoadAll<Sprite>("Graphic/Graphic/PlayingCard");
    }

    public void createCards()
    {
        int matchCardNo = Random.Range(1, 10);
        if (!isPlayer && counter != 4)
        {
            Banker.Add(matchCardNo);
            isPlayer = true;
        } else if (isPlayer && counter != 4)
        {
            Player.Add(matchCardNo);
            isPlayer = false;
        }
        //Cards Limitation
        if (counter == 4)
        {
            CaculatePlayerData();
            return;
        }
        GameObject go = Instantiate(Card, cardParent);
        
        //Debug.Log("CARDNO " + matchCardNo);
        ChooseCards(matchCardNo);
        RemoveCardNumberFromMainList(matchCardNo);
        Image img = go.transform.GetChild(0).GetComponent<Image>();
        for (int i = 0; i < LoadSprites.Length; i++)
        {
            if (LoadSprites[i].name == cardName)
            {
                img.sprite = LoadSprites[i];
            }
        }
        go.transform.localPosition = new Vector2(0, -50);
        counter++;
    }

    public void RemoveCardNumberFromMainList(int cardNo)
    {
        for(int i = 0; i < CardsList.Count; i++)
        {
            if(CardsList[i] == cardNo)
            {
                CardsList.RemoveAt(i+1);
                return;
            }
        }
    }

    public void DrawPlayerThirdCard()
    {
        Debug.Log("DRAW PLAYER THIRD CARD");
        int cardNo = GetCardNumber();
        playerThirdCard = cardNo;

        RemoveCardNumberFromMainList(cardNo);
    }
    public void CaculatePlayerData()
    {
        if(playerTotal < 10)
        {
            // နောက်ဆုံးအလုံးကိုယူရမယ်

        }
        Debug.Log("CACULATE PLAYER DATA");   
        if(playerTotal == 8 || playerTotal == 9)
        {
            if(bankerTotal > playerTotal)
            {
                WinnerName("Banker");
            } else if (playerTotal == bankerTotal)
            {
                Tie();
            } else if(bankerTotal < playerTotal)
            {
                WinnerName("Player");
            }
        }
        else if (playerTotal <= 5) // HAVE AN ERROR
        {
            Debug.Log("LESS THAN 5");
            DrawPlayerThirdCard();
            int playerData = getPlayerData();
            playerData += playerThirdCard;
            playerTotal = playerData;
            onceCheck = true;
        }
        else if (playerTotal > bankerTotal)
        {
            Debug.Log("playerTotal" + getPlayerData());
            Debug.Log("bankerTotal" + getBankerData());
            WinnerName("Player");
        }
        else if (playerTotal < bankerTotal)
        {
            Debug.Log("playerTotal" + getPlayerData());
            Debug.Log("bankerTotal" + getBankerData());
            WinnerName("Banker");
        }
        else if (onceCheck == true)
        {
            Debug.Log("One Check True");
            return;
        }
    }

    public void WinnerName(string winner)
    {
        Debug.Log("WINNER : " + winner);
    }

    public void Tie()
    {
        Debug.Log("IT's A TIE:::::::::");
    }

    public int getPlayerData()
    {
        playerTotal = 0;

        for (int i = 0; i < Player.Count; i++)
        {
            playerTotal += Player[i];
        }
        return playerTotal;
    }

    public int getBankerData()
    {
        bankerTotal = 0;

        for (int i = 0; i < Banker.Count; i++)
        {
            bankerTotal += Banker[i];
        }
        return bankerTotal;
    }
}
