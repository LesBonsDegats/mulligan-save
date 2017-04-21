using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuObject;
    public bool isActive = false;

	void Update ()
    {
       
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume_button();
            if (isActive == true)
            {
                menuObject.SetActive(true);
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            else
            {
                menuObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
	}
    public void Resume_button()
    {
        isActive = !isActive;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Load_Menu()
    {
        Application.LoadLevel("MainMenu");
    } 
}
