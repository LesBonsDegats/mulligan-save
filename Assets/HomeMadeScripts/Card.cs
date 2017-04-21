using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public Sprite image1;
    public Sprite image2;
    public Sprite image3;

    private List<Sprite> SpriteList = new List<Sprite>();
    


    public GameObject player;
    public GameObject showCard;
    public SpriteRenderer sr;

    private int x;
    private int z;
    private int cardId;
    private bool isDone;



    public Card(int posx, int posz, int cardid)
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        showCard = GameObject.FindGameObjectsWithTag("ShowCard")[0];
        sr = showCard.GetComponent<SpriteRenderer>();
        SpriteList = new List<Sprite> { image1, image2, image3 };

        x = posx;
        z = posz;
        cardId = cardid;
    }

    public void DispUpdate()
    {
        sr.sprite = SpriteList[cardId];
    }

    private void OnMouseEnter()
    {
        sr.sprite = SpriteList[cardId];
    }

    private void OnMouseExit()
    {
        sr.sprite = null;
    }

}

