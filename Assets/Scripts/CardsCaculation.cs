using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsCaculation : MonoBehaviour
{
    List<float> playerCards = new List<float>();
    List<float> bankerCards = new List<float>();

    bool Player = false;
    int counter ;
    Vector2 startPos;
    Vector2 targetPos =new Vector2(0,0);
    private RectTransform m_rect;

    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
        startPos = m_rect.anchoredPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        counter = CardInstantiate.Instance.counter;
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     
    private IEnumerator Animate()
    {
        switch(counter)
        {
            case 0:
                targetPos = new Vector2(1000, 550);
                break;
            case 1:
                targetPos = new Vector2(500, 550);
                break;
            case 2:
                targetPos = new Vector2(1100, 550);
                break;

            case 3:
                targetPos = new Vector2(400, 550);
                break;
            case 4:
                Player = true;
                targetPos = new Vector2(1221, 540);
                break;

            case 5:
                Player = false;
                targetPos = new Vector2(278, 540);
                break;
            default:
                break;
        }
        
        yield return new WaitForSeconds(2f);
        float t = 0;
       
        while (t < 1f)
        {
            t += Time.deltaTime;
            m_rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return new WaitForEndOfFrame();
        }
        m_rect.anchoredPosition = targetPos;
        StartCoroutine(RotateCard());
    }

    private IEnumerator RotateCard()
    {
        float t = 0;
        float z = 0f;
        if(Player && counter == 4)
        {
            //Debug.Log("COUNTER 4");
            while(t<1f)
            {
                z = -90;
                t += Time.deltaTime;
                m_rect.transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 0, z), t);
                yield return new WaitForEndOfFrame();
            }
            
        } else if (!Player && counter == 5)
        {
            //Debug.Log("COUNTER 5");
            while(t<1f)
            {
                z = 90;
                t += Time.deltaTime;
                m_rect.transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 0, z), t);
                yield return new WaitForEndOfFrame();
            }
        } else
        {
            while (t < 1f)
            {
                t += Time.deltaTime;
                m_rect.transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 180, 0), t);
                yield return new WaitForEndOfFrame();
            }
        }
        
        m_rect.transform.rotation = Quaternion.Euler(0, 180, z);
    }

    
}
