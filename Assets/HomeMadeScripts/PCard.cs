using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCard : MonoBehaviour {

    public GameObject floor;
    public GameObject player;
    public GameObject THIS;
    public GameObject camera;

    public GameObject PCard1;
    public GameObject PCard2;

    private GameObject NewCard;

    public int x = 0;
    public int z = 0;

    public System.Random rnd = new System.Random();

    // Use this for initialization
    void Start()
    {

        x = (int)THIS.transform.position.x;
        z = (int)THIS.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        NewBehaviourScript script = camera.GetComponent<NewBehaviourScript>();

        if (player.transform.position.x == x
            && player.transform.position.z == z)
        {

            int tirage = rnd.Next(script.PCardlist.Count);

            NewCard = script.PCardlist[tirage];
            script.PCardlist.Remove(NewCard);


            Instantiate(NewCard, new Vector3(x, 1, z), Quaternion.identity, floor.transform);
            THIS.SetActive(false);
        }

    }
}
