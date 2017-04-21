using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour {

    public int life;
    private Rigidbody r;

	// Use this for initialization
	void Start () {
        r = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "weaponAttack")
        {
            collision.collider.gameObject.tag = "weapon";
            bool isDead = loseLife(10);
            if (isDead)
            {
                this.gameObject.SetActive(false);
            }
            
            GameObject parent = collision.gameObject;
            while (parent.transform.parent != null) //?
            {
                parent = parent.transform.parent.gameObject;
            }

            Vector3 distance = parent.transform.position - this.transform.position;

            r.AddForce(new Vector3(-distance.x, 0, -distance.z) * 100000);

            }
        }

        private bool loseLife(int dmg)
    {
        life -= dmg;

        Debug.Log(this.gameObject.name + " lost " + dmg.ToString() + " health");
        return (life < 0);
    }
}
