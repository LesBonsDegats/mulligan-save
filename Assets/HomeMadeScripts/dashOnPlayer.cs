using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class dashOnPlayer : MonoBehaviour {

    public GameObject player;
    public Collider playerCollider;
    public mobAnimation mobAnim;

    public Animation test;

    private Collider mobCollider;

    public bool closeEnough;
    public bool canDash = true;
    public bool isDashing = false;
    public int speed;


    public Vector3 targetPos;
	// Use this for initialization
	void Start () {
        mobCollider = this.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {

        
        closeEnough = isCloseEnough();

        if (closeEnough && canDash && !mobAnim.Anim.IsPlaying("attack1")) 
            dash();

        if (isDashing)
        {
            
            this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
            
        }
        

     //   transform.Translate(Vector3.forward * Time.deltaTime);

        
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
            isDashing = false;
        }
    }

    public bool isCloseEnough()
    {
        float posx = player.transform.position.x;
        float posz = player.transform.position.z;

        float distance = (float)Math.Sqrt((posx - transform.position.x) * (posx - transform.position.x) + (posz - transform.position.z) * (posz - transform.position.z));
        
    return (distance < 7) && (distance > 3);
    }

    public void dash()
    {
      //  test.Play("attack1");
        targetPos = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
        StartCoroutine("dashCd");
        StartCoroutine("dashSpan");
        isDashing = true;
        mobAnim.playAnimation("run");
        canDash = false;
    }

    IEnumerator dashCd()
    {
        bool swtch = false;

        while (true)
        {

            if (swtch)
            {
                canDash = true;
                StopCoroutine("dashCd");
            }

            swtch = true;
        yield return new WaitForSeconds(3);
        }
    }

    IEnumerator dashSpan()
    {
        bool swtch = false;

        while (true)
        {

            if (swtch)
            {
                isDashing = false;
                StopCoroutine("dashSpan");
                mobAnim.playAnimation("combat_idle");
            }

            swtch = true;
            yield return new WaitForSeconds(0.5f);
        }

    }

}
