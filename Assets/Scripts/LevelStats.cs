using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelStats
{
    public List<Vector3> levelSlidesPos = new List<Vector3>();
    public List<Quaternion> levelSlidesRot = new List<Quaternion>();
    public int nomberOfSlidesInLevel;
    public int nomberOfDeadMonsterInLevel;

    public List<Vector3> levelDeadMonstersPos = new List<Vector3>();
    public List<int> levelMonstersControlPoints = new List<int>();
    public List<bool> levelMonstersMoveDirections = new List<bool>();
    public List<int> levelMonstersNomber = new List<int>();

    public void SaveLevel()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SavedOBJ")) 
        {
            levelSlidesPos.Add(go.transform.position);
            levelSlidesRot.Add(go.transform.rotation);
        }
        nomberOfSlidesInLevel = WorldStats.nombersOfSlides;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("DeadMonster"))
        {
            levelDeadMonstersPos.Add(go.transform.position);
            MonsterController _mcGO = go.GetComponent<MonsterController>();
            levelMonstersControlPoints.Add(_mcGO.currentPatrolPoint);
            levelMonstersMoveDirections.Add(_mcGO.pointsUpper);
            levelMonstersNomber.Add(_mcGO.monsterNomber);
            
        }
        nomberOfDeadMonsterInLevel = levelDeadMonstersPos.Count;
        Debug.Log("Level saved, nomber of TR: " + levelSlidesPos.Count + ", slides " + nomberOfSlidesInLevel);
        Debug.Log("And " + levelDeadMonstersPos.Count + " dead monsters");
    }
}
