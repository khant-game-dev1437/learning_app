using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CardsTutorials : MonoBehaviour
{
    public GameObject VideoTex;
    public Button NextBtn;
    public Button PrevBtn;

    private VideoClip[] cardTutos;     
    private List<string> cardVideos = new List<string>();
    
    private int VideosCounter = 0;

    private void Awake()
    {
        LoadAllCardVideos();
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log("VideoCounter " + VideosCounter);
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
        Debug.Log("VideoCounter " + VideosCounter);
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
