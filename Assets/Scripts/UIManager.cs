using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    [SerializeField]
    // Register GameObject
    public GameObject Register;
    public InputField Name_Input;
    public Button Name_Btn;
    public Button btnBankerThird;
    public Button btnPlayerThird;
    public Button btnNextGame;
    public Button btnCardEach;

    public Button btnPlayerWins;
    public Button btnBankerWins;
    public Button btnTie;

    public Text Name_Txt;
    public Text WinOrLose_Txt;
    public Text Wrong_Txt;
    // MainMenu GameObject
    public GameObject MainMenu;

    // Cards, Chips, Overall GameObject
    public GameObject Cards;
    public GameObject Chips;
    public GameObject OverAll;
    public GameObject MenuCategories;

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
        btnBankerThird.gameObject.SetActive(false);
        btnPlayerThird.gameObject.SetActive(false);
        btnCardEach.gameObject.SetActive(false);
        btnNextGame.gameObject.SetActive(false);
        btnBankerWins.gameObject.SetActive(false);
        btnPlayerWins.gameObject.SetActive(false);
        btnTie.gameObject.SetActive(false);

        MainMenu.SetActive(false);
        //StartCoroutine(ActiveCardButtons());
        btnBankerWins.onClick.AddListener(delegate { CardsManager.Instance.CheckTrueOrFalseWinner(btnBankerWins.gameObject.transform.GetChild(0).GetComponent<Text>().text); });
        btnPlayerWins.onClick.AddListener(delegate { CardsManager.Instance.CheckTrueOrFalseWinner(btnPlayerWins.gameObject.transform.GetChild(0).GetComponent<Text>().text); });
        btnTie.onClick.AddListener(delegate { CardsManager.Instance.CheckTrueOrFalseWinner(btnTie.gameObject.transform.GetChild(0).GetComponent<Text>().text); });
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void CloseBankerThirdButton()
    {
        btnBankerThird.interactable = false;
    }

    public void ClosePlayerThirdButton()
    {
        btnPlayerThird.interactable = false;
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
    }
}
