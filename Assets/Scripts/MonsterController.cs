using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    Transform monsterTR;
    
    [SerializeField]
    float monsterSpeed, mosterRotationSpeed;

    [SerializeField]
    int direction;

    [SerializeField]
    Transform[] patrolPoints;

    [SerializeField]
    bool isDead;

    [SerializeField]
    bool isKilled;

    private int currentPatrolPoint;
    private bool pointsUpper = true;

    private void Awake()
    {
        if (!isDead)
        {
            monsterTR.position = patrolPoints[0].position;
            currentPatrolPoint = 0;
        }
    }

    private void MonsterMove()
    {
        monsterTR.position += monsterTR.forward * monsterSpeed * Time.deltaTime;
    }

    private void MonsterRotation()
    {
        monsterTR.Rotate(new Vector3(0f, direction, 0f) * mosterRotationSpeed * Time.deltaTime, Space.Self);
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
        if (nextPatrolPoint < patrolPoints.Length && nextPatrolPoint >= 0)
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
        this.gameObject.tag = "DeadMonster";
        monsterTR.position += new Vector3(0f, -1f, -1f);
        monsterTR.eulerAngles = new Vector3(70f, 0f, 0f);
        this.gameObject.GetComponent<Collider>().isTrigger = true;
        isDead = true;
    }
}
