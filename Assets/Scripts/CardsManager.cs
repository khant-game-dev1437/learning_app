using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;
using UnityEngine.SceneManagement;

public class CardsManager : MonoBehaviour
{
    public static CardsManager Instance { get; private set; }

    //UI

    public Button btnQuit;
    public Button btnBankerThird;
    public Button btnPlayerThird;
    public Button btnNextGame;
    public Button btnCardEach;
    public Button btnPlayerWins;
    public Button btnBankerWins;
    public Button btnTie;
    public Text WinOrLose_Txt;
    public Text Wrong_Txt;
    public Text cardTimer_Txt;
    public GameObject imgCross;
    public Image ProgressBar;
    public GameObject ResultPanel;
    public Image ResultPanelProgressBar;
    public Button BtnBackToMenu;
    public GameObject CirleResult;
    public GameObject FailTest;

    [SerializeField]
    private int UnlockCardGame = 0;
    private int UnlockCardGameCounter = 0;
    [HideInInspector]
    public static int saveTimer = 0;
    public string cardStates = string.Empty;

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
    public int timer = 0;

    public int counter = 0;
    public int bankerCounter = 0;

    [HideInInspector]
    public bool player3rdCounter = false;
    [HideInInspector]
    public bool banker3rdCounter = false;

    private bool isBankerDrawed = true;
    private bool isPlayerDrawed = true;

    
    [HideInInspector]
    public int practice = 0;

    public int cardQuestionsTotal = 2;
    public static int cardQuesTotal;

    [HideInInspector]
    public int cardQuestionsCounter = 0;

    [HideInInspector]
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

    [HideInInspector]
    public List<int> Player = new List<int>();
    [HideInInspector]
    public List<int> Banker = new List<int>();


