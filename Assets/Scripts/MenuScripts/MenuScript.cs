using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Dungeon");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    [SerializeField]
    Canvas ESCCanvas;

    public void CloseCanvas()
    {
        ESCCanvas.enabled = false;
    }
}
