using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScr : MonoBehaviour
{
    [SerializeField]
    Transform playerTR;

    public Transform pastSpavnPoint;

    [SerializeField]
    float playerSpeed, playerRotateSpeed;

    public bool isMoveForward;

    private void Awake()
    {
        if (WorldStats.inPast)
        {
            WorldStats.LoadLevel(WorldStats.currenLevelNomber);
            Debug.Log("Loads available " + WorldStats.levels.Count);
            playerTR.position = pastSpavnPoint.position;
            WorldStats.inPast = false;
        }
        Debug.Log("Level: " + WorldStats.currenLevelNomber + ". Nomber of slides: " + WorldStats.nombersOfSlides);
    }

    private void PlayerMove()
    {
        playerTR.position += playerTR.forward * Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        playerTR.Rotate(new Vector3(0f, Input.GetAxis("Horizontal"), 0f) * playerRotateSpeed * Time.deltaTime, Space.Self);
        if (Input.GetAxis("Vertical") > 0) isMoveForward = true;
        else isMoveForward = false;
    }

    private void Update()
    {
        PlayerMove();
    }
}
