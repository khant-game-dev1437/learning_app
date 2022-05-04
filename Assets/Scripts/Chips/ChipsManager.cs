using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChipsManager : MonoBehaviour
{
    public static ChipsManager Instance { get; private set; }

    public GameObject FailPanel;
    public GameObject CorrectImage;
    public GameObject ChipCrossImage;
    public Image ProgressBarPrac;
    public Text ChipQuesPracCorrect;
    public Text ChipQuesPracTotal;
    public GameObject FailChipText;

    public Text ChipTestQuesCounter;
    public Text ChipTestQuesTotal;

    public string chipStates = "";
    List<int> chipsQues = new List<int>();
    private int bankerBet = 0;
    private int bankerPercent = 0;

    private int playerTotal = 0;

    private int counter5 = 4;
    private int counter25 = 3;
    private int counter100 = 4;
    private int counter500 = 1;
    private int counter1000 = 4;
    private int counter5000 = 4;
    private int TestQuesCounter = 0;
    private int PracQuesCounter = 0;
    private int Timer = 30;
    private int ChipQuesTrueCounter = 0;

    public static int chipQuesTotal;
    public static int saveChipTimer;
    public GameObject btn5;
    public GameObject btn25;
    public GameObject btn100;
    public GameObject btn500;
    public GameObject btn1000;
    public GameObject btn5000;

    public GameObject btnMainMenu;
    public GameObject ResultPanel;

    public Transform chipParent;

    private Sprite[] chipImgs;
    [SerializeField]
    private Image ChipHolder;
    [SerializeField]
    private GameObject ChipConfirm;

    public Text txt_playerTotal;
    public Text txt_Timer;
    public GameObject ChipResultPanelText;

    public GameObject ChipTestProgressBar;

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
        LoadChipsImages();

        chipQuesTotal = 2;
        saveChipTimer = Timer;

    }

    // Start is called before the first frame update
    void Start()
    {

        AddBankerBet();
        ShowChipsCounts();
        BankerBet();
        if (chipStates == "isChipTest")
        {
            btnMainMenu.SetActive(false);
            InvokeRepeating("CountTimer", 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        txt_playerTotal.text = playerTotal.ToString();
    }

    public void LoadChipsImages()
    {
        chipImgs = Resources.LoadAll<Sprite>("ChipsGraphics");


    }
    public void AddBankerBet()
    {
        chipsQues.Add(100);
        chipsQues.Add(600);
        chipsQues.Add(2300);
        chipsQues.Add(3200);
        chipsQues.Add(3700);
        chipsQues.Add(4200);
        chipsQues.Add(5000);
        chipsQues.Add(5100);
        chipsQues.Add(6100);
        chipsQues.Add(7200);
        chipsQues.Add(8200);
        chipsQues.Add(9300);
        chipsQues.Add(10300);
        chipsQues.Add(13400);
        chipsQues.Add(15600);
        chipsQues.Add(16700);
        chipsQues.Add(16800);
        chipsQues.Add(17200);
        chipsQues.Add(18700);
        chipsQues.Add(26000);
    }

    public void ShowChipsCounts()
    {
        btn5.transform.GetChild(0).GetComponent<Text>().text = counter5.ToString();
        btn25.transform.GetChild(0).GetComponent<Text>().text = counter25.ToString();
        btn100.transform.GetChild(0).GetComponent<Text>().text = counter100.ToString();
        btn500.transform.GetChild(0).GetComponent<Text>().text = counter500.ToString();
        btn1000.transform.GetChild(0).GetComponent<Text>().text = counter1000.ToString();
        btn5000.transform.GetChild(0).GetComponent<Text>().text = counter5000.ToString();
    }

    private IEnumerator moveChip(GameObject go, Vector2 startPos, Vector2 targetPos)
    {
        yield return new WaitForSeconds(1f);
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * 10;
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return new WaitForEndOfFrame();
        }
        go.GetComponent<RectTransform>().anchoredPosition = targetPos;
        Destroy(go.GetComponent<Button>());
    }
    #region 5
    public void Five()
    {
        if (counter5 <= 0)
        {
            counter5 = 0;
            return;
        }
        counter5--;
        create5Chip();
    }

    private void create5Chip()
    {
        Vector2 startPos;
        Vector2 targetPos = new Vector2(0, 0);
        GameObject go = Instantiate(btn5, chipParent);
        playerTotal += 5;
        Destroy(go.transform.GetChild(0).GetComponent<Text>());
        startPos = btn5.GetComponent<RectTransform>().anchoredPosition;
        if (counter5 == 3)
        {
            targetPos = new Vector2(897 + 40, 271);
        }
        else if (counter5 == 2)
        {
            targetPos = new Vector2(936 + 40, 221);
        }
        else if (counter5 == 1)
        {
            targetPos = new Vector2(973 + 40, 165);
        }
        else if (counter5 == 0)
        {
            targetPos = new Vector2(1016 + 40, 113);
        }
        StartCoroutine(moveChip(go, startPos, targetPos));
        btn5.transform.GetChild(0).GetComponent<Text>().text = counter5.ToString();
    }
    #endregion 5
    #region 25
    public void TwentyFive()
    {
        if (counter25 <= 0)
        {
            counter25 = 0;
            return;
        }
        counter25--;
        create25Chip();
    }

    private void create25Chip()
    {
        Vector2 startPos;
        Vector2 targetPos = new Vector2(0, 0);
        GameObject go = Instantiate(btn25, chipParent);
        playerTotal += 25;
        Destroy(go.transform.GetChild(0).GetComponent<Text>());
        startPos = btn25.GetComponent<RectTransform>().anchoredPosition;
        if (counter25 == 2)
        {
            targetPos = new Vector2(762 + 40, 221);
        }
        else if (counter25 == 1)
        {
            targetPos = new Vector2(801 + 40, 165);
        }
        else if (counter25 == 0)
        {
            targetPos = new Vector2(838 + 40, 113);
        }
        StartCoroutine(moveChip(go, startPos, targetPos));
        btn25.transform.GetChild(0).GetComponent<Text>().text = counter25.ToString();
    }
    #endregion 25
    #region 100
    public void Hundred()
    {
        if (counter100 <= 0)
        {
            counter100 = 0;
            return;
        }
        counter100--;
        create100Chip();
    }


    private void create100Chip()
    {
        Vector2 startPos;
        Vector2 targetPos = new Vector2(0, 0);
        GameObject go = Instantiate(btn100, chipParent);
        playerTotal += 100;
        Destroy(go.transform.GetChild(0).GetComponent<Text>());
        startPos = btn100.GetComponent<RectTransform>().anchoredPosition;

        if (counter100 == 3)
        {
            targetPos = new Vector2(569 + 40, 271);
        }
        else if (counter100 == 2)
        {
            targetPos = new Vector2(608 + 40, 221);
        }
        else if (counter100 == 1)
        {
            targetPos = new Vector2(648 + 40, 165);
        }
        else if (counter100 == 0)
        {
            targetPos = new Vector2(685 + 40, 113);
        }
        StartCoroutine(moveChip(go, startPos, targetPos));
        btn100.transform.GetChild(0).GetComponent<Text>().text = counter100.ToString();
    }
    #endregion 100
    #region 500
    public void FiveHundred()
    {
        if (counter500 <= 0)
        {
            counter500 = 0;
            return;
        }
        counter500--;
        create500Chip();
    }

    private void create500Chip()
    {
        Vector2 startPos;
        Vector2 targetPos = new Vector2(0, 0);
        GameObject go = Instantiate(btn500, chipParent);
        playerTotal += 500;
        Destroy(go.transform.GetChild(0).GetComponent<Text>());
        startPos = btn500.GetComponent<RectTransform>().anchoredPosition;
        if (counter500 == 0)
        {
            targetPos = new Vector2(504 + 40, 113);
        }
        StartCoroutine(moveChip(go, startPos, targetPos));
        btn500.transform.GetChild(0).GetComponent<Text>().text = counter500.ToString();
    }
    #endregion 500
    #region 1000
    public void OneThousand()
    {
        if (counter1000 <= 0)
        {
            counter1000 = 0;
            return;
        }
        counter1000--;
        create1000Chip();
    }

    private void create1000Chip()
    {
        Vector2 startPos;
        Vector2 targetPos = new Vector2(0, 0);
        GameObject go = Instantiate(btn1000, chipParent);
        playerTotal += 1000;
        Destroy(go.transform.GetChild(0).GetComponent<Text>());
        startPos = btn1000.GetComponent<RectTransform>().anchoredPosition;
        if (counter1000 == 3)
        {
            targetPos = new Vector2(207 + 40, 271);
        }
        else if (counter1000 == 2)
        {
            targetPos = new Vector2(246 + 40, 221);
        }
        else if (counter1000 == 1)
        {
            targetPos = new Vector2(283 + 40, 165);
        }
        else if (counter1000 == 0)
        {
            targetPos = new Vector2(326 + 40, 113);
        }
        StartCoroutine(moveChip(go, startPos, targetPos));
        btn1000.transform.GetChild(0).GetComponent<Text>().text = counter1000.ToString();
    }
    #endregion 1000
    #region 5000
    public void FiveThousand()
    {
        if (counter5000 <= 0)
        {
            counter5000 = 0;
            return;
        }
        counter5000--;
        create5000Chip();
    }

    private void create5000Chip()
    {
        Vector2 startPos;
        Vector2 targetPos = new Vector2(0, 0);
        GameObject go = Instantiate(btn5000, chipParent);
        playerTotal += 5000;
        Debug.Log("PlayerTotal ::    " + playerTotal);
        Destroy(go.transform.GetChild(0).GetComponent<Text>());
        startPos = btn5000.GetComponent<RectTransform>().anchoredPosition;
        if (counter5000 == 3)
        {
            targetPos = new Vector2(-2 + 40, 271);
        }
        else if (counter5000 == 2)
        {
            targetPos = new Vector2(37 + 40, 221);
        }
        else if (counter5000 == 1)
        {
            targetPos = new Vector2(74 + 40, 165);
        }
        else if (counter5000 == 0)
        {
            targetPos = new Vector2(117 + 40, 113);
        }
        StartCoroutine(moveChip(go, startPos, targetPos));
        btn5000.transform.GetChild(0).GetComponent<Text>().text = counter5000.ToString();
    }
    #endregion 5000

    public void BankerBet()
    {
        int randomNo = Random.Range(0, chipsQues.Count - 1);
        bankerBet = chipsQues[randomNo];
        bankerPercent = Mathf.RoundToInt(bankerBet * 0.95f);
        foreach (Sprite chip in chipImgs)
        {
            if (bankerBet.ToString() == chip.name)
            {
                ChipHolder.sprite = chip;
            }
        }
    }

    public IEnumerator ShowCorrectImage()
    {
        CorrectImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        CorrectImage.SetActive(false);
        StartCoroutine(NextGame());
    }

    public IEnumerator NextGame()
    {
        CancelInvoke("CountTimer");

        yield return new WaitForSeconds(2f);

        CorrectImage.SetActive(false);
        ChipCrossImage.SetActive(false);
        

        int count = chipParent.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(chipParent.transform.GetChild(i).gameObject);
        }
        counter5 = 4;
        counter25 = 3;
        counter100 = 4;
        counter500 = 1;
        counter1000 = 4;
        counter5000 = 4;
        bankerBet = 0;
        bankerPercent = 0;
        playerTotal = 0;
        Timer = 30;
        ShowChipsCounts();
        BankerBet();
        ChipConfirm.SetActive(true);
        InvokeRepeating("CountTimer", 1f, 1f);
    }

    public void checkResult()
    {
        if (chipStates == "isChipTest")
        {
            TestQuesCounter++;
            ChipTestQuesCounter.text = TestQuesCounter.ToString();
            ChipTestQuesTotal.text = chipQuesTotal.ToString();
            ChipTestProgressBar.GetComponent<Image>().fillAmount = (float)TestQuesCounter / chipQuesTotal;
            if (playerTotal == bankerPercent)
            {
                CorrectImage.SetActive(true);
                ChipQuesTrueCounter++;
                StartCoroutine(ShowCorrectImage());
            }
            else
            {
                ChipCrossImage.SetActive(true);
                StartCoroutine(NextGame());
            }

            ChipConfirm.SetActive(false);
            if (TestQuesCounter == chipQuesTotal)
            {
                float a = (float)ChipQuesTrueCounter / chipQuesTotal;

                float percent = a * 100;
                if (percent < 90)
                {
                    
                    FailPanel.SetActive(true);
                    FailChipText.GetComponent<TextMeshProUGUI>().text = percent.ToString() + "%";
                    FailPanel.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = a;
                    return;
                }
                else
                {
                    ResultPanel.SetActive(true);
                    ChipResultPanelText.GetComponent<TextMeshProUGUI>().text = percent.ToString() + "%";
                    ResultPanel.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = a;
                    //FailTest.SetActive(false);
                }
            }
        }
        else
        {
            if (playerTotal == bankerPercent)
            {
                CorrectImage.SetActive(true);
                PracQuesCounter++;
                ChipQuesPracCorrect.text = PracQuesCounter.ToString();
                ChipQuesPracTotal.text = chipQuesTotal.ToString();

                ProgressBarPrac.fillAmount = (float)PracQuesCounter / chipQuesTotal;
                if (PracQuesCounter == chipQuesTotal)
                {
                    SceneManager.LoadScene("SampleScene");
                    UIManager.Instance.ChipPracComplete.SetActive(true);
                    return;
                }
                StartCoroutine(NextGame());

            }
            else
            {
                ChipCrossImage.SetActive(true);
                StartCoroutine(NextGame());

            }
            ChipConfirm.SetActive(false);
        }
    }

    public void CountTimer()
    {
        txt_Timer.text = Timer.ToString();
        if (chipStates == "isChipTest")
        {
            if (Timer <= 0)
            {
                CancelInvoke("CountTimer");
                StartCoroutine(NextGame());
                if (chipStates == "isChipTest")
                {
                    TestQuesCounter++;
                }
            }
            Timer--;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
        UIManager.Instance.MenuCategories.SetActive(true);
        UIManager.Instance.Cards.SetActive(false);
        UIManager.Instance.Chips.SetActive(false);
        UIManager.Instance.OverAll.SetActive(false);
        UIManager.Instance.backgroundImg.SetActive(true);
    }

    public void Redo()
    {
        SceneManager.LoadScene("SampleScene");
        UIManager.Instance.ChipsTestWelcome.SetActive(true);
    }
}
