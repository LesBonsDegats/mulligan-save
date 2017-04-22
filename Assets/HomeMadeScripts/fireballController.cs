using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballController : MonoBehaviour
{

    private Collider explosionRadius;
    private Collider mine;
    private float timeMax;
    private GameObject fire;
    private GameObject explosion;
    public int speed;
    // Use this for initialization
    void Start()
    {
        timeMax = 10;
        mine = this.gameObject.GetComponent<Collider>();
        explosionRadius = this.gameObject.GetComponentInChildren<Collider>();
        explosion = this.gameObject.transform.FindChild("explosion").gameObject;
        fire = this.gameObject.transform.FindChild("fire").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0,0,1) * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("NTM");
        explosionRadius.enabled = true;
        explosionRadius.enabled = false;
        mine.enabled = false;
        explosion.SetActive(true);
        explosion.GetComponent<ParticleSystem>().Simulate(0,false);
        fire.SetActive(false);
    }

    IEnumerator invulnerabilitySpan()
    {
        bool swtch = false;
        while (true)
        {
            if (swtch)
            {
                Destroy(this.gameObject);
                yield break;
            }

            swtch = true;
            yield return new WaitForSeconds(timeMax);
        }
    }
}
