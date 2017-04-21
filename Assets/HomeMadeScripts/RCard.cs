using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCard : MonoBehaviour {

    public GameObject floor;
    public GameObject player;
    public GameObject THIS;
    public GameObject camera;

    public GameObject RCard1;
    public GameObject RCard2;

    public System.Random rnd = new System.Random();

    private GameObject NewCard;

    public int x = 0;
    public int z = 0;

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

            int tirage = rnd.Next(script.RCardlist.Count);

            NewCard = script.RCardlist[tirage];
            script.RCardlist.Remove(NewCard);

            Instantiate(NewCard, new Vector3(x, 1, z), Quaternion.identity, floor.transform);
            THIS.SetActive(false);
        }

    }
}
