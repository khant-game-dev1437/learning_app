using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardsTutorials : MonoBehaviour, IDragHandler, IPointerDownHandler
{
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
        cardVideos.Add("CardTutorial1");
        cardVideos.Add("CardTutorial2");
        cardVideos.Add("CardTutorial3");
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
