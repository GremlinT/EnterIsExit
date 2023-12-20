using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScr : MonoBehaviour
{
    [SerializeField]
    Transform playerTR;

    [SerializeField]
    GameObject deadPlayer;

    [SerializeField]
    Canvas ESCCanvas;
    public Transform pastSpavnPoint;

    [SerializeField]
    float playerSpeed, playerRotateSpeed;

    public bool isMoveForward;
    public bool isDead;

    private void Awake()
    {
        if (WorldStats.inPast)
        {
            WorldStats.LoadLevel(WorldStats.currenLevelNomber);
            playerTR.position = pastSpavnPoint.position;
            WorldStats.inPast = false;
        }
        else
        {
            WorldStats.NewLevel();
        }
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
        if (!isDead) PlayerMove();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESCCanvas.enabled = true;
        }
    }

    public void PlayerDie()
    {
        isDead = true;
        GameObject _dead = Instantiate(deadPlayer, playerTR);
        Destroy(playerTR.Find("Model").gameObject);
        this.gameObject.GetComponent<Collider>().enabled = false;
        WorldStats.nombersOfSlides = 0;
        WorldStats.levels.Clear();
        WorldStats.currenLevelNomber = 0;
        WorldStats.nomberOfDeadMonsters = 0;
        Invoke("StartNewGame", 3f);
    }

    private void StartNewGame()
    {
        SceneManager.LoadScene("Dungeon");
    }
}
