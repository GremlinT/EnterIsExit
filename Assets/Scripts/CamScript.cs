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

    [SerializeField]
    Vector3 camPreRotation;

    public bool isFollowCam;
    public bool isToEnter, isToExit;

    private void FixedUpdate()
    {
        if (isFollowCam)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, playerTR.position + camPrePosition, Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(camPreRotation), Time.deltaTime);
        }
        else
        {
            if (isToEnter) CamToEnter();
            if (isToExit) CamToExit();
        }
    }

    public void CamToEnter()
    {
        isFollowCam = false;
        isToEnter = true;
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(new Vector3(22.8f, -88.5f, 0)), Time.deltaTime);
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(-25.6f, 3.48f, 0.1f), Time.deltaTime);
    }

    public void CamToExit()
    {
        isFollowCam = false;
        isToExit = true;
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(new Vector3(22.8f, 88.5f, 0)), Time.deltaTime);
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(25.6f, 3.48f, 0.1f), Time.deltaTime);
    }
}
