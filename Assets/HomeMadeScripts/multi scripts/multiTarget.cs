using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiTarget : MonoBehaviour {

    public int life;
    private Rigidbody r;
    private PhotonView view;
    private float  time;
    public Material[] materials;
    private Renderer rend;
    // Use this for initialization
    void Start()
    {
        r = this.GetComponent<Rigidbody>();
        view = this.GetComponent<PhotonView>();
        time = Time.time;
        rend = this.gameObject.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            if (Time.time - time > 0.6)
            {
                time = Time.time;
                StartCoroutine(invulnerabilitySpan());
                GameObject parent = other.gameObject;
                while (parent.transform.parent != null) //?
                {
                    parent = parent.transform.parent.gameObject;
                }
                Vector3 distance = parent.transform.position - this.transform.position;
                r.AddForce(new Vector3(-distance.x, 0, -distance.z) * 100000);
                view.RPC("loselife", PhotonTargets.All, view.viewID);
            }
        }
    }

    IEnumerator invulnerabilitySpan()
    {
        bool swtch = false;
        while (true)
        {
            rend.sharedMaterial = materials[1];
            if (swtch)
            {
                rend.sharedMaterial = materials[0];
                StopCoroutine(invulnerabilitySpan());

            }
           
            swtch = true;
            yield return new WaitForSeconds(0.60F);
        }
    }

    [PunRPC]
    void loselife(int id)
    {
       
        if (id == view.viewID)
        {
            life -= 10;
            if (life <= 0)
            {
                if(view.isMine)
                {
                    PhotonNetwork.Disconnect();
                    Application.LoadLevel("deathscreen");
                }
                Destroy(this.gameObject);
            }
        }
    }

}