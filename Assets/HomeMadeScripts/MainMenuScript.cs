using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
    }


    public void Load_Solo()
    {
        Application.LoadLevel("Scene1");
	}
    public void Load_Menu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void Load_Multi()
    {
        Application.LoadLevel("2players");
    }

    public void Quit ()
    {
        Application.Quit();
	}
}
