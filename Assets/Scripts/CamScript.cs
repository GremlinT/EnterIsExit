using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    [SerializeField]
    GameObject cam;

    [SerializeField]
    Transform playerTR;

    [SerializeField]
    Vector3 camPrePosition;

    private void FixedUpdate()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, playerTR.position + camPrePosition, Time.deltaTime);
    }
}
