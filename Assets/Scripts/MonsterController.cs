using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    Transform monsterTR;

    public GameObject monsterPrefab;
    
    [SerializeField]
    float monsterSpeed, mosterRotationSpeed;
        
    [SerializeField]
    public List<Transform> patrolPoints;

    [SerializeField]
    public bool isDead;

    [SerializeField]
    public bool isKilled;

    [SerializeField]
    GameObject deadGO;

    public int currentPatrolPoint;
    public bool pointsUpper = true;
    public int monsterNomber;

    private Rigidbody monsterRB;

    private int myNomber;

    /*private void Awake()
    {
        monsterRB = GetComponent<Rigidbody>();
        if (!isDead)
        {
            monsterTR.position = patrolPoints[0].position;
            currentPatrolPoint = 0;
        }
        else
        {
            

        }
    }*/

    private void Start()
    {
        monsterRB = GetComponent<Rigidbody>();
        if (!isDead)
        {
            monsterTR.position = patrolPoints[0].position;
            currentPatrolPoint = 0;
        }
        if (this.gameObject.GetComponentInChildren<ParticleSystem>())
        {
            this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
    }

    private void MonsterMove()
    {
        monsterTR.position += monsterTR.forward * monsterSpeed * Time.deltaTime;
    }

    private void MonsterPatroling()
    {
        int nextPatrolPoint;
        if (pointsUpper)
        {
            nextPatrolPoint = currentPatrolPoint + 1;
        }
        else
        {
            nextPatrolPoint = currentPatrolPoint - 1;
        }
        if (nextPatrolPoint < patrolPoints.Count && nextPatrolPoint >= 0)
        {
            monsterTR.LookAt(patrolPoints[nextPatrolPoint]);
            if ((monsterTR.position - patrolPoints[nextPatrolPoint].position).sqrMagnitude > 0.1f)
            {
                MonsterMove();
            }
            else
            {
                if (pointsUpper) currentPatrolPoint++;
                else currentPatrolPoint--;
            }
        }
        else
        {
            pointsUpper = !pointsUpper;
        }
    }

    private void Update()
    {
        if (!isDead) MonsterPatroling();
        if (isKilled) MonsterDying();
    }

    private void MonsterDying()
    {
        isKilled = false;
        GameObject _dead = Instantiate(deadGO, monsterTR);
        this.gameObject.tag = "DeadMonster";
        this.gameObject.GetComponent<Collider>().isTrigger = true;
        isDead = true;
        monsterRB.isKinematic = true;
        monsterTR.Find("Model").gameObject.SetActive(false);
        if (this.gameObject.GetComponentInChildren<ParticleSystem>())
        {
            this.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }
        WorldStats.nomberOfDeadMonsters++;
        Debug.Log("monseter die " + WorldStats.nomberOfDeadMonsters);
    }

    private void MonsterRessurection()
    {
        isDead = false;
        string deadName = deadGO.name + "(Clone)";
        Destroy(monsterTR.Find(deadName).gameObject);
        monsterRB.isKinematic = false;
        monsterTR.Find("Model").gameObject.SetActive(true);
        this.gameObject.GetComponent<Collider>().isTrigger = false;
        if (this.gameObject.GetComponentInChildren<ParticleSystem>())
        {
            this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
        WorldStats.nomberOfDeadMonsters--;
        Debug.Log("monseter res " + WorldStats.nomberOfDeadMonsters);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            if (other.transform.name == "Sword")
            {
                MonsterDying();
            }
        }
        else if (other.transform.name == "Player" && !other.GetComponent<PlayerControllerScr>().isMoveForward)
        {
            Invoke("MonsterRessurection", 1f);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerControllerScr>())
        {
            collision.gameObject.GetComponent<PlayerControllerScr>().PlayerDie();
        }

    }
    
}
