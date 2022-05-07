using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public GameObject backgroundImg;
    public GameObject ModuleImg;

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


    public Button ChipTutorial;
    public GameObject ChipVideos;

    public GameObject CardRulePanel;
    public GameObject CardRulePracPanel;
    public GameObject CardPracWelcome;

    public GameObject CardTestPanel;
    public Text NumberOfQues;
    public Text Timer;

    public Text ChipNumberOfQues;
    public Text ChipTimer;
    //Chips
    public GameObject ChipsPracWelcome;
    public GameObject ChipsTestWelcome;
    public GameObject ChipPracComplete;

    
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
        ChipTutorial.onClick.AddListener(ShowChipPanel);
    }

    public IEnumerator ShowCardRulePracPanel()
    {
        CardPracWelcome.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("CardPrac");
        yield return new WaitForSeconds(0.1f);
        CardPracWelcome.SetActive(false);
        ModuleImg.SetActive(false);
        //MainMenu.transform.GetChild(3).gameObject.SetActive(true);
        //CardRulePracPanel.SetActive(false);
    }

    public void HideCardTestPanel()
    {
        CardTestPanel.SetActive(false);
        CardRulePracPanel.SetActive(false);
        ShowMenu();
    }

    public void StartTestPrac()
    {
        SceneManager.LoadScene("CardTest");
        SceneManager.LoadScene("SampleScene");

        StartCoroutine(DelayTestPanel());
        
    }

    

    public IEnumerator DelayTestPanel()
    {
        yield return new WaitForSeconds(0.5f);
        CardTestPanel.SetActive(true);
        NumberOfQues.text = CardsManager.cardQuesTotal.ToString();
        Timer.text = CardsManager.saveTimer.ToString();
        ModuleImg.SetActive(false);
    }

    public void StartTest()
    {
        SceneManager.LoadScene("CardTest");
        CardTestPanel.SetActive(false);
    }
    public void StartChipTestWelcome()
    {
        SceneManager.LoadScene("ChipTest");
        ChipsTestWelcome.SetActive(false);
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
        ModuleImg.SetActive(false);
        CardsTutorials.Instance.TutoAgain();
    }

    public void ShowChipPanel()
    {
        ShowChipVideos();
        ModuleImg.SetActive(false);
        ChipsTutorial.Instance.TutoAgain();
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
        MenuCategories.SetActive(true);
        Cards.SetActive(false);
        Chips.SetActive(false);
        backgroundImg.SetActive(true);
    }

    public void ShowMenuChipPrac()
    {
        ChipPracComplete.SetActive(false);
        Register.SetActive(false);
        MainMenu.SetActive(true);
        MenuCategories.SetActive(true);
        backgroundImg.SetActive(true);
    }
    public void PracticeAgain()
    {
        SceneManager.LoadScene("CardPrac");
        CardRulePracPanel.SetActive(false);
    }

    public void ShowBtnChipTutorialMainMenu()
    {
        ChipVideos.SetActive(false);
        ShowMenu();
    }

    public void ShowMenu()
    {
        Register.SetActive(false);
        MainMenu.SetActive(true);
        MenuCategories.SetActive(true);
        Cards.SetActive(false);
        Chips.SetActive(false);
        backgroundImg.SetActive(true);
    }

    public void ShowCardVideos()
    {
        CardVideos.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void ShowChipVideos()
    {
        ChipVideos.SetActive(true);
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
        ModuleImg.SetActive(true);
        backgroundImg.SetActive(false);
    }

    public void ClickBtnChips()
    {
        Chips.SetActive(true);
        Cards.SetActive(false);
        OverAll.SetActive(false);
        MenuCategories.SetActive(false);
        ModuleImg.SetActive(true);
        backgroundImg.SetActive(false);
    }

    public void ClickBtnOverAll()
    {
        OverAll.SetActive(true);
        Cards.SetActive(false);
        Chips.SetActive(false);
        MenuCategories.SetActive(false);
    }

    public void ClickBtnChipPrac()
    {
        StartCoroutine(ShowChipPracWelcome());
        
        OverAll.SetActive(false);
        Cards.SetActive(false);
        Chips.SetActive(false);
        MenuCategories.SetActive(false);
        ModuleImg.SetActive(false);
    }

    public void ClickBtnChipTest()
    {
        SceneManager.LoadScene("ChipTest");
        OverAll.SetActive(false);
        Cards.SetActive(false);
        Chips.SetActive(false);
        MenuCategories.SetActive(false);
    }

    public IEnumerator ShowChipPracWelcome()
    {  
        ChipsPracWelcome.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ChipPrac");
        ChipsPracWelcome.SetActive(false);
    }
    //Chips
    public void StartChipTest()
    {
        SceneManager.LoadScene("ChipTest");
        SceneManager.LoadScene("SampleScene");

        StartCoroutine(DelayChipTestPanel());
    }

    public IEnumerator DelayChipTestPanel()
    {
        yield return new WaitForSeconds(0.5f);
        ChipsTestWelcome.SetActive(true);
        ChipNumberOfQues.text = ChipsManager.chipQuesTotal.ToString();
        ChipTimer.text = ChipsManager.saveChipTimer.ToString();
    }
    
    public void HideChipTestPanel()
    {
        ChipsTestWelcome.SetActive(false);
        MenuCategories.SetActive(true);
        ModuleImg.SetActive(false);
        backgroundImg.SetActive(true);
        Chips.SetActive(false);
    }
    public void ChipPracticeAgain()
    {
        SceneManager.LoadScene("ChipPrac");
        ChipPracComplete.SetActive(false);
    }

    public void BtnStartChipTest()
    { 
        StartChipTest();
        ChipPracComplete.SetActive(false);
    }
}
