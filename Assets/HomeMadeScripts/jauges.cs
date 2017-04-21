using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class jauges : MonoBehaviour {

    public int init_max;
    public int attribute;
    public float init_coef_taille;
    public GameObject cam;
    public NewBehaviourScript s;
    public string type;

    public Text IntIndicator;
    // Use this for initialization
    void Start ()
    {

        s = cam.GetComponent<NewBehaviourScript>();
        switch (type)
        {
            case "life":
                init_max = s.lifemax;
                break;

            case "hunger":
                init_max = s.hungermax;
                break;

            case "moral":
                init_max = s.moralmax;
                break;


        }
        attribute = init_max;
        init_coef_taille = this.transform.localScale.z / init_max;
    

    }
	//j'ai dégagé le update qui n'était pas nécessaire
    public void update()
    {
        int t1 = attribute;


        switch (type)
        {
            case "life":
                attribute = s.life;
                init_max = s.lifemax;
                break;

            case "hunger":
                attribute = s.hunger;
                init_max = s.hungermax;
                break;

            case "moral":
                attribute = s.moral;
                init_max = s.moralmax;
                break;
        }

        int delta = attribute - t1;
        

        if (attribute< 0)
        {
            attribute = 0;
        }
        else if (attribute > init_max)
        {
            attribute = init_max;
        }
        else
        {
            float delta_taille = init_coef_taille * delta;
            this.transform.localScale += new Vector3(0, 0, delta_taille);
            this.transform.localPosition += new Vector3(-delta_taille / 2, 0, 0);
        }



    }

    private void OnMouseEnter()
    {
        IntIndicator.text = attribute.ToString() + " / " + init_max.ToString();
    }

    private void OnMouseExit()
    {
        IntIndicator.text = "";
    }
}
