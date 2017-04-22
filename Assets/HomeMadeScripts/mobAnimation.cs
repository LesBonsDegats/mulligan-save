using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAnimation : MonoBehaviour {

    public int monsterId;

    private Animation Anim;
    public Animator animator;
    private System.Random rnd;
    public fightcontroller player;

    public int[] Agenda;
    // 0 -> approachPlayer
    // 1 -> strafeAroundPlayer
    // 2 -> dashOnPlayer
    // 3 -> attackPlayer
    // 4 -> blockPlayer
    // 5 -> 
    public dashOnPlayer dashOnPlayer;
    public strafeAroundPlayer strafeAroundPlayer;
    public attackPlayer attackPlayer;
    public approachPlayer approachPlayer;

    public bool isDoingSomething;
    public bool isDashing;
    public bool isApproaching;
    public bool isStrafingLeft;
    public bool isStrafingRight;
    //cooldowns
    public bool canDash;
    public bool canAttack;

    public int speed; //approach speed

    public List<Coroutine> SpanList;
    public List<Coroutine> CdList;

	// Use this for initialization
	void Start () {

        SpanList = new List<Coroutine>();

        CdList = new List<Coroutine>();

        Agenda = new int[5];
        Anim = this.GetComponent<Animation>();
        canDash = true;
        canAttack = true;
        StartCoroutine(getNextAction());

	}
	
	// Update is called once per frame
	void Update () {
        if (isApproaching)
        {
            this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }
        else if (isDashing)
        {
            this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * dashOnPlayer.speed);
        }
        else if (isStrafingLeft)
        {
            this.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * strafeAroundPlayer.speed);
        }
        else if (isStrafingRight)
        {
            this.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * strafeAroundPlayer.speed);
        }
       


    }

    IEnumerator getNextAction()
    {
        while (true)
        {
            switch(monsterId)
            {
                case (0): //gobelin
                    isDoingSomething = !Anim.IsPlaying("combat_idle");
                    break;
                case (1): //squelette
                    isDoingSomething = !animator.GetCurrentAnimatorStateInfo(0).IsName("idle");
                    break;
                case (2): //araignée
                    isDoingSomething = !Anim.IsPlaying("idle");
                    break;
            }

            if (!isDoingSomething)
            {
                GetCommands();
                ExecuteBestCommand();
                Agenda = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    Agenda[i] = 0;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    public void GetCommands()
    {
        System.Random rnd = new System.Random();
        ////////////////////////////////
        if (approachPlayer != null)
        {
           // int tirage = rnd.Next(4);
           // if (tirage == 1)
           // {
                if (approachPlayer.getCommand())
                {
                    Agenda[0] = 1;
                }
                else { Agenda[0] = 0; }
          //  }
        }
        /////////////////////////////////////

        if(strafeAroundPlayer != null)
        {
            if (strafeAroundPlayer.getCommand())
            {
                Agenda[1] = 1;
            }
            else { Agenda[1] = 0; }
        }
        ////////////////////////////////////////////////////
        if (attackPlayer != null)
        {
            if (attackPlayer.getCommand() && canAttack)
            {
                Agenda[3] = 1;
            }
            else { Agenda[3] = 0; }
        }
        ////////////////////////////////////////////////////
        if (dashOnPlayer != null )
        {
            if (dashOnPlayer.getCommand() && canDash)
            {
                Agenda[2] = 1;
            }
            else { Agenda[2] = 0; }
        }
        //////////////////////////////////////////////////////

        Agenda[4] = 0;
    }
    public void ExecuteBestCommand()
    {
        System.Random rnd = new System.Random();

        //on cherche l'action la plus intéressante


        // approach action à intéret la plus basse, valeur par défaut

        // strafe AroundPlayer, en conflit avec dashOnPlayer et ayant pour but de délayer le dash (créer l'effet de surprise)
        Agenda[1] *= 2;


        // dashOnPlayer, mieux que approach, moins bon que attack
        Agenda[2] *= 2;

        //départageons Dash et Strafe

        int tirage = rnd.Next(3);
        if (tirage == 0)
            Agenda[1] = 0;
        else
            Agenda[2] = 0; // 1 chance sur 3 de Dash


        //attack, action à prioritétiser, mais en conflit avec block
        Agenda[3] *= 3;
        Agenda[4] *= 2;
        if (player.isAttacking)
            Agenda[4] *= 2;



        int index = getMaxValueIndex(Agenda);
        switch (index)
        {
            case (0):
                {
                    if (Agenda[0] != 0)
                    {
                        approach();
                        Debug.Log("youyou");
                    }
                }
                 break;
            case (1):
                strafe();
                break;
            case (2):
                dash();
                break;
            case (3):
                attack();
                break;
            case (4):
               // block();
                break;
        }
    }


    public int getMaxValueIndex(int[] arr)
    {
        int max = 0;
        int L = arr.Length;

        if (L > 1)
        {
            for (int i = 1; i < L; i++)
            {
                if (arr[i] > arr[max])
                    max = i;
            }
            return max;
        }
        else return 0;


    }

    public void playAnim(string str)
    {
        switch(monsterId)
        {
            case 0: //gobelin
                Anim.Play(str);
                break;
            case 2: //spider
                Anim.Play(str);
                break;
            case 1: //squelette
                if (str == "attack1")
                {
                    animator.SetBool("attack1", true);
                    animator.SetBool("walk", false);
                    animator.SetBool("combat_idle", false);
                }
                else if (str == "walk")
                {
                    animator.SetBool("attack1", false);
                    animator.SetBool("walk", true);
                    animator.SetBool("combat_idle", false);
                }
                else if (str == "combat_idle")
                {
                    animator.SetBool("attack1", false);
                    animator.SetBool("walk", false);
                    animator.SetBool("combat_idle", true);
                }
                break;
        }
    


    }

    IEnumerator Cooldown (float seconds, string action)
    {
        bool swtch = false;
        while (true)
        {
            if (swtch)
            {
                switch(action) // :))))))))
                {
                    case ("dashOnPlayer"):
                        canDash = true;
                        break;
                    case ("attackPlayer"):
                        canAttack = true;
                        break;
                }

                yield break;
            }
            swtch = true;
            yield return new WaitForSeconds(seconds);
        }
    }

    public void dash()
    {
        Debug.Log(monsterId);
        isDashing = true;
        canDash = false;

        StartCoroutine(Cooldown(4, "dashOnPlayer"));
        StartCoroutine(Span(Anim["run"].length, "dash"));


        playAnim("run");
    }

    public void approach()
    {
        isApproaching = true;
        if (monsterId != 0)
        {
            playAnim("walk");
        }
        if (monsterId != 1)
            StartCoroutine(Span(Anim["walk"].length, "approach"));
        else
            StartCoroutine(Span(1.04f, "approach"));
    }

    public void strafe()
    {
        int action = rnd.Next(0, 2);
        switch(action)
        {
            case 0:
                isStrafingLeft = true;
                isStrafingRight = false;
                break;
            case 1:
                isStrafingLeft = false;
                isStrafingRight = true;
                break;
            case 2:
                isStrafingLeft = false;
                isStrafingRight = false;
                break;
            default:
                break;
        }
        StartCoroutine(Span((float)rnd.NextDouble()*1.5f, "strafe"));
        playAnim("walk");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            isDashing = false;
        }
    }

    public void attack()
    {
        canAttack = false;

        StartCoroutine(Cooldown(2, "attackPlayer"));
        StartCoroutine(Span(Anim["attack1"].length, "attack"));
        attackPlayer.weapon.tag = "weaponAttack";

        playAnim("attack1");
    }

    IEnumerator Span(float seconds, string action)
    {
        bool swtch = false;
        while (true)
        {
            if (swtch)
            {
                if (Anim.GetClip("combat_idle") != null)
                {
                    playAnim("combat_idle");
                }
                else
                    playAnim("idle");

                switch(action)
                {
                    case ("dash"):
                        isDashing = false;
                        break;
                    case ("approach"):
                        isApproaching = false;
                        break;
                    case ("strafe"):
                        isStrafingLeft = false;
                        isStrafingRight = false;
                        break;
                    case ("attack"):
                        attackPlayer.weapon.tag = "weapon";
                        break;

                }

                yield break;
            }
            swtch = true;
            yield return new WaitForSeconds(seconds);
        }

    }
}
