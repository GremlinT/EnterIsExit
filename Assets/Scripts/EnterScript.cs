using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterScript : MonoBehaviour
{
    [SerializeField]
    CamScript cs;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Canvas tipsCanvas;
    [SerializeField]
    Text tipsText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControllerScr>())
        {
            cs.CamToExit();
            tipsCanvas.enabled = true;
            switch (WorldStats.nomberOfNextTry)
            {
                case 0:
                    tipsText.text = WorldStats.tipsNext1;
                    break;
                case 1:
                    tipsText.text = WorldStats.tipsNext2;
                    break;
                case 2:
                    tipsText.text = WorldStats.tipsNext3;
                    break;
                default:
                    tipsText.text = WorldStats.tipsNext4;
                    break;
            }
            WorldStats.nomberOfNextTry++;
            if (other.GetComponent<PlayerControllerScr>().isMoveForward)
            {
                canvas.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControllerScr>())
        {
            cs.isFollowCam = true;
            cs.isToExit = false;
            canvas.enabled = false;
            tipsCanvas.enabled = false;
        }
    }

    public void NextLevel()
    {
        WorldStats.SaveLevel();
        WorldStats.nombersOfSlides = 0;
        WorldStats.nomberOfDeadMonsters = 0;
        WorldStats.currenLevelNomber++;
        SceneManager.LoadScene("Dungeon");
    }

    public void Return()
    {
        canvas.enabled = false;
    }
}
