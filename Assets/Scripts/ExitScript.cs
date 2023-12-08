using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControllerScr>())
        {
            if (WorldStats.nombersOfSlides <= 0)
            {
                if(WorldStats.currenLevelNomber > 0)
                {
                    Debug.Log("Hurray!!!");
                    WorldStats.currenLevelNomber--;
                    WorldStats.inPast = true;
                    SceneManager.LoadScene("Dungeon");
                }
                else
                {
                    Debug.Log("You win!");
                }
            }
            else
            {
                Debug.Log("No exit! only enter");
            }
            
        }
    }
}
