using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControllerScr>())
        {
            Debug.Log("Next level");
            WorldStats.SaveLevel();
            WorldStats.nombersOfSlides = 0;
            WorldStats.currenLevelNomber++;
            SceneManager.LoadScene("Dungeon");
        }
    }
}
