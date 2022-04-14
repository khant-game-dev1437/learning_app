using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardsTutorials : MonoBehaviour, IDragHandler, IPointerDownHandler
{   
    [SerializeField]
    public VideoPlayer VideoTex;
    public Image progressBar;

    private VideoClip[] cardTutos;     
    private List<string> cardVideos = new List<string>();
    
    private int VideosCounter = 0;

    private void Awake()
    {
        LoadAllCardVideos();
    }

    
    void Start()
    {
        //PlayVideo();
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
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(progressBar.rectTransform, eventData.position, null,out localPoint))
        {
            float percentage = Mathf.InverseLerp(progressBar.rectTransform.rect.xMin, progressBar.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(percentage);
        }
    }

    private void SkipToPercent(float pct)
    {
        var frame = VideoTex.frameCount * pct;
        VideoTex.frame = (long)frame;
    }

    private void LoadAllCardVideos()
    {
        cardTutos = Resources.LoadAll<VideoClip>("CardTutorialsVideos");
        Debug.Log("cardTutos " + cardTutos.Length);
        cardVideos.Add("COD");
        cardVideos.Add("Circles");
    }

    public void PlayVideo()
    {
        Debug.Log("WT" + VideosCounter);
        foreach(VideoClip i in cardTutos)
        {
            if(i.name == cardVideos[VideosCounter])
            {
                VideoTex.GetComponent<VideoPlayer>().clip = i;
            }
        }
    }

    public void NextVid()
    {
        VideosCounter++;
        if (VideosCounter < 0)
        {
            VideosCounter = cardVideos.Count -1;
        }
        else if (VideosCounter > cardVideos.Count -1 )
        {
            VideosCounter = 0;
        }
        
        PlayVideo();
    }

    public void PrevVid()
    {
        VideosCounter--;
        if (VideosCounter < 0)
        {
            VideosCounter = cardVideos.Count - 1;
        }
        else if (VideosCounter > cardVideos.Count - 1)
        {
            VideosCounter = 0;
        }

        PlayVideo();
    }
}
