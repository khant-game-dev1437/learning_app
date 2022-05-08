using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardsTutorials : MonoBehaviour, IDragHandler, IPointerDownHandler
{

    public static CardsTutorials Instance { get; private set; }
    [SerializeField]
    public VideoPlayer VideoTex;
    public Image progressBar;

    [SerializeField]
    private GameObject FinishedPanel;

    private VideoClip[] cardTutos;
    private List<string> cardVideos = new List<string>();

    private int VideosCounter = 0;

    [SerializeField]
    private GameObject MenuCategories;

    public GameObject BtnPlay;
    public GameObject BtnPause;
    public GameObject ParentPlayOrPuase;

    public Text VideosTotal;
    public Text CounterVideos;
    public Text Slash;

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
        LoadAllCardVideos();
    }


    void Start()
    {
        
        VideosTotal.text = cardVideos.Count.ToString();
        CounterVideos.text = (VideosCounter+1).ToString();
    }


    void Update()
    {
        if (VideoTex.frameCount > 0)
        {
            progressBar.fillAmount = (float)VideoTex.frame / (float)VideoTex.frameCount;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        SkipVideo(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SkipVideo(eventData);
    }

    private void SkipVideo(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(progressBar.rectTransform, eventData.position, null, out localPoint))
        {
            float percentage = Mathf.InverseLerp(progressBar.rectTransform.rect.xMin, progressBar.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(percentage);
        }
    }

    public void SkipToPercent(float pct)
    {
        var frame = VideoTex.frameCount * pct;
        VideoTex.frame = (long)frame;
    }

    private void LoadAllCardVideos()
    {
        cardTutos = Resources.LoadAll<VideoClip>("CardTutorialsVideos");
        cardVideos.Add("Card1");
        cardVideos.Add("Card2");
        cardVideos.Add("Card3");
        cardVideos.Add("Card4");
        cardVideos.Add("Card5");
        cardVideos.Add("Card6");
        cardVideos.Add("Card7");
        cardVideos.Add("Card8");
        cardVideos.Add("Card9");
        cardVideos.Add("Card10");
        cardVideos.Add("Card11");
        cardVideos.Add("Card12");
        cardVideos.Add("Card13");
        cardVideos.Add("Card14");
        cardVideos.Add("Card15");
        cardVideos.Add("Card16");
        cardVideos.Add("Card17");
        cardVideos.Add("Card18");
        cardVideos.Add("Card19");
        cardVideos.Add("Card20");
        cardVideos.Add("Card21");
        cardVideos.Add("Card22");
        cardVideos.Add("Card23");
        cardVideos.Add("Card24");
        cardVideos.Add("Card25");
        cardVideos.Add("Card26");
        cardVideos.Add("Card27");
        cardVideos.Add("Card28");
        cardVideos.Add("Card29");
        cardVideos.Add("Card30");
        cardVideos.Add("Card31");

    }

    public void PlayVideo()
    {
        Debug.Log("WT" + VideosCounter);
        foreach (VideoClip i in cardTutos)
        {
            if (i.name == cardVideos[VideosCounter])
            {
                VideoTex.GetComponent<VideoPlayer>().clip = i;
            }
        }
    }

    public void NextVid()
    {
        VideosCounter++;
        if (VideosCounter > cardVideos.Count - 1)
        {
            VideosCounter = cardVideos.Count - 1;
            VideoTex.GetComponent<VideoPlayer>().Pause();
            FinishedPanel.SetActive(true);
            ParentPlayOrPuase.SetActive(false);
            CounterVideos.gameObject.SetActive(false);
            VideosTotal.gameObject.SetActive(false);
            Slash.gameObject.SetActive(false);
        }
        CounterVideos.text = (VideosCounter + 1).ToString();
        PlayVideo();
    }

    public void PrevVid()
    {
        VideosCounter--;
        if(VideosCounter <= 0)
        {
            VideosCounter = 0;
        }
        CounterVideos.text = (VideosCounter + 1).ToString();
        PlayVideo();
    }

    public void StartPrac()
    {
        SceneManager.LoadScene("CardPrac");
        FinishedPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ShowMainMenu()
    {
        FinishedPanel.SetActive(false);
        UIManager.Instance.MainMenu.SetActive(true);
        MenuCategories.SetActive(true);
        gameObject.SetActive(false);
        UIManager.Instance.MenuCategories.SetActive(true);
        UIManager.Instance.Cards.SetActive(false);
        UIManager.Instance.Chips.SetActive(false);
        UIManager.Instance.backgroundImg.SetActive(true);
    }

    public void TutoAgain()
    {
        VideosCounter = 0;
        PlayVideo();
        CounterVideos.text = (VideosCounter + 1).ToString();
        UIManager.Instance.MainMenu.SetActive(false);
        FinishedPanel.SetActive(false);
        SkipToPercent(0f);
        VideoTex.Play();
        ParentPlayOrPuase.SetActive(true);
        
        CounterVideos.gameObject.SetActive(true);
        Slash.gameObject.SetActive(true);
        VideosTotal.gameObject.SetActive(true);
        
        VideosTotal.text = cardVideos.Count.ToString();
    }

    public void UnpauseVideo()
    {
        BtnPlay.SetActive(false);
        BtnPause.SetActive(true);
        VideoTex.Play();
    }

    public void PauseVideo()
    {
        BtnPlay.SetActive(true);
        BtnPause.SetActive(false);
        VideoTex.Pause();
    }
}
