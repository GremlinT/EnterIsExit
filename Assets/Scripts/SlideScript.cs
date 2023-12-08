using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlideScript : MonoBehaviour
{
    public int slideNomber;
    private void Awake()
    {
        slideNomber = WorldStats.nombersOfSlides;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerControllerScr>())
        {
            if (!other.gameObject.GetComponent<PlayerControllerScr>().isMoveForward &&
                Vector3.Angle(other.gameObject.transform.forward, this.transform.forward)<25 &&
                slideNomber == WorldStats.nombersOfSlides - 1)
            {
                DestroySlide();
                WorldStats.nombersOfSlides--;
            }
        }
    }

    private void DestroySlide()
    {
        Destroy(this.gameObject);
    }
}
