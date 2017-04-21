using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helpButton : MonoBehaviour {

    public string toolMessage;
    public GameObject Box;
    public Text t;

    public bool isActive = true;

	// Use this for initialization
	void Start () {
        toolMessage = toolMessage.Replace("\\n", "\n");
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnMouseEnter()
    {
        if (isActive)
        SetActive(true, toolMessage);
    }

    public void SetActive(bool a, string text)
    {
        if (a)
        {
            Box.SetActive(true);
            t.text = text;
        }
        if (!a)
        {
            Box.SetActive(false);
            t.text = "";
        }
      //  t.rectTransform.position = pos + new Vector3(-0.5f, 0, 4);
       // Box.transform.position = pos + new Vector3 (-0.5f, 0, 4);
       // changeSize(allLetters(t.text));
    }

    /*
    public int[] allLetters(string str)
    {
        int[] arr = new int[27];

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] != '\n')
            {
                if (96 < str[i] && str[i] < 123)
                {
                    arr[str[i] % 97]++;
                }
                else
                {
                    arr[26]++;
                }
            }
            else
            {
                break;
            }
        }
        return arr;
    }

    public void changeSize(int[] arr) //valeurs à changer selon l'appréciation
    {
        float sum = (arr[0] * 0.09f) +
                    (arr[2] * 0.08f) +
                    (arr[3] * 0.08f) +
                    (arr[4] * 0.08f) +
                    (arr[5] * 0.08f) +
                    (arr[6] * 0.08f) +
                    (arr[7] * 0.08f) +
                    (arr[8] * 0.06f) + //i
                    (arr[9] * 0.06f) + //j
                    (arr[10] * 0.1f) + //h
                    (arr[11] * 0.08f) +
                    (arr[12] * 0.16f) + //m
                    (arr[13] * 0.08f) +
                    (arr[14] * 0.08f) +
                    (arr[15] * 0.08f) +
                    (arr[16] * 0.08f) +
                    (arr[17] * 0.08f) +
                    (arr[18] * 0.08f) +
                    (arr[19] * 0.08f) +
                    (arr[20] * 0.08f) +
                    (arr[21] * 0.08f) +
                    (arr[22] * 0.12f) + //w
                    (arr[23] * 0.1f) + //x
                    (arr[24] * 0.11f) + //y
                    (arr[25] * 0.08f) +
                    (arr[26] * 0.15f); //others


        int LineNbr = 1;
        for (int i = 0; i < t.text.Length; i++)
        {
            if (t.text[i] == '\n')
            {
                LineNbr++;
            }
        }
        Box.transform.localScale = new Vector3(sum * 0.8f, 0.2f * LineNbr, 1);
    }

    */

    public void OnMouseExit()
    {
       SetActive(false, toolMessage);
    }

}
