using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    private GameObject CardGame;
    private GameObject ChipsGame;

    public Button btn_Practice;
    public Button btn_Deal;

    Achievements achievement = new Achievements();
    // Start is called before the first frame update
    void Start()
    {
        btn_Practice.interactable = false;
        btn_Deal.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnTutorial()
    {
        achievement.cards_practice = 1;
        btn_Practice.interactable = true;
        CardGame.SetActive(true);
        UIManager.Instance.Register.SetActive(false);
        UIManager.Instance.MainMenu.SetActive(false);
    }

    public void btnDeal()
    {
        achievement.cards_deal = 1;
        btn_Deal.interactable = true;
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
