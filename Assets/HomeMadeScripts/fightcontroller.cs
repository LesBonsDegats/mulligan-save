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
    public bool isBlocking;

    public string lasthit;

    public float aSpeed;
    private damageOutput damages;

    public int Stamina;
    public int maxStamina;

    public int Mana;
    public int maxMana;

    public float hit1Duration;
    public float hit2Duration;
    public float ChargedHitDuration;


    // Use this for initialization
    void Start()
    {
        aSpeed = 0.25f;
        weapon = this.GetComponentInChildren<Collider>();
        weapon.gameObject.tag = "weapon";
    }

    public void updateStats()
    {

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
        if (weaponObject != null)
        {
            damages = weaponObject.GetComponent<damageOutput>();

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

            
        }
    }
    // Update is called once per frame

    public void launchAttack(string attackType, int AspeedCoef)
    {
        hit.speed = aSpeed * AspeedCoef;
        weaponObject.tag = "weaponAttack";
        hit.SetTrigger(attackType);

        float hitDuration = 0;
        switch(attackType)
        {
            case "hit1":
                hitDuration = hit1Duration;
                isAttacking1 = true;
                break;
            case "hit2":
                hitDuration = hit2Duration;
                isAttacking2 = true;
                break;
            case "chargedHit":
                hitDuration = ChargedHitDuration;
                isAttackingCharged = true;
                break;
        }
        charge = 0;
        lasthit = attackType;
        StartCoroutine(AttackSpan(hitDuration * (1f/hit.speed), attackType));
        StartCoroutine("combo");

    }

    void Update()
    {
        isAttacking = isAttacking1 || isAttacking2 || isAttackingCharged;
        weapon.enabled = weapon.gameObject.tag == "weaponAttack" || weapon.gameObject.tag == "weaponBlock";

        if (!isAttacking && !isBlocking)
        {
            weapon.gameObject.tag = "weapon";
        }
        //blocage de coups
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
        //////////////////

        //charge hit
        if (Input.GetMouseButton(0) && !isCharging && !isAttacking && !isBlocking)
        {
            hit.speed = 1;
            hit.SetBool("charging", true);
            isCharging = true;
            StartCoroutine("ChargeAttack");

        }
        ///////////
        else if (!isAttacking)
        { 
            if (Input.GetMouseButtonUp(0))
            {
                weapon.enabled = true;
                weapon.gameObject.tag = "weaponAttack";
                hit.SetBool("charging", false);
                isCharging = false;
                StopCoroutine("ChargeAttack");
                //coup chargé
                if (charge >= 15)
                {
                    /*
                    hit.speed = aSpeed;
                    hit.SetTrigger("chargedHit");
                    isAttackingCharged = true;
                    StartCoroutine(AttackSpan(((1f / hit.speed) * ChargedHitDuration), "hitCharged"));
                    */
                    launchAttack("chargedHit", 1);

                }
                else
                {
                    /*
                    hit.SetTrigger("hit1");
                    hit.speed = aSpeed;
                    */

                    if (lasthit == "hit1")
                        launchAttack("hit2", 1);
                    else
                        launchAttack("hit1", 1);


                }
            }
            /*

            isAttacking1 = animInfo.IsName("hit1");
            isAttacking2 = animInfo.IsName("hit2");
            isAttackingCharged = animInfo.IsName("chargedHit");









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
            */
        }
    }

    IEnumerator AttackSpan (float seconds, string attackType)
    {
        bool swtch = false;
        while (true)
        {
            if (swtch)
            {
                switch (attackType) //youpi
                {
                    case ("hit1"):
                        isAttacking1 = false;
                        break;
                    case ("hit2"):
                        isAttacking2 = false;
                        break;
                    case ("chargedHit"):
                        isAttackingCharged = false;
                        break;
                }
                yield break;
            }
            swtch = true;
            yield return new WaitForSeconds(seconds);
        }
    }


    IEnumerator ChargeAttack()
    {
        while (true)
        {
            charge++;
            yield return new WaitForSeconds(0.05f);
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
                lasthit = "";
                yield break;
            }
      
            swtch = true;
            yield return new WaitForSeconds(0.8f);
        }
    }
    
}
