using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }
    
    // Register GameObject
    public GameObject Register;
    public InputField Name_Input;
    public Button Name_Btn;
    public Button BtnTutorialMainMenu;

    public Button btn_CardPractice;
    public Button btn_CardDeal;

    public Text Name_Txt;
    
    // MainMenu GameObject
    public GameObject MainMenu;

    // Cards, Chips, Overall GameObject
    public GameObject Cards;
    public GameObject Chips;
    public GameObject OverAll;
    public GameObject MenuCategories;

    
    public Button CardTutorial;
    public GameObject CardVideos;

    public GameObject CardRulePanel;
    public GameObject CardRulePracPanel;
    public GameObject CardPracWelcome;

    public GameObject CardTestPanel;
    public Text NumberOfQues;
    public Text Timer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        btn_CardPractice.onClick.AddListener(delegate { StartCoroutine(ShowCardRulePracPanel()); });
        btn_CardDeal.onClick.AddListener(delegate { StartTestPrac(); });

        MainMenu.SetActive(false);
        //StartCoroutine(ActiveCardButtons());
        CardTutorial.onClick.AddListener(delegate { StartCoroutine(ShowCardRulePanel()); });
    }

    public IEnumerator ShowCardRulePracPanel()
    {
        CardPracWelcome.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("CardPrac");
        yield return new WaitForSeconds(0.1f);
        CardPracWelcome.SetActive(false);

        //MainMenu.transform.GetChild(3).gameObject.SetActive(true);
        //CardRulePracPanel.SetActive(false);
    }

    public void HideCardTestPanel()
    {
        CardTestPanel.SetActive(false);
        ShowMenu();
    }

    public void StartTestPrac()
    {
        SceneManager.LoadScene("CardTest");
        Debug.Log("Lee");
        SceneManager.LoadScene("SampleScene");

        StartCoroutine(DelayTestPanel());
    }

    public IEnumerator DelayTestPanel()
    {
        yield return new WaitForSeconds(0.5f);
        CardTestPanel.SetActive(true);
        NumberOfQues.text = CardsManager.cardQuesTotal.ToString();
        Timer.text = CardsManager.saveTimer.ToString();
    }

    public void StartTest()
    {
        SceneManager.LoadScene("CardTest");
        CardTestPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowBtnTutorialMainMenu()
    {
        CardVideos.SetActive(false);
        ShowMenu();
    }

    public IEnumerator ShowCardRulePanel()
    {
        CardRulePanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        CardRulePanel.SetActive(false);
        ShowCardVideos();
    }
    public void CloseAllUI()
    {
        Register.SetActive(false);
        MainMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenuCardPrac()
    {
        CardRulePracPanel.SetActive(false);
        Register.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void PracticeAgain()
    {
        SceneManager.LoadScene("CardPrac");
        CardRulePracPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        Register.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void ShowCardVideos()
    {
        CardVideos.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void NameBtn()
    {
        Name_Txt.text = Name_Input.text.ToString();
        Register.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ClickBtnCards()
    {
        Cards.SetActive(true);
        Chips.SetActive(false);
        OverAll.SetActive(false);
        MenuCategories.SetActive(false);
    }

    public void ClickBtnChips()
    {
        Chips.SetActive(true);
        Cards.SetActive(false);
        OverAll.SetActive(false);
        MenuCategories.SetActive(false);
    }

    public void ClickBtnOverAll()
    {
        OverAll.SetActive(true);
        Cards.SetActive(false);
        Chips.SetActive(false);
        MenuCategories.SetActive(false);
    }

    //public void CloseBankerThirdButton()
    //{
    //    btnBankerThird.interactable = false;
    //}

    //public void ClosePlayerThirdButton()
    //{
    //    btnPlayerThird.interactable = false;
    //}

    //public void CloseBtnsInNextGame()
    //{
    //    btnBankerThird.gameObject.SetActive(false);
    //    btnPlayerThird.gameObject.SetActive(false);
    //    btnNextGame.gameObject.SetActive(false);
    //    WinOrLose_Txt.gameObject.SetActive(false);
    //    btnCardEach.gameObject.SetActive(false);
    //    btnBankerWins.gameObject.SetActive(false);
    //    btnPlayerWins.gameObject.SetActive(false);
    //    btnTie.gameObject.SetActive(false);
    //    imgCross.SetActive(false);
    //}
    //public IEnumerator ActiveCardButtons()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    btnBankerThird.gameObject.SetActive(true);
    //    btnPlayerThird.gameObject.SetActive(true);
    //    btnNextGame.gameObject.SetActive(true);
    //    WinOrLose_Txt.gameObject.SetActive(true);
    //    btnCardEach.gameObject.SetActive(true);
    //    btnBankerWins.gameObject.SetActive(true);
    //    btnPlayerWins.gameObject.SetActive(true);
    //    btnTie.gameObject.SetActive(true);
    //    if (CardsManager.Instance.isStates == "isCardTest")
    //    {
    //        btnNextGame.gameObject.SetActive(false);
    //    } else
    //    {
    //        btnNextGame.gameObject.SetActive(true);
    //    }

    //    //ActiveCardBtns();
    //}

    //public void DisableCardBtns()
    //{
    //    btnBankerThird.interactable = false;
    //    btnPlayerThird.interactable = false;
    //    btnCardEach.interactable = false;
    //    btnBankerWins.interactable = false;
    //    btnPlayerWins.interactable = false;
    //    btnTie.interactable = false;
    //}

    //public void ActiveCardBtns()
    //{
    //    btnBankerThird.interactable = true;
    //    btnPlayerThird.interactable = true;
    //    btnCardEach.interactable = true;
    //    btnBankerWins.interactable = true;
    //    btnPlayerWins.interactable = true;
    //    btnTie.interactable = true;
    //}
}