    private void Awake()
    {
        cardQuesTotal = cardQuestionsTotal;
        saveTimer = timer;
        Debug.Log("QUES TOTAL" + cardQuesTotal);
        LoadAllResources();
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
        
        btnQuit.onClick.AddListener(QuitGame);
        cardTimer_Txt.gameObject.SetActive(false);
        imgCross.SetActive(false);
        btnBankerThird.gameObject.SetActive(false);
        btnPlayerThird.gameObject.SetActive(false);
        btnCardEach.gameObject.SetActive(false);
        btnNextGame.gameObject.SetActive(false);
        btnBankerWins.gameObject.SetActive(false);
        btnPlayerWins.gameObject.SetActive(false);
        btnTie.gameObject.SetActive(false);
        btnBankerWins.onClick.AddListener(delegate { CardsManager.Instance.CheckTrueOrFalseWinner(btnBankerWins.gameObject.transform.GetChild(0).GetComponent<Text>().text); });
        btnPlayerWins.onClick.AddListener(delegate { CardsManager.Instance.CheckTrueOrFalseWinner(btnPlayerWins.gameObject.transform.GetChild(0).GetComponent<Text>().text); });
        btnTie.onClick.AddListener(delegate { CardsManager.Instance.CheckTrueOrFalseWinner(btnTie.gameObject.transform.GetChild(0).GetComponent<Text>().text); });
        BtnBackToMenu.onClick.AddListener(ShowMenu);

        Screen.orientation = ScreenOrientation.LandscapeRight;
        Ready();
        checkCardsPracOrTest(cardStates);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowMenu()
    {
        SceneManager.LoadScene("SampleScene");
        UIManager.Instance.Register.SetActive(false);
        UIManager.Instance.MainMenu.SetActive(true);
        UIManager.Instance.CardRulePracPanel.SetActive(false);
        UIManager.Instance.MainMenu.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void DisableCardBtns()
    {
        btnBankerThird.interactable = false;
        btnPlayerThird.interactable = false;
        btnCardEach.interactable = false;
        btnBankerWins.interactable = false;
        btnPlayerWins.interactable = false;
        btnTie.interactable = false;
    }

    public void ActiveCardBtns()
    {
        btnBankerThird.interactable = true;
        btnPlayerThird.interactable = true;
        btnCardEach.interactable = true;
        btnBankerWins.interactable = true;
        btnPlayerWins.interactable = true;
        btnTie.interactable = true;
    }

    public IEnumerator ActiveCardButtons()
    {
        yield return new WaitForSeconds(1.5f);
        btnBankerThird.gameObject.SetActive(true);
        btnPlayerThird.gameObject.SetActive(true);
        btnNextGame.gameObject.SetActive(true);
        WinOrLose_Txt.gameObject.SetActive(true);
        btnCardEach.gameObject.SetActive(true);
        btnBankerWins.gameObject.SetActive(true);
        btnPlayerWins.gameObject.SetActive(true);
        btnTie.gameObject.SetActive(true);
        if (cardStates == "isCardTest")
        {
            btnNextGame.gameObject.SetActive(false);
        }
        else
        {
            btnNextGame.gameObject.SetActive(true);
        }

        //ActiveCardBtns();
    }

    public void CloseBtnsInNextGame()
    {
        btnBankerThird.gameObject.SetActive(false);
        btnPlayerThird.gameObject.SetActive(false);
        btnNextGame.gameObject.SetActive(false);
        WinOrLose_Txt.gameObject.SetActive(false);
        btnCardEach.gameObject.SetActive(false);
        btnBankerWins.gameObject.SetActive(false);
        btnPlayerWins.gameObject.SetActive(false);
        btnTie.gameObject.SetActive(false);
        imgCross.SetActive(false);
    }

    public void ClosePlayerThirdButton()
    {
        btnPlayerThird.interactable = false;
    }

    public void CloseBankerThirdButton()
    {
        btnBankerThird.interactable = false;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Ready()
    {
        CardsAddToList();
        CardsMatchWithSprites();
        InvokeRepeating("CreateCards", 0.5f, 0.5f);
        ProgressBar.transform.GetChild(1).GetComponent<Text>().text = cardQuestionsCounter.ToString();
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
        Wrong_Txt.text = string.Empty;
        if(isPlayerDraw != true)
        {
            DisableCardBtns();
            Wrong_Txt.text = "Player cannot Draw";
            imgCross.SetActive(true);
            WrongAndStopTimer();
        }
        if(beforePlayerTotal == 8 || beforePlayerTotal == 9)
        {
            DisableCardBtns();
            Wrong_Txt.text = "Player cannot draw in Nautral";
            WinOrLose_Txt.text = winnerName;
            imgCross.SetActive(true);
        }
        if (beforeBankerTotal == 8 || beforeBankerTotal == 9)
        {
            DisableCardBtns();
            Wrong_Txt.text = "Player cannot draw";
            WinOrLose_Txt.text = winnerName;
            imgCross.SetActive(true);
            WrongAndStopTimer();
            if (beforePlayerTotal < beforeBankerTotal)
            {
                WinnerName("Banker Wins");
                 WinOrLose_Txt.text = winnerName;
            }
        }
        if (isBankerDraw != true && isPlayerDraw != true)
        {
            WinOrLose_Txt.text = winnerName;
            return;
        }
        if (isPlayerDraw && isPlayerDrawed == true)
        {
            Debug.Log("isPlayerDraw && isPlayerDrawed == true" + " " + isPlayerDraw + "" + isPlayerDrawed);
            InstantiatePlayerThirdCard();
            //UIManager.Instance.WinOrLose_Txt.text = winnerName;
        } else if(isPlayerDraw != true && isBankerDraw == true && isBankerDrawed != false)
        {
            DisableCardBtns();
            InstantiateBankerThirdCard();
            WinOrLose_Txt.text = winnerName;
            Wrong_Txt.text = "Player cannot draw, it stands";
            imgCross.SetActive(true);
            WrongAndStopTimer();
        } else if(isPlayerDraw != true && isBankerDraw == true && isBankerDrawed != true)
        {
            DisableCardBtns();
            WinOrLose_Txt.text = winnerName;
            Wrong_Txt.text = "Player cannot draw, it stands";
            imgCross.SetActive(true);
            WrongAndStopTimer();
        } else if(isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == true)
        {
            InstantiateBankerThirdCard();
            DisableCardBtns();
            Wrong_Txt.text = "Player cannot draw again";
            WinOrLose_Txt.text = winnerName;
            imgCross.SetActive(true);
            WrongAndStopTimer();
        }
        else
        {
            WinOrLose_Txt.text = winnerName;
            DisableCardBtns();
            Debug.Log("isPlayerDraw " + isPlayerDraw + " isPlayerDrawed " + isPlayerDrawed);
            Wrong_Txt.text = "Player cannot draw";
            imgCross.SetActive(true);
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
        Wrong_Txt.text = string.Empty;

        if (beforeBankerTotal == 8 || beforeBankerTotal == 9)
        {
            DisableCardBtns();
            Wrong_Txt.text = "Banker cannot draw in Nautral";
            WinOrLose_Txt.text = winnerName;
            imgCross.SetActive(true);
            WrongAndStopTimer();
        }
        if (beforePlayerTotal == 8 || beforePlayerTotal == 9)
        {
            DisableCardBtns();
           imgCross.SetActive(true);
            Wrong_Txt.text = "Banker cannot draw";
            WrongAndStopTimer();
            if (beforePlayerTotal > beforeBankerTotal)
            {
                WinnerName("Player Wins");
                 WinOrLose_Txt.text = winnerName;
            }
        }
        if (isBankerDraw != true && isPlayerDraw != true)
        {
            DisableCardBtns();
            Wrong_Txt.text = "Banker cannot draw";
            WinOrLose_Txt.text = winnerName;
            imgCross.SetActive(true);
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
            DisableCardBtns();
            InstantiatePlayerThirdCard();
            Debug.Log("isBankerDraw : " + isBankerDraw + " isBankerDrawed " + isBankerDrawed + " isPlayerDraw " + isPlayerDraw);
           Wrong_Txt.text = "Banker cannot draw";
            WinOrLose_Txt.text = winnerName;
            Debug.Log("WRONG Banker DRAW");
            imgCross.SetActive(true);
            WrongAndStopTimer();
        } else if(isBankerDraw == true && isBankerDrawed == true && isPlayerDraw == true && isPlayerDrawed == true)
        {
            DisableCardBtns();
            InstantiatePlayerThirdCard();
            InstantiateBankerThirdCard();
            WinOrLose_Txt.text = winnerName;
            Wrong_Txt.text = "Banker cannot draw, Player needs to draw first";
            imgCross.SetActive(true);
            WrongAndStopTimer();
        } else if(isBankerDraw == false && isPlayerDraw == true && isPlayerDrawed == false)
        {
            DisableCardBtns();
            WinOrLose_Txt.text = winnerName;
            Wrong_Txt.text = "Banker cannot draw...";
            imgCross.SetActive(true);
            WrongAndStopTimer();
        }
        else if (isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == false)
        {
            DisableCardBtns();
            Wrong_Txt.text = "Banker cannot draw again";
            WinOrLose_Txt.text = winnerName;
            imgCross.SetActive(true);
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
                    imgCross.SetActive(true);
                    DisableCardBtns();
                    InstantiateBankerThirdCard();
                    WinOrLose_Txt.text = winnerName;
                    Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect xD";
                    isBankerDrawed = false;
                    WrongAndStopTimer();
                }
            } else
            {
                InstantiatePlayerThirdCard();
                InstantiateBankerThirdCard();
                isBankerDrawed = false;
                isPlayerDrawed = false;
                Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect xD";
                WinOrLose_Txt.text = winnerName;
                WrongAndStopTimer();
                return;
            }
            
        } 
        
        if(isPlayerDraw != true && isBankerDraw != true)
        {
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect";
            WinOrLose_Txt.text = winnerName;
            WrongAndStopTimer();
        } else if(isBankerDraw == true && isPlayerDraw == false)
        {
            InstantiateBankerThirdCard();
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect";
            WinOrLose_Txt.text = winnerName;
            isPlayerDrawed = false;
            WrongAndStopTimer();
        } else if(isPlayerDraw == true && isBankerDraw == false)
        {
            InstantiatePlayerThirdCard();
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "CardEach only works in BankerPoints 0 to 2 and PlayerPoints 0 to 5. Incorrect";
            WinOrLose_Txt.text = winnerName;
            WrongAndStopTimer();
        } else if(isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == false)
        {
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "You have already drawed CardEach. Incorrect.";
            WinOrLose_Txt.text = winnerName;
            WrongAndStopTimer();
        } else if (isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == true)
        {
            InstantiateBankerThirdCard();
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "You clicked Draw Player. Cannot click CardEach";
            WinOrLose_Txt.text = winnerName;
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
        //if(cardQuestionsCounter == cardQuestionsTotal)
        //{
        //    UIManager.Instance.ResultPanel.SetActive(true);
        //    return;
        //}
        ActiveCardBtns();
        WinOrLose_Txt.text = string.Empty;
        Wrong_Txt.text = string.Empty;
        foreach (Transform eachChild in cardParent)
        {
            if (eachChild.name == "RealCard(Clone)")
            {
                Destroy(eachChild.gameObject);
            }
        };
        CancelInvoke("CreateCards");
        CloseBtnsInNextGame();
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
        timer = saveTimer;
        checkCardsPracOrTest(cardStates);
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
                    StartCoroutine(ActiveCardButtons());
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
           DisableCardBtns();
            Wrong_Txt.text = "Wrong";

            if (isPlayerDraw && isPlayerDrawed)
            {
                WinOrLose_Txt.text = winnerName;
                InstantiatePlayerThirdCard();
                Wrong_Txt.text = "You are wrong, Player needs to draw 3rd card";
                imgCross.SetActive(true);
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
                WinOrLose_Txt.text = winnerName;

                InstantiateBankerThirdCard();
                Wrong_Txt.text = "You are wrong, Banker needs to draw 3rd card";
                imgCross.SetActive(true);
                Debug.Log("FUCK2");
                isBankerDrawed = false;
                WrongAndStopTimer();
            }
            if (isPlayerDraw && isPlayerDrawed == false && isBankerDraw && isBankerDrawed == false)
            {
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Incorrect";
                imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            else if (isBankerDraw != true && isPlayerDraw != true)
            {
                Debug.Log("WTF DOG");
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Incorrect";
                imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            if (isPlayerDraw == false && isBankerDraw == true && isBankerDrawed == true)
            {
                InstantiateBankerThirdCard();
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Incorrect";
                imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            if (isPlayerDraw == false && isBankerDraw == true && isBankerDrawed == false)
            {
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Incorrect";
                imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            if (isPlayerDraw == true && isBankerDraw == false && isPlayerDrawed == false)
            {
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Incorrect";
                imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            return;
        } else if(name == winnerName) 
        {
            Debug.Log("Tuu twr p" + winnerName);    
            if (isPlayerDraw && isPlayerDrawed )
            {
                DisableCardBtns();
                WinOrLose_Txt.text = winnerName;
                
                InstantiatePlayerThirdCard();
                Wrong_Txt.text = "You are wrong, Player needs to draw 3rd card";
                imgCross.SetActive(true);
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
                DisableCardBtns();
                WinOrLose_Txt.text = winnerName;
                
                InstantiateBankerThirdCard();
                Wrong_Txt.text = "You are wrong, Banker needs to draw 3rd card";
                imgCross.SetActive(false);
                Debug.Log("FUCK2");
                isBankerDrawed = false;
                WrongAndStopTimer();
                return;
            }
            if (isPlayerDraw == true && isPlayerDrawed == false && isBankerDraw== true && isBankerDrawed == true)
            {
                InstantiateBankerThirdCard();
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "You are wrong, Banker needs to draw 3rd card";
                imgCross.SetActive(false);
                WrongAndStopTimer();
            }
            if(isPlayerDraw && isPlayerDrawed == false && isBankerDraw && isBankerDrawed == false)
            {
                DisableCardBtns();
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Correct";
               
                CancelInvoke("Timer");
                StartCoroutine("NextGameAfterTimer");
            } else if(isBankerDraw != true && isPlayerDraw != true)
            {
                DisableCardBtns();
                Debug.Log("WTF DOG");
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Correct";
                
                CancelInvoke("Timer");
                StartCoroutine("NextGameAfterTimer");
            }
            if (isPlayerDraw == false && isBankerDraw == true && isBankerDrawed == true)
            {
                DisableCardBtns();
                InstantiateBankerThirdCard();
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Incorrect";
                imgCross.SetActive(true);
                WrongAndStopTimer();
            }
            if (isPlayerDraw == false && isBankerDraw == true && isBankerDrawed == false)
            {
                DisableCardBtns();
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Correct";
                //UIManager.Instance.imgCross.SetActive(true);
                
                CancelInvoke("Timer");
                StartCoroutine("NextGameAfterTimer");
            }
            if(isPlayerDraw == true && isBankerDraw == false && isPlayerDrawed == false)
            {
                DisableCardBtns();
                WinOrLose_Txt.text = winnerName;
                Wrong_Txt.text = "Correct";
                
                CancelInvoke("Timer");
                StartCoroutine("NextGameAfterTimer");
                //UIManager.Instance.imgCross.SetActive(true);
            }
        }
    }

    public void Timer()
    {
        cardTimer_Txt.gameObject.SetActive(true);
        cardTimer_Txt.text = timer.ToString();
        if (timer == 0)
        {
            Debug.Log("UP P");
            Wrong_Txt.text = "Time is Up!!!! ";
            forCardTest();
            WrongAndStopTimer();
            return;
        }
        //Debug.Log("TIMER " + timer);
        timer--;
    }


    public void checkCardsPracOrTest(string checkCardExam)
    {
        Debug.Log("CardNo Ha : " + cardQuestionsTotal);
        cardStates = checkCardExam;
       
        if (cardStates == "isCardTest")
        {   
            if (cardQuestionsCounter == cardQuestionsTotal)
            {
                Debug.Log("Pyae Twr p ha, checkCardsPracOrTest () : "+ cardQuestionsTotal);
               float a = (float)cardQuestionCorrect / cardQuestionsTotal;
                float percent = a * 100;
                
                if(percent < 90)
                {
                    FailTest.SetActive(true);
                    FailTest.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = a;
                    FailTest.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = percent.ToString() + "%";
                    ResultPanel.SetActive(false);
                } else
                {
                    ResultPanel.SetActive(true);
                    ResultPanelProgressBar.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(a, 1, 1);
                    ResultPanelProgressBar.transform.GetChild(1).GetComponent<Text>().text = cardQuestionCorrect + " questions correct in " + cardQuestionsTotal;

                    CirleResult.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = a;
                    CirleResult.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = percent.ToString() + "%";
                    FailTest.SetActive(false);
                }
                return;
            }
            cardQuestionsCounter++;
            InvokeRepeating("Timer", 1f, 1f);
        } 
        //if(cardStates == "isPractice")
        //{
        //    checkPrac();
        //}
    }

    public void checkPrac()
    {
        if(cardStates == "isPractice")
        {
            UnlockCardGameCounter++;
            ProgressBar.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3((float)UnlockCardGameCounter / (float)UnlockCardGame, 1, 1);
            ProgressBar.transform.GetChild(1).GetComponent<Text>().text = UnlockCardGame.ToString();
            ProgressBar.transform.GetChild(2).GetComponent<Text>().text = UnlockCardGameCounter + "/" + UnlockCardGame;
            if (UnlockCardGameCounter == UnlockCardGame)
            {
                SceneManager.LoadScene("SampleScene");
                UIManager.Instance.CardPracWelcome.SetActive(false);
                UIManager.Instance.CardRulePracPanel.SetActive(true);
                UnlockCardGameCounter = 0;
            }
        }
    }
    public IEnumerator NextGameAfterTimer()
    {
        if(cardStates == "isPractice")
        {
            checkPrac();
        } else if (cardStates == "isCardTest")
        {
            cardQuestionCorrect++;
            float a = (float)cardQuestionsCounter / cardQuestionsTotal;
            ProgressBar.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(a, 1, 1);
            ProgressBar.transform.GetChild(1).GetComponent<Text>().text = cardQuestionsCounter.ToString();
            ProgressBar.transform.GetChild(2).GetComponent<Text>().text = cardQuestionsCounter + "/" + cardQuestionsTotal;
            yield return new WaitForSeconds(3f);
            NextGame();
        }
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
                    imgCross.SetActive(true);
                    DisableCardBtns();
                    InstantiateBankerThirdCard();
                    WinOrLose_Txt.text = winnerName;
                    Wrong_Txt.text = "Timer is up!";
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
                Wrong_Txt.text = "Timer is up!";
                WinOrLose_Txt.text = winnerName;
                return;
            }

        }

        if (isPlayerDraw != true && isBankerDraw != true)
        {
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "Timer is up!";
            WinOrLose_Txt.text = winnerName;
        }
        else if (isBankerDraw == true && isPlayerDraw == false && isBankerDrawed == true)
        {
            InstantiateBankerThirdCard();
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "Timer is up!";
            WinOrLose_Txt.text = winnerName;
            isBankerDrawed = false;
        }
        else if (isPlayerDraw == true && isBankerDraw == false && isPlayerDrawed == true)
        {
            InstantiatePlayerThirdCard();
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "Timer is up!";
            WinOrLose_Txt.text = winnerName;
            isPlayerDrawed = false;
        }
        else if (isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == false)
        {
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "Timer is up!";
            WinOrLose_Txt.text = winnerName;
        }
        else if (isPlayerDraw == true && isBankerDraw == true && isPlayerDrawed == false && isBankerDrawed == true)
        {
            InstantiateBankerThirdCard();
            imgCross.SetActive(true);
            DisableCardBtns();
            Wrong_Txt.text = "Timer is up!";
            WinOrLose_Txt.text = winnerName;
        }
    }

    public void WrongAndStopTimer()
    {
       if(cardStates == "isCardTest")
        {
            Debug.Log("Lee tway phyit p");
            float a = (float)cardQuestionsCounter / cardQuestionsTotal;
            ProgressBar.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(a, 1, 1);
            ProgressBar.transform.GetChild(1).GetComponent<Text>().text = cardQuestionsCounter.ToString();
            ProgressBar.transform.GetChild(2).GetComponent<Text>().text = cardQuestionsCounter + "/" + cardQuestionsTotal;
            CancelInvoke("Timer");
            StartCoroutine("NextGameAfterTimerWrong");
            DisableCardBtns();
        }
    }
}
