using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public static CardsManager Instance { get; private set; }

    Vector2 playerStartPos;
    Vector2 playerTargetPos;
    private RectTransform player_rect;

    Vector2 bankerStartPos;
    Vector2 bankerTargetPos;
    private RectTransform banker_rect;

    public GameObject Card;
    public Transform cardParent;

    private Sprite[] LoadSprites;

    private string cardName = string.Empty;
    private string winnerName = string.Empty;

    private bool isBankerDraw = false;
    private bool isPlayerDraw = false;
    private bool isPlayer = true;
    //private bool isGameFinished;

    private int count = 0;
    private int playerTotal;
    private int playerThirdCard;
    private int bankerTotal;
    private int bankerThirdCard;
    private int randomNo = 0;
    private int gameObjectsCounter = 0;
    private int beforeBankerTotal = -1;
    private int beforePlayerTotal = 0;
    private int timer = 20;

    public int counter = 0;
    public int bankerCounter = 0;

    public bool player3rdCounter = false;
    public bool banker3rdCounter = false;

    private bool isBankerDrawed = true;
    private bool isPlayerDrawed = true;
    public string isStates = string.Empty;
    public int practice = 0;

    public int cardQuestionsTotal = 2;
    public int cardQuestionsCounter = 1;
    public int cardQuestionCorrect = 0;

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
        Screen.orientation = ScreenOrientation.LandscapeRight;
        LoadAllResources();
        CardsAddToList();
        CardsMatchWithSprites();
        InvokeRepeating("CreateCards", 0.5f, 0.5f);
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
                cardName = Card10[randomNo];
                break;

            default:
                break;
        }

    }

    private void LoadAllResources()
    {
        LoadSprites = Resources.LoadAll<Sprite>("Graphic/Graphic/PlayingCard");
    }

    public void CreateCards()
    {

        int matchCardNo = Random.Range(1, 10);
        if (!isPlayer && counter != 4)
        {
            Banker.Add(matchCardNo);
            isPlayer = true;
        }
        else if (isPlayer && counter != 4)
        {
            Player.Add(matchCardNo);
            isPlayer = false;
        }
        if (counter == 4)
        {
            playerTotal = getPlayerData();
            bankerTotal = getBankerData();

            Debug.Log("CounterValue " + counter);
            CaculatePlayerData();
            counter = 0;
            CancelInvoke("CreateCards");
            return;
        }
        //Cards Limitation

        GameObject go = Instantiate(Card, cardParent);

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
        //go.transform.localPosition = new Vector2(0, -50);
        counter++;
    }


    public void RemoveCardNumberFromMainList(int cardNo)
    {
        for (int i = 0; i < CardsList.Count - 1; i++)
        {
            if (CardsList[i] == cardNo)
            {
                CardsList.RemoveAt(i);
                return;
            }
        }
    }

    public int DrawACard()
    {
        int cardNo = GetCardNumber();
        RemoveCardNumberFromMainList(cardNo);
        return cardNo;
    }

    public void DrawPlayerThirdCard()
    {
        int cardNo = DrawACard();
        beforePlayerTotal = playerTotal;
        Debug.Log("BeforePlayerTotal" + playerTotal);
        Debug.Log("CardNo" + cardNo);
        playerTotal += cardNo;
        Debug.Log("AfterPlayerTotal" + playerTotal);
        playerThirdCard = cardNo;
        Debug.Log("PLayerThirdCard" + playerThirdCard);
        isPlayerDraw = true;
    }

    public void DrawBankerThirdCard()
    {
        int cardNo = DrawACard();
        beforeBankerTotal = bankerTotal;
        Debug.Log("BeforeBankerTotal" + bankerTotal);
        Debug.Log("CardNo" + cardNo);
        bankerTotal += cardNo;
        Debug.Log("AfterBankerTotal" + bankerTotal);
        bankerThirdCard = cardNo;
        Debug.Log("BankerThirdCard" + bankerThirdCard);
        isBankerDraw = true;
    }

    public void CaculatePlayerData()
    {
        Debug.Log("PlayerTotal " + playerTotal);
        Debug.Log("BankerTotal " + bankerTotal);
        checkOver10();
        if (playerTotal == 8 || playerTotal == 9 || bankerTotal == 8 || bankerTotal == 9)
        {
            checkOver10();
            Debug.Log("Natural");
            isBankerDraw = false;
            isPlayerDraw = false;
            Debug.Log("player Banker Nautral" + playerTotal + " : " + bankerTotal);
            if (playerTotal > bankerTotal)
            {
                WinnerName("Player Wins");
                //UIManager.Instance.WinOrLose_Txt.text = "Player Wins";
               // UIManager.Instance.WinOrLose_Txt.text = winnerName;
                return;
            }
            else if (playerTotal < bankerTotal)
            {
                WinnerName("Banker Wins");
                //UIManager.Instance.WinOrLose_Txt.text = "Banker Wins";
                //UIManager.Instance.WinOrLose_Txt.text = winnerName;
                return;
            }
            else
            {
                WinnerName("Tie");
                return;
            }
        }
        else
        {


            checkOver10();
            //For Player Stands and Banker < 6
            if (playerTotal >= 6 && playerTotal <= 7)
            {
                Debug.Log("PlayerTotal playerTotal >= 6 && playerTotal <= 7");
                if (bankerTotal < 6)
                {
                    DrawBankerThirdCard();
                    //createBankerThirdCard();
                    Debug.Log("Banker Draw a card " + bankerThirdCard + "  Banker Total:  " + bankerTotal);

                    checkOver10();
                    if (bankerTotal > playerTotal)
                    {
                        WinnerName("Banker Wins");
                    }
                    else if (bankerTotal < playerTotal)
                    {
                        WinnerName("Player Wins");
                    }
                    else
                    {
                        WinnerName("Tie");
                    }
                } else if(bankerTotal >=6 || bankerTotal <= 7)
                {
                    if(playerTotal > bankerTotal)
                    {
                        WinnerName("Player Wins");
                    } else if (playerTotal < bankerTotal)
                    {
                        WinnerName("Banker Wins");
                    } else
                    {
                        WinnerName("Tie");
                    }
                }
            }
            else if (bankerTotal == 3 && playerThirdCard == 8)
            {
                if (bankerTotal > playerTotal)
                {
                    WinnerName("Banker Wins");
                }
                else if (bankerTotal < playerTotal)
                {
                    WinnerName("Player Wins");
                }
                else
                {
                    WinnerName("Tie");
                }
            }//Player is < 6 and check Banker checks Player 3rd card
            else if (playerTotal >= 0 && playerTotal < 6)
            {
                checkOver10();
                DrawPlayerThirdCard();
                //createPlayerThirdCard();
                checkOver10();
                Debug.Log("Player Draw a card " + playerThirdCard + "  Player Total:  " + playerTotal);

                isBankerDraw = checkBankerDraw(bankerTotal, playerThirdCard);
                if (isBankerDraw)
                {
                    DrawBankerThirdCard();
                    Debug.Log("IS BANKER DRAW " + isBankerDraw + "   BankerThirdCard :: " + bankerThirdCard);

                    checkOver10();
                    if (bankerTotal > playerTotal)
                    {
                        WinnerName("Banker Wins");
                    }
                    else if (bankerTotal < playerTotal)
                    {
                        WinnerName("Player Wins");
                    }
                    else
                    {
                        WinnerName("Tie");
                    }
                }
                else
                {
                    if (bankerTotal > playerTotal)
                    {
                        WinnerName("Banker Wins");
                    }
                    else if (bankerTotal < playerTotal)
                    {
                        WinnerName("Player Wins");
                    }
                    else
                    {
                        WinnerName("Tie");
                    }
                }

            }
            return;
        }
    }
    public void checkOver10()
    {
        if (playerTotal >= 10)
        {
            playerTotal -= 10;
        }
        else if (playerTotal > 20)
        {
            playerTotal -= 20;
        }

        if (bankerTotal >= 10)
        {
            bankerTotal -= 10;
        }
        else if (bankerTotal > 20)
        {
            bankerTotal -= 20;
        }
    }
    public void WinnerName(string winner)
    {
        winnerName = winner;
    }

    public int getPlayerData()
    {
        //playerTotal = 0;

        for (int i = 0; i < Player.Count; i++)
        {
            playerTotal += Player[i];
        }
        return playerTotal;
    }

    public int getBankerData()
    {
        //bankerTotal = 0;

        for (int i = 0; i < Banker.Count; i++)
        {
            bankerTotal += Banker[i];
        }
        return bankerTotal;
    }

    public bool checkBankerDraw(int bankerPoints, int player3rdCard)
    {
        switch (bankerPoints)
        {
            case 0:
            case 1:
            case 2:
                return true;                // The banker always draws a card if their points are less than 3
            case 3:
                if (player3rdCard != 8)
                {
                    return true;                // Draws a card if the player does not draw an 8
                }
                break;
            case 4:
                if (player3rdCard >= 2 && player3rdCard <= 7)
                {       // Draws a card if the player draws cards 2-7
                    return true;
                }
                break;
            case 5:
                if (player3rdCard >= 4 && player3rdCard <= 7)
                {       // Draws a card if the player draws cards 4-7
                    return true;
                }
                break;
            case 6:
                if (player3rdCard >= 6 && player3rdCard <= 7)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public void createPlayerThirdCard()
    {
        //if (isGameFinished)
        //{
        //    UIManager.Instance.Wrong_Txt.text = "Time is up! You failed";
        //    return;
        //}
        //UIManager.Instance.DisableCardBtns();
        Debug.Log("BeforeBankerTotal = " + beforeBankerTotal + " BeforePlayerTotal = " + beforePlayerTotal);
        UIManager.Instance.Wrong_Txt.text = string.Empty;
        if(isPlayerDraw != true)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Player cannot Draw";
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        }
        if(beforePlayerTotal == 8 || beforePlayerTotal == 9)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Player cannot draw in Nautral";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.imgCross.SetActive(true);
        }
        if (beforeBankerTotal == 8 || beforeBankerTotal == 9)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Player cannot draw";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
            if (beforePlayerTotal < beforeBankerTotal)
            {
                WinnerName("Banker Wins");
                 UIManager.Instance.WinOrLose_Txt.text = winnerName;
            }
        }
        if (isBankerDraw != true && isPlayerDraw != true)
        {
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            return;
        }
        if (isPlayerDraw && isPlayerDrawed == true)
        {
            Debug.Log("isPlayerDraw && isPlayerDrawed == true" + " " + isPlayerDraw + "" + isPlayerDrawed);
            InstantiatePlayerThirdCard();
            //UIManager.Instance.WinOrLose_Txt.text = winnerName;
        } else if(isPlayerDraw != true && isBankerDraw == true && isBankerDrawed != false)
        {
            UIManager.Instance.DisableCardBtns();
            InstantiateBankerThirdCard();
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.Wrong_Txt.text = "Player cannot draw, it stands";
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        } else if(isPlayerDraw != true && isBankerDraw == true && isBankerDrawed != true)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.Wrong_Txt.text = "Player cannot draw, it stands";
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        } else if(isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == true)
        {
            InstantiateBankerThirdCard();
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Player cannot draw again";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        }
        else
        {
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.DisableCardBtns();
            Debug.Log("isPlayerDraw " + isPlayerDraw + " isPlayerDrawed " + isPlayerDrawed);
            UIManager.Instance.Wrong_Txt.text = "Player cannot draw";
            UIManager.Instance.imgCross.SetActive(true);
            //Debug.Log("WRONG PLAYER DRAW");
            WrongAndStopTimer();
        }
    }

    public void createBankerThirdCard()
    {
        //if (isGameFinished)
        //{
        //    UIManager.Instance.Wrong_Txt.text = "Time is up! You failed";
        //    return;
        //}
        UIManager.Instance.Wrong_Txt.text = string.Empty;

        if (beforeBankerTotal == 8 || beforeBankerTotal == 9)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Banker cannot draw in Nautral";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        }
        if (beforePlayerTotal == 8 || beforePlayerTotal == 9)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.Wrong_Txt.text = "Banker cannot draw";
            WrongAndStopTimer();
            if (beforePlayerTotal > beforeBankerTotal)
            {
                WinnerName("Player Wins");
                 UIManager.Instance.WinOrLose_Txt.text = winnerName;
            }
        }
        if (isBankerDraw != true && isPlayerDraw != true)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Banker cannot draw";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        }
        if (isBankerDraw == true && isBankerDrawed == true && isPlayerDrawed == false && isPlayerDraw == true)
        {
            InstantiateBankerThirdCard();
            //UIManager.Instance.WinOrLose_Txt.text = winnerName;
            Debug.Log("isBankerDraw && isBankerDrawed == true && isPlayerDrawed == false && isPlayerDraw == true");
        }
        else if (isBankerDraw == true && isBankerDrawed == true && isPlayerDraw == false)
        {
            InstantiateBankerThirdCard();
            //UIManager.Instance.WinOrLose_Txt.text = winnerName;
            Debug.Log("isBankerDraw && isBankerDrawed == true && isPlayerDraw == false");
        }
        else if(isBankerDraw != true && isPlayerDraw == true && isPlayerDrawed == true)
        {
            UIManager.Instance.DisableCardBtns();
            InstantiatePlayerThirdCard();
            Debug.Log("isBankerDraw : " + isBankerDraw + " isBankerDrawed " + isBankerDrawed + " isPlayerDraw " + isPlayerDraw);
            UIManager.Instance.Wrong_Txt.text = "Banker cannot draw";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            Debug.Log("WRONG Banker DRAW");
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        } else if(isBankerDraw == true && isBankerDrawed == true && isPlayerDraw == true && isPlayerDrawed == true)
        {
            UIManager.Instance.DisableCardBtns();
            InstantiatePlayerThirdCard();
            InstantiateBankerThirdCard();
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.Wrong_Txt.text = "Banker cannot draw, Player needs to draw first";
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        } else if(isBankerDraw == false && isPlayerDraw == true && isPlayerDrawed == false)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.Wrong_Txt.text = "Banker cannot draw...";
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        }
        else if (isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == false)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Banker cannot draw again";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            UIManager.Instance.imgCross.SetActive(true);
            WrongAndStopTimer();
        }
    }

    public void CardEach()
    {
        //if (isGameFinished)
        //{
        //    UIManager.Instance.Wrong_Txt.text = "Time is up! You failed";
        //    return;
        //}
        //UIManager.Instance.DisableCardBtns();
        Debug.Log("CardEach" + beforeBankerTotal);
        if (isBankerDraw && isPlayerDraw && isBankerDrawed == true && isPlayerDrawed == true)
        {
            Debug.Log("CardEach True ");
            if (beforeBankerTotal == 0 || beforeBankerTotal == 1 || beforeBankerTotal == 2)
            {
                if (beforePlayerTotal < 6)
                {
                    Debug.Log("CardEach Banker ");
                    InstantiatePlayerThirdCard();
                    InstantiateBankerThirdCard();
                    isBankerDrawed = false;
                    isPlayerDrawed = false;
                    return;
                }
                else if (isPlayerDraw == true && isPlayerDrawed == false && isBankerDraw == true && isBankerDrawed == true)
                {
                    UIManager.Instance.imgCross.SetActive(true);
                    UIManager.Instance.DisableCardBtns();
                    InstantiateBankerThirdCard();
                    UIManager.Instance.WinOrLose_Txt.text = winnerName;
                    UIManager.Instance.Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect xD";
                    isBankerDrawed = false;
                    WrongAndStopTimer();
                }
            } else
            {
                InstantiatePlayerThirdCard();
                InstantiateBankerThirdCard();
                isBankerDrawed = false;
                isPlayerDrawed = false;
                UIManager.Instance.Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect xD";
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                WrongAndStopTimer();
                return;
            }
            
        } 
        
        if(isPlayerDraw != true && isBankerDraw != true)
        {
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            WrongAndStopTimer();
        } else if(isBankerDraw == true && isPlayerDraw == false)
        {
            InstantiateBankerThirdCard();
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            isPlayerDrawed = false;
            WrongAndStopTimer();
        } else if(isPlayerDraw == true && isBankerDraw == false)
        {
            InstantiatePlayerThirdCard();
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            WrongAndStopTimer();
        } else if(isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == false)
        {
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "You have already drawed CardEach. Incorrect.";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            WrongAndStopTimer();
        } else if (isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == true)
        {
            InstantiateBankerThirdCard();
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "You clicked Draw Player. Cannot click CardEach";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            WrongAndStopTimer();
        }

        //Test
        
    }

    public void InstantiateBankerThirdCard()
    {
        GameObject go = Instantiate(Card, cardParent);
        ChooseCards(bankerThirdCard);
        RemoveCardNumberFromMainList(bankerThirdCard);
        Image img = go.transform.GetChild(0).GetComponent<Image>();
        for (int i = 0; i < LoadSprites.Length; i++)
        {
            if (LoadSprites[i].name == cardName)
            {
                img.sprite = LoadSprites[i];
            }
        }
        Destroy(go.GetComponent<CardsCaculation>());
        banker_rect = go.GetComponent<RectTransform>();
        bankerStartPos = banker_rect.anchoredPosition;
        bankerTargetPos = new Vector2(278, 387);
        StartCoroutine(moveBankerThirdCard());
        isBankerDrawed = false;
    }

    public void InstantiatePlayerThirdCard()
    {
        Debug.Log("PlayerDraw");
        GameObject go = Instantiate(Card, cardParent);
        ChooseCards(playerThirdCard);
        RemoveCardNumberFromMainList(playerThirdCard);
        Image img = go.transform.GetChild(0).GetComponent<Image>();
        for (int i = 0; i < LoadSprites.Length; i++)
        {
            if (LoadSprites[i].name == cardName)
            {
                img.sprite = LoadSprites[i];
            }
        }
        Destroy(go.GetComponent<CardsCaculation>());
        player_rect = go.GetComponent<RectTransform>();
        playerStartPos = player_rect.anchoredPosition;
        playerTargetPos = new Vector2(1221, 387);
        StartCoroutine(movePlayerThirdCard());            //go.transform.localPosition = new Vector2(1221, 540);
        isPlayerDrawed = false;
    }
    public void NextGame()
    {
        UIManager.Instance.ActiveCardBtns();
        UIManager.Instance.WinOrLose_Txt.text = string.Empty;
        UIManager.Instance.Wrong_Txt.text = string.Empty;
        foreach (Transform eachChild in cardParent)
        {
            if (eachChild.name == "RealCard(Clone)")
            {
                Destroy(eachChild.gameObject);
            }
        };
        CancelInvoke("CreateCards");
        UIManager.Instance.CloseBtnsInNextGame();
        //StartCoroutine(UIManager.Instance.ActiveCardButtons());
        count = 0;
        Player = new List<int>();
        Banker = new List<int>();
        CardsList = new List<int>();
        cardName = string.Empty;
        winnerName = string.Empty;

        isBankerDraw = false;
        isPlayerDraw = false;
        isPlayer = true;

        playerTotal = 0;
        playerThirdCard = 0;
        bankerTotal = 0;
        bankerThirdCard = 0;
        randomNo = 0;
        counter = 0;
        player3rdCounter = false;
        banker3rdCounter = false;
        isBankerDrawed = true;
        isPlayerDrawed = true;
        beforeBankerTotal = -1;
        beforePlayerTotal = 0;
        //isGameFinished = false;
        timer = 20;
        checkCardsPracOrTest(isStates);
        CardsAddToList();
        CardsMatchWithSprites();
        InvokeRepeating("CreateCards", 0.5f, 0.5f);
        //if (isStates == "isCardTest")
        //{
        //    UIManager.Instance.btnNextGame.gameObject.SetActive(false); 
        //}
    }

    private IEnumerator movePlayerThirdCard()
    {
        float z = -90f;
        yield return new WaitForSeconds(1f);
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * 5;
            player_rect.anchoredPosition = Vector2.Lerp(playerStartPos, playerTargetPos, t);
            yield return new WaitForEndOfFrame();
        }
        player_rect.anchoredPosition = playerTargetPos;
        StartCoroutine(RotateCards(player_rect, z));
    }

    private IEnumerator moveBankerThirdCard()
    {
        float z = 90f;
        yield return new WaitForSeconds(1f);
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * 5;
            banker_rect.anchoredPosition = Vector2.Lerp(bankerStartPos, bankerTargetPos, t);
            yield return new WaitForEndOfFrame();
        }
        banker_rect.anchoredPosition = bankerTargetPos;
        StartCoroutine(RotateCards(banker_rect, z));
    }

    private IEnumerator RotateCards(RectTransform m_rect, float degree)
    {
        float t = 0;
        yield return new WaitForSeconds(1f);

        while (t < 1f)
        {
            t += Time.deltaTime * 3;
            m_rect.transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 0, degree), t);
            yield return new WaitForEndOfFrame();
        }
        m_rect.transform.rotation = Quaternion.Euler(0, 180, degree);

    }

    //Banker Draw Button (380,360);
    //Player Draw Button (1100, 360);
    public void CountChildObjects()
    {
        foreach (Transform eachChild in cardParent)
        {
            if (eachChild.name == "RealCard(Clone)")
            {
                if (gameObjectsCounter == 4)
                {
                    Debug.Log("gameObjectsCounter " + gameObjectsCounter);
                    StartCoroutine(UIManager.Instance.ActiveCardButtons());
                    gameObjectsCounter = 0;
                }
                gameObjectsCounter++;
            }
        }
    }

    public void CheckTrueOrFalseWinner(string name)
    {
        
        Debug.Log("CHECK TRUE OR FALSE" + winnerName);
        if (name != winnerName)
        {
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Wrong";

            if (isPlayerDraw && isPlayerDrawed)
            {
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                InstantiatePlayerThirdCard();
                UIManager.Instance.Wrong_Txt.text = "You are wrong, Player needs to draw 3rd card";
                UIManager.Instance.imgCross.SetActive(true);
                Debug.Log("FUCK1");
                if (isBankerDraw && isBankerDrawed)
                {
                    InstantiateBankerThirdCard();
                }
                isPlayerDrawed = false;
                WrongAndStopTimer();
            }
            if (isBankerDraw && isBankerDrawed)
            {
                UIManager.Instance.WinOrLose_Txt.text = winnerName;

                InstantiateBankerThirdCard();
                UIManager.Instance.Wrong_Txt.text = "You are wrong, Banker needs to draw 3rd card";
                UIManager.Instance.imgCross.SetActive(true);
                Debug.Log("FUCK2");
                isBankerDrawed = false;
                WrongAndStopTimer();
            }
            if (isPlayerDraw && isPlayerDrawed == false && isBankerDraw && isBankerDrawed == false)
            {
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Incorrect";
                UIManager.Instance.imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            else if (isBankerDraw != true && isPlayerDraw != true)
            {
                Debug.Log("WTF DOG");
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Incorrect";
                UIManager.Instance.imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            if (isPlayerDraw == false && isBankerDraw == true && isBankerDrawed == true)
            {
                InstantiateBankerThirdCard();
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Incorrect";
                UIManager.Instance.imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            if (isPlayerDraw == false && isBankerDraw == true && isBankerDrawed == false)
            {
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Incorrect";
                UIManager.Instance.imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            if (isPlayerDraw == true && isBankerDraw == false && isPlayerDrawed == false)
            {
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Incorrect";
                UIManager.Instance.imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            return;
        } else if(name == winnerName) 
        {
            Debug.Log("Tuu twr p" + winnerName);    
            if (isPlayerDraw && isPlayerDrawed )
            {
                UIManager.Instance.DisableCardBtns();
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                
                InstantiatePlayerThirdCard();
                UIManager.Instance.Wrong_Txt.text = "You are wrong, Player needs to draw 3rd card";
                UIManager.Instance.imgCross.SetActive(true);
                Debug.Log("FUCK1");
                if(isBankerDraw && isBankerDrawed )
                {
                    InstantiateBankerThirdCard();
                }
                isPlayerDrawed = false;
                WrongAndStopTimer();
                return;
            }
            if (isBankerDraw && isBankerDrawed)
            {
                UIManager.Instance.DisableCardBtns();
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                
                InstantiateBankerThirdCard();
                UIManager.Instance.Wrong_Txt.text = "You are wrong, Banker needs to draw 3rd card";
                UIManager.Instance.imgCross.SetActive(false);
                Debug.Log("FUCK2");
                isBankerDrawed = false;
                WrongAndStopTimer();
                return;
            }
            if (isPlayerDraw == true && isPlayerDrawed == false && isBankerDraw== true && isBankerDrawed == true)
            {
                InstantiateBankerThirdCard();
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "You are wrong, Banker needs to draw 3rd card";
                UIManager.Instance.imgCross.SetActive(false);
                WrongAndStopTimer();
            }
            if(isPlayerDraw && isPlayerDrawed == false && isBankerDraw && isBankerDrawed == false)
            {
                UIManager.Instance.DisableCardBtns();
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Correct";
                CancelInvoke("Timer");
                StartCoroutine("NextGameAfterTimer");
            } else if(isBankerDraw != true && isPlayerDraw != true)
            {
                UIManager.Instance.DisableCardBtns();
                Debug.Log("WTF DOG");
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Correct";
                CancelInvoke("Timer");
                StartCoroutine("NextGameAfterTimer");
            }
            if (isPlayerDraw == false && isBankerDraw == true && isBankerDrawed == true)
            {
                UIManager.Instance.DisableCardBtns();
                InstantiateBankerThirdCard();
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Incorrect";
                UIManager.Instance.imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            if (isPlayerDraw == false && isBankerDraw == true && isBankerDrawed == false)
            {
                UIManager.Instance.DisableCardBtns();
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Correct";
                //UIManager.Instance.imgCross.SetActive(true);
                CancelInvoke("Timer");
                StartCoroutine("NextGameAfterTimer");
            }
            if(isPlayerDraw == true && isBankerDraw == false && isPlayerDrawed == false)
            {
                UIManager.Instance.DisableCardBtns();
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                UIManager.Instance.Wrong_Txt.text = "Correct";
                CancelInvoke("Timer");
                StartCoroutine("NextGameAfterTimer");
                //UIManager.Instance.imgCross.SetActive(true);
            }
        }
    }

    public void Timer()
    {
        UIManager.Instance.cardTimer_Txt.gameObject.SetActive(true);
        UIManager.Instance.cardTimer_Txt.text = timer.ToString();
        if (timer == 0)
        {
            Debug.Log("UP P");
            UIManager.Instance.Wrong_Txt.text = "Time is Up!!!! ";
            forCardTest();
            WrongAndStopTimer();
            return;
        }
        //Debug.Log("TIMER " + timer);
        timer--;
    }

    public void checkCardsPracOrTest(string checkCardExam)
    {
        AchievementManager.Instance.CardGame.SetActive(true);
        Debug.Log("CardNo Ha : " + cardQuestionsTotal);
        isStates = checkCardExam;

        if (isStates == "isCardTest")
        {   
            if (cardQuestionsCounter == cardQuestionsTotal)
            {
                Debug.Log("Pyae Twr p ha, checkCardsPracOrTest () : "+ cardQuestionsTotal);
                StopAllCoroutines();
                CancelInvoke("Timer");
                CancelInvoke("CreateCards");
                UIManager.Instance.ResultPanel.SetActive(true);
                return;
            }
            cardQuestionsCounter++;
            InvokeRepeating("Timer", 1f, 1f);
        } 
    }

    public IEnumerator NextGameAfterTimer()
    {
        cardQuestionCorrect++;
        //float subtract = cardQuestionsTotal - cardQuestionCorrect;
        
        UIManager.Instance.ProgressBar.GetComponent<RectTransform>().localScale =new Vector3(cardQuestionCorrect/cardQuestionsTotal, 1,1) ;
        yield return new WaitForSeconds(3f);
        NextGame();
    }

    public IEnumerator NextGameAfterTimerWrong()
    {
        yield return new WaitForSeconds(7f);
        NextGame();
    }

    public void forCardTest()
    {
        if (isBankerDraw && isPlayerDraw && isBankerDrawed == true && isPlayerDrawed == true)
        {
            if (beforeBankerTotal == 0 || beforeBankerTotal == 1 || beforeBankerTotal == 2)
            {
                if (beforePlayerTotal < 6)
                {
                    Debug.Log("CardEach Banker ");
                    InstantiatePlayerThirdCard();
                    InstantiateBankerThirdCard();
                    isBankerDrawed = false;
                    isPlayerDrawed = false;
                    return;
                }
                else if (isPlayerDraw == true && isPlayerDrawed == false && isBankerDraw == true && isBankerDrawed == true)
                {
                    UIManager.Instance.imgCross.SetActive(true);
                    UIManager.Instance.DisableCardBtns();
                    InstantiateBankerThirdCard();
                    UIManager.Instance.WinOrLose_Txt.text = winnerName;
                    UIManager.Instance.Wrong_Txt.text = "Timer is up!";
                    isBankerDrawed = false;
                }
            }
            else 
            {
                Debug.Log("xD");
                InstantiatePlayerThirdCard();
                InstantiateBankerThirdCard();
                isBankerDrawed = false;
                isPlayerDrawed = false;
                UIManager.Instance.Wrong_Txt.text = "Timer is up!";
                UIManager.Instance.WinOrLose_Txt.text = winnerName;
                return;
            }

        }

        if (isPlayerDraw != true && isBankerDraw != true)
        {
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Timer is up!";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
        }
        else if (isBankerDraw == true && isPlayerDraw == false && isBankerDrawed == true)
        {
            InstantiateBankerThirdCard();
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Timer is up!";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            isBankerDrawed = false;
        }
        else if (isPlayerDraw == true && isBankerDraw == false && isPlayerDrawed == true)
        {
            InstantiatePlayerThirdCard();
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Timer is up!";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
            isPlayerDrawed = false;
        }
        else if (isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == false)
        {
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Timer is up!";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
        }
        else if (isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == true)
        {
            InstantiateBankerThirdCard();
            UIManager.Instance.imgCross.SetActive(true);
            UIManager.Instance.DisableCardBtns();
            UIManager.Instance.Wrong_Txt.text = "Timer is up!";
            UIManager.Instance.WinOrLose_Txt.text = winnerName;
        }
    }

    public void WrongAndStopTimer()
    {
       if(isStates == "isCardTest")
        {
            CancelInvoke("Timer");
            StartCoroutine("NextGameAfterTimerWrong");
            UIManager.Instance.DisableCardBtns();
        }
    }
}
