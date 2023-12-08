using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public static class WorldStats
{
    private static GameObject slidePref = Resources.Load("Prefabs/slide") as GameObject;
    
    public static int nombersOfSlides = 0;
    public static List<LevelStats> levels = new List<LevelStats>();
    public static int currenLevelNomber = 0;

    public static bool inPast = false;

    public static void SaveLevel()
    {
        LevelStats _level = new LevelStats();
        _level.SaveLevel();
        levels.Add( _level);
    }

    public static void LoadLevel(int levelNomber)
    {
        Debug.Log("In loaded level slides: " + levels[levelNomber].nomberOfSlidesInLevel + "and " + levels[levelNomber].levelSlidesPos.Count + " TR");
        for (int i = 0; i < levels[levelNomber].levelSlidesPos.Count; i++)
        {
            GameObject _slide = GameObject.Instantiate(slidePref, levels[levelNomber].levelSlidesPos[i], levels[levelNomber].levelSlidesRot[i]);
            _slide.GetComponent<SlideScript>().slideNomber = i;
        }
        nombersOfSlides = levels[levelNomber].nomberOfSlidesInLevel;
    }
}
