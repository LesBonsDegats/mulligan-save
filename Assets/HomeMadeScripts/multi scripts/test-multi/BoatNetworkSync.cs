using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatNetworkSync : MonoBehaviour {

    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;
    private PhotonView view;
    public Animator animator;
    void Start()
    {
        view = GetComponent<PhotonView>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!view.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
         
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(animator.GetBool("hit1"));
            stream.SendNext(animator.GetBool("hit2"));
            stream.SendNext(animator.GetBool("hit3"));
            stream.SendNext(animator.GetBool("block"));
            stream.SendNext(animator.GetBool("charging"));
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

        }
        else
        {
            // Network player, receive data
            if ((bool)stream.ReceiveNext())
            {
                animator.SetTrigger("hit1");
            }
            if ((bool)stream.ReceiveNext())
            {
                animator.SetTrigger("hit2");
            }
            if ((bool)stream.ReceiveNext())
            {
                animator.SetTrigger("hit3");
            }
            animator.SetBool("block", (bool)stream.ReceiveNext());
            animator.SetBool("charging", (bool)stream.ReceiveNext());
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
