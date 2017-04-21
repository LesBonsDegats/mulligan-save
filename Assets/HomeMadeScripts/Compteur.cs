using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compteur : MonoBehaviour
{

    public Text T;

    private Animator Anim;
    private int memoryInt;

    // Use this for initialization
    void Start()
    {
        memoryInt = 0;
        Anim = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setInt(int x, int y = 0)
    {
        T.text = x.ToString() + " ";
        if (y != 0)
        {
            T.text += "/ " + y.ToString();
        }
        if (x != memoryInt)
        {
            memoryInt = x;
            Anim.SetTrigger("changed");
        }

    }

   
}