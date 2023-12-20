using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour
{
    [SerializeField]
    CamScript cs;

    [SerializeField]
    Canvas tipsCanvas;
    [SerializeField]
    Text tipsText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControllerScr>())
        {
            cs.CamToEnter();
            if (WorldStats.nombersOfSlides <= 0 && WorldStats.nomberOfDeadMonsters <= 0)
            {
                if(WorldStats.currenLevelNomber > 0)
                {
                    WorldStats.currenLevelNomber--;
                    WorldStats.inPast = true;
                    SceneManager.LoadScene("Dungeon");
                }
                else
                {
                    tipsCanvas.enabled = true;
                    tipsText.text = "ÒÛ ÂÛÐÂÀËÑß!!!";
                    Invoke("EndGame", 2f);
                }
            }
            else
            {
                tipsCanvas.enabled = true;
                switch (WorldStats.nomberOfPrevTry)
                {
                    case 0:
                        tipsText.text = WorldStats.tipsPrev1;
                        break;
                    case 1:
                        tipsText.text = WorldStats.tipsPrev2;
                        break;
                    case 2:
                        tipsText.text = WorldStats.tipsPrev3;
                        break;
                    default:
                        tipsText.text = WorldStats.tipsPrev4;
                        break;
                }
                WorldStats.nomberOfPrevTry++;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControllerScr>())
        {
            cs.isFollowCam = true;
            cs.isToEnter = false;
            tipsCanvas.enabled = false;
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene("WinScene");
    }
}
