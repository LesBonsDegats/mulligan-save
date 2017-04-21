using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButton : MonoBehaviour {

    public GameObject cam;
    private NewBehaviourScript s;

	// Use this for initialization
	void Start () {
        s = cam.GetComponent<NewBehaviourScript>();
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskonCLick);
	}
	
	// Update is called once per frame
	void Update () {

        //s.canMove = true;
    }

    private void TaskonCLick()
    {
        s.canMove = true;
    }


}
