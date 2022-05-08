using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChipsTutorial : MonoBehaviour, IDragHandler, IPointerDownHandler
{

    public static ChipsTutorial Instance { get; private set; }

    [SerializeField]
    public VideoPlayer VideoTex;
    public Image progressBar;

    [SerializeField]
    private GameObject FinishedPanel;

    private VideoClip[] chipTutos;
    private List<string> chipVideos = new List<string>();

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
        LoadAllChipVideos();
    }


    void Start()
    {

        VideosTotal.text = chipVideos.Count.ToString();
        CounterVideos.text = (VideosCounter + 1).ToString();
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

    private void LoadAllChipVideos()
    {
        chipTutos = Resources.LoadAll<VideoClip>("ChipTutorial");
        chipVideos.Add("1");
        chipVideos.Add("2");
        chipVideos.Add("3");
        chipVideos.Add("4");
        chipVideos.Add("5");
        chipVideos.Add("6");
        chipVideos.Add("7");
        chipVideos.Add("8");
        chipVideos.Add("9");
        chipVideos.Add("10");
        chipVideos.Add("11");
        chipVideos.Add("12");
    }

    public void PlayVideo()
    {
        Debug.Log("WT" + VideosCounter);
        foreach (VideoClip i in chipTutos)
        {
            if (i.name == chipVideos[VideosCounter])
            {
                VideoTex.GetComponent<VideoPlayer>().clip = i;
            }
        }
    }

    public void NextVid()
    {
        VideosCounter++;
        if (VideosCounter > chipVideos.Count - 1)
        {
            VideosCounter = chipVideos.Count - 1;
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
        if (VideosCounter <= 0)
        {
            VideosCounter = 0;
        }
        CounterVideos.text = (VideosCounter + 1).ToString();
        PlayVideo();
    }

    public void StartChip()
    {
        SceneManager.LoadScene("ChipPrac");
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

        VideosTotal.text = chipVideos.Count.ToString();
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
