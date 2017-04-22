using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{

    public float life;
    public float armor;
    private int maxlife;
    private Rigidbody r;

    // Use this for initialization
    void Start()
    {
        r = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "weaponAttack")
        {
            collision.collider.gameObject.tag = "weapon";


            GameObject parent = collision.gameObject;
            while (parent.transform.parent != null && GetComponent<damageOutput>() == null)
            {
                parent = parent.transform.parent.gameObject;
            }

            Vector3 distance = parent.transform.position - this.transform.position;
            damageOutput damage = parent.GetComponent<damageOutput>();
            bool isDead = takeDamage(damage);
            if (isDead)
            {
                this.gameObject.SetActive(false);
            }



            r.AddForce(new Vector3(-distance.x, 0, -distance.z) * 10000);

        }
    }

    private bool loseLife(float dmg)
    {
        life -= dmg;

        Debug.Log(this.gameObject.name + " lost " + dmg.ToString() + " health");
        return (life <= 0);
    }

    private bool takeDamage(damageOutput damages)
    {
        float totalDamage = 0;

        totalDamage += (damages.BladeDamage - (damages.BladeDamage * (armor * 2) / 100));
        totalDamage += (damages.BluntDamage - (damages.BluntDamage * (armor) / 100));
        totalDamage += damages.MagicDamage;
        totalDamage += damages.ElementalDamage + (damages.ElementalDamage * (life / 20));
        return loseLife(totalDamage);
    }
}
