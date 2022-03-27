using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstantiate : MonoBehaviour
{
    public static CardInstantiate Instance { get; private set; }
    [SerializeField]
    private GameObject card;
    public Transform cardParent;
    public int counter = 0;

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
        InvokeRepeating("createCards", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createCards()
    {
        if(counter == 5)
        {
            return;
        }
        GameObject go = Instantiate(card, cardParent);
        go.transform.localPosition = new Vector2(0, -50);
        counter++;
    }
}
