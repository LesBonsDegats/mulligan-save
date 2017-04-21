using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class attackPlayer : MonoBehaviour {


    public GameObject Player;
    public GameObject weapon;


    private mobAnimation mobAnim;
    private bool canAttack = true;
	// Use this for initialization
	void Start () {
        mobAnim = this.GetComponent<mobAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isCloseEnough() && canAttack)
        {
            attack();
        }
 
	}

    public void attack()
    {
        mobAnim.playAnimation("attack1");
        weapon.tag = "weaponAttack";

        canAttack = false;

        StartCoroutine("attackCd");
        StartCoroutine("attackSpan");
    }

    public bool isCloseEnough()
    {
        float posx = Player.transform.position.x;
        float posz = Player.transform.position.z;

        float distance = (float)Math.Sqrt((posx - transform.position.x) * (posx - transform.position.x) + (posz - transform.position.z) * (posz - transform.position.z));

        return (distance < 2);
    }

    IEnumerator attackCd()
    {
        bool swtch = false;

        while (true)
        {

            if (swtch)
            {
                canAttack = true;
                StopCoroutine("attackCd");
            }

            swtch = true;
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator attackSpan()
    {
        bool swtch = false;
        while (true)
        {

            if (swtch)
            {
                StopCoroutine("attackSpan");
                mobAnim.playAnimation("combat_idle");
            }

            swtch = true;
            yield return new WaitForSeconds(2f);
        }

    }
}
