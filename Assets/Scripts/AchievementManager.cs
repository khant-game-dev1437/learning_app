using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }
    [SerializeField]
    public GameObject CardGame;
    public GameObject ChipsGame;

    public Button btn_CardPractice;
    public Button btn_CardDeal;

    Achievements achievement = new Achievements();
    
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
        //btn_Practice.interactable = false;
        //btn_Deal.interactable = false;
        //btn_CardPractice.onClick.AddListener(Hi);
        //btn_CardPractice.onClick.AddListener(btnCardPractice);
        //btn_CardDeal.onClick.AddListener(btnCardPractice);
        btn_CardPractice.onClick.AddListener(delegate { SceneManager.LoadScene("CardPrac"); });
        btn_CardDeal.onClick.AddListener(delegate { SceneManager.LoadScene("CardTest"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void btnCardPractice()
    {
        
        //achievement.cards_practice = 1;
        btn_CardPractice.interactable = true;
        CardGame.SetActive(true);
        UIManager.Instance.Register.SetActive(false);
        UIManager.Instance.MainMenu.SetActive(false);
    }

    public void btnDeal()
    {
        achievement.cards_deal = 1;
        btn_CardDeal.interactable = true;
    }
}

public class Achievements
{
    public int cards_practice = 0;
    public int cards_deal = 0;

    public int chips_practice = 0;
    public int chips_deal = 0;

    public int overall_practice = 0;
    public int overall_deal = 0;
}
