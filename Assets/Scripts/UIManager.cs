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

    //CardGame
    //public Button btnBankerThird;
    //public Button btnPlayerThird;
    //public Button btnNextGame;
    //public Button btnCardEach;
    //public Button btnPlayerWins;
    //public Button btnBankerWins;
    //public Button btnTie;
    //public Text WinOrLose_Txt;
    //public Text Wrong_Txt;
    //public Text cardTimer_Txt;
    //public GameObject imgCross;
    //public Image ProgressBar;
    //public GameObject ResultPanel;
    //public Image ResultPanelProgressBar;
    //public Button BtnBackToMenu;
    //public GameObject CirleResult;

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
        

        MainMenu.SetActive(false);
        //StartCoroutine(ActiveCardButtons());
        
        CardTutorial.onClick.AddListener(ShowCardVideos);
        
       
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
    

    //public void ShowMenu()
    //{
    //    Register.SetActive(false);
    //    MainMenu.SetActive(true);
    //}
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
