using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightcontroller : MonoBehaviour
{

    public GameObject camjoueur;
    public GameObject weaponObject;
    public Collider weapon;
    public Transform weaponSpot;
    public Target body;
    public NewBehaviourScript s;


    //armes 
    public GameObject epee1;
    public GameObject epee2;
    public GameObject epee3;

    public GameObject dague;

    public GameObject katana;
    //

    public Animator hit;
    public int compteur = 0;
    public int charge = 0;

    public bool isAttacking = false;

    public bool isAttacking1 = false;
    public bool isAttacking2 = false;

    public bool isCharging = false;
    public bool isAttackingCharged = false;

    public string lastHit;

    public bool isBlocking;

    public float aSpeed;
    public damageOutput damages;

    public int Stamina;
    public int maxStamina;

    public int Mana;
    public int maxMana;


    // Use this for initialization
    void Start()
    {
        aSpeed = 0.25f;
        weapon = this.GetComponentInChildren<Collider>();
        weapon.gameObject.tag = "weapon";
    }

    public void updateStats()
    {

        aSpeed = s.aspeed;
        damages.BladeDamage = s.BladeDmg;
        damages.BluntDamage = s.BluntDmg;
        damages.MagicDamage = s.MagicalDmg;
        damages.ElementalDamage = s.ElementalDmg;

        body.armor = s.armor;

        maxMana = s.mana;
        Mana = maxMana;

        maxStamina = s.stamina;
        Stamina = maxStamina;

        body.life = s.life;

        GameObject newWeapon = null;

        switch (s.idArme)
        {
            case 2:
                newWeapon = epee1;
                break;
        }
        switch (s.idArme2)
        {

        }
        switch (s.idTalisman1)
        {

        }
        switch (s.idTalisman2)
        {

        }
        switch (s.idTalisman3)
        {

        }

        //replaceWeapon
        Destroy(weaponObject);
        weaponObject = Instantiate(newWeapon, weaponSpot.position, weaponSpot.rotation, camjoueur.transform);
        weapon = weaponObject.GetComponent<Collider>();
        hit = weaponObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animInfo = hit.GetCurrentAnimatorStateInfo(0);
        isAttacking1 = animInfo.IsName("hit1");
        isAttacking2 = animInfo.IsName("hit2");
        isAttackingCharged = animInfo.IsName("chargedHit");
        isAttacking = isAttacking1 || isAttacking2 || isAttackingCharged;
        weapon.enabled = weapon.gameObject.tag == "weaponAttack" || weapon.gameObject.tag == "weaponBlock";
        if (!isAttacking && !isBlocking)
        {
            weapon.gameObject.tag = "weapon";
        }

        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            weapon.gameObject.tag = "weaponBlock";
            isBlocking = true;
            hit.speed = 1;
            hit.SetBool("block", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isBlocking = false;
            hit.SetBool("block", false);
            weapon.gameObject.tag = "weapon";
        }


        if (Input.GetMouseButton(0) && !isCharging && !isAttacking && !isBlocking)
        {
            hit.speed = 1;
            hit.SetBool("charging", true);
            isCharging = true;
            StartCoroutine("ChargeAttack");

        }
        else if (Input.GetMouseButtonUp(0) && !isAttacking)
        {
            weapon.enabled = true;
            weapon.gameObject.tag = "weaponAttack";
            hit.SetBool("charging", false);
            isCharging = false;
            StopCoroutine("ChargeAttack");
            //coup chargé
            if (charge >= 10)
            {
                //   hit.SetTrigger("ChargedAttack");

                hit.speed = aSpeed * 0.9f;
                hit.SetTrigger("hit3");
                StartCoroutine("combo");
            }
            else
            {

                //coup pas chargé
                //   hit.SetBool("test", true);
                if (lastHit == "hit1")
                {
                    hit.speed = aSpeed;
                    hit.SetTrigger("hit2");
                    StopAllCoroutines();
                    lastHit = "hit2";
                }
                else
                {
                    hit.speed = aSpeed;
                    hit.SetTrigger("hit1");
                    lastHit = "hit1";
                }


                StartCoroutine("combo");
            }


            //  isAttacking = isAttacking1 || isAttacking2 || isAttackingCharged;
            charge = 0;
        }
    }


    IEnumerator ChargeAttack()
    {
        while (true)
        {
            charge++;
            yield return new WaitForSeconds(0.07f);
        }
    }



    IEnumerator combo()
    {

        bool swtch = false;
        bool fix = true;
        while (fix)
        {
            if (swtch)
            {
                lastHit = null;
                StopCoroutine("combo");
            }

            swtch = true;

            yield return new WaitForSeconds(0.8f);
        }
    }
}
