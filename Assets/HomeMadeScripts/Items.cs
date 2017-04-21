using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public Sprite vide;
    public Sprite libre;

    public Sprite sword1;
    public Sprite Crossroad;
    public Sprite The_Gate;
    //etc..

    public GameObject Inventory;

    public List<Sprite> ImageList = new List<Sprite>();

    public List<string> DescriptionList = new List<string>();

	// Use this for initialization
	void Start () {
        ImageList = new List<Sprite>
        {
            vide,
            libre,
            sword1,
            Crossroad,
            The_Gate
        };

        DescriptionList = new List<string>
        {
            "aaaa",
            "bbbb",
            "Epée en Acier" 
        };
        Inventory.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
