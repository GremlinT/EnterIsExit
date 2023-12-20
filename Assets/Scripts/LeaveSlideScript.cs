using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeaveSlideScript : MonoBehaviour
{
    [SerializeField]
    GameObject slide;

    [SerializeField]
    float slideTimer;
    private float currentSlideTimer;

    [SerializeField]
    Transform playerTR;
    [SerializeField]
    PlayerControllerScr playerScr;

    private void CreateSlide(Vector3 slidePos)
    {
        GameObject _slide = Instantiate(slide, slidePos, playerTR.rotation);
        WorldStats.nombersOfSlides++;
    }

    private void FixedUpdate()
    {
        currentSlideTimer -= Time.deltaTime;
        if (currentSlideTimer < 0 && playerScr.isMoveForward && !playerScr.isDead)
        {
            CreateSlide(playerTR.position);
            currentSlideTimer = slideTimer;
        }
    }


}
