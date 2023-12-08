using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStats
{
    public List<Vector3> levelSlidesPos = new List<Vector3>();
    public List<Quaternion> levelSlidesRot = new List<Quaternion>();
    public int nomberOfSlidesInLevel;

    public void SaveLevel()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SavedOBJ")) 
        {
            levelSlidesPos.Add(go.transform.position);
            levelSlidesRot.Add(go.transform.rotation);
        }
        nomberOfSlidesInLevel = WorldStats.nombersOfSlides;
        Debug.Log("Level saved, nomber of TR: " + levelSlidesPos.Count + ", slides " + nomberOfSlidesInLevel);
    }
}
