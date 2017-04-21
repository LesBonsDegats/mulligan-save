using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightcontrollerMulti : MonoBehaviour
{

    public Collider weapon;
    public GameObject parent;
    public NewBehaviourScript s;

    public Animator hit;
    public int compteur = 0;
    public int charge = 0;

    public bool isAttacking = false;

    public bool isAttacking1 = false;
    public bool isAttacking2 = false;

    public bool isCharging = false;
    public bool isAttackingCharged = false;

    public bool isBlocking;

    public float aSpeed;
    public int BladeDmg;
    public int BluntDmg;
    public int MagicDmg;
    public int ElementalDmg;
    public int Armor;

    public int Stamina;
    public int maxStamina;

    public int Mana;
    public int maxMana;
    private PhotonView view;

    // Use this for initialization
    void Start()
    {
        view = parent.GetComponent<PhotonView>();
        aSpeed = 0.3f;
        weapon = this.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.isMine)
        {
            AnimatorStateInfo animInfo = hit.GetCurrentAnimatorStateInfo(0);
            isAttacking = animInfo.IsName("hit1") || animInfo.IsName("hit2") || animInfo.IsName("chargedHit");
            if (Input.GetMouseButtonDown(1) && !isAttacking)
            {
                hit.speed = 1;
                weapon.enabled = true;
                isBlocking = true;
                hit.SetBool("block", true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                weapon.enabled = false;
                isBlocking = false;
                hit.SetBool("block", false);
            }


            if (Input.GetMouseButton(0) && !isCharging)
            {
                hit.speed = 1;
                hit.SetBool("charging", true);
                isCharging = true;
                hit.SetTrigger("charge");
                StartCoroutine("ChargeAttack");

            }
            else if (Input.GetMouseButtonUp(0))
            {
                weapon.enabled = true;
                hit.SetBool("charging", false);
                isCharging = false;
                StopCoroutine("ChargeAttack");
                //coup chargé
                if (charge >= 10)
                {
                    //   hit.SetTrigger("ChargedAttack");

                    hit.speed = aSpeed * 0.9f;
                    hit.SetTrigger("hit1");
                    isAttackingCharged = true;
                    StartCoroutine(Attacktime(aSpeed));
                    StartCoroutine("combo");



                }
                else
                {

                    //coup pas chargé
                    //   hit.SetBool("test", true);
                    if (isAttacking1)
                    {
                        hit.speed = aSpeed;
                        hit.SetTrigger("hit2");
                        isAttacking1 = false;
                        isAttacking2 = true;
                        StopAllCoroutines();
                        StartCoroutine("Attacktime", aSpeed);
                    }
                    else
                    {
                        hit.speed = aSpeed;
                        hit.SetTrigger("hit1");
                        isAttacking1 = true;
                        isAttacking2 = false;
                        StartCoroutine("Attacktime", aSpeed);
                    }


                    StartCoroutine("combo");
                }


                //  isAttacking = isAttacking1 || isAttacking2 || isAttackingCharged;
                charge = 0;
            }
        }

    }


    IEnumerator Attacktime(float attackspeed)
    {
        bool swtch = false;
        bool fix = true;
        while (fix)
        {
            if (swtch)
            {
                isAttacking = false;

                fix = false;
                StopCoroutine("Attacktime");
            }

            swtch = true;

            yield return new WaitForSeconds(5 / attackspeed);
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
                isAttacking1 = false;
                isAttacking2 = false;
                isAttackingCharged = false;
                weapon.enabled = false;

                fix = false;
                StopCoroutine("combo");
            }

            swtch = true;

            yield return new WaitForSeconds(0.8f);
        }
    }
}
