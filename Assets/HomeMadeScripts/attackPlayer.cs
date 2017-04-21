using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class attackPlayer : MonoBehaviour
{


    public GameObject Player;
    public GameObject weapon;
    public damageOutput damage;

    public int distanceMax;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


    }

    public bool getCommand()
    {
        return isCloseEnough();
    }

    public bool isCloseEnough()
    {
        float posx = Player.transform.position.x;
        float posz = Player.transform.position.z;

        float distance = (float)Math.Sqrt((posx - transform.position.x) * (posx - transform.position.x) + (posz - transform.position.z) * (posz - transform.position.z));

        return (distance < distanceMax);
    }

}

    /*

    IEnumerator OneSecUpdate()
    {
        while (true)
        {
            if (isCloseEnough() && canAttack)
            {
                attack();
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void attack()
    {
        mobAnim.playAnimation("attack1");
        weapon.tag = "weaponAttack";
         */                           