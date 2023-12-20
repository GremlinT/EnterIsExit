using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class WorldStats
{
    private static GameObject slidePref = Resources.Load("Prefabs/slide") as GameObject;
    private static GameObject flyRatMonsterPref = Resources.Load("Prefabs/FlyRat") as GameObject;
    private static GameObject amebMonsterPref = Resources.Load("Prefabs/Ameb") as GameObject;

    private static List<Transform> flyRatPatrolPoints = new List<Transform>();
    private static List<Transform> amebNorthPatrolPoints = new List<Transform>();
    private static List<Transform> amebSouthPatrolPoints = new List<Transform>();

    public static int nombersOfSlides = 0;
    public static int nomberOfDeadMonsters = 0;
    
    public static List<LevelStats> levels = new List<LevelStats>();
    public static int currenLevelNomber = 0;

    public static bool inPast = false;

    public static bool wolrdPause = false;

    public static void SaveLevel()
    {
        LevelStats _level = new LevelStats();
        _level.SaveLevel();
        levels.Add( _level);
    }

    public static int nomberOfNextTry = 0;
    public static int nomberOfPrevTry = 0;
    //тексты дл€ подсказок
    public static string tipsNext1 = "“олько тот, кто смело идет вперед, сможет пройти в эту дверь!";
    public static string tipsPrev1 = "»д€ вперед невозможно вернутьс€ назад!";
    public static string tipsPrev2 = "ѕрошлое возможно только когда ничего не оставлено в будущем!";
    public static string tipsPrev3 = "Ќельз€ вернутьс€ назад, если уже наследил впереди!";
    public static string tipsPrev4 = "√оворю пр€мым текстом: пройти можно если уничтожил все следы и воскресил всех монстров!";
    public static string tipsNext2 = "Ќовые свершени€ не всегда помогу найти путь. »ногда стоит огл€нутьс€ назад.";
    public static string tipsNext3 = "ƒвига€сь назад можно исправить то, что исправить нельз€.";
    public static string tipsNext4 = "√оворю пр€мым текстом: иди спиной вперед по своим следам и тогда сможешь победить!";

    public static void LoadLevel(int levelNomber)
    {
        Debug.Log("In loaded level slides: " + levels[levelNomber].nomberOfSlidesInLevel + "and " + levels[levelNomber].levelSlidesPos.Count + " TR");
        for (int i = 0; i < levels[levelNomber].levelSlidesPos.Count; i++)
        {
            GameObject _slide = GameObject.Instantiate(slidePref, levels[levelNomber].levelSlidesPos[i], levels[levelNomber].levelSlidesRot[i]);
            _slide.GetComponent<SlideScript>().slideNomber = i;
        }
        nombersOfSlides = levels[levelNomber].nomberOfSlidesInLevel;
        
        Debug.Log("In loaded level is " + levels[levelNomber].levelDeadMonstersPos.Count  + " dead monster");
        //так делать нельз€!!!
        GameObject flyRat = GameObject.Instantiate(flyRatMonsterPref);
        foreach (GameObject flyRatPatrolPoint in GameObject.FindGameObjectsWithTag("flyRatPatrolPoints"))
        {
            flyRatPatrolPoints.Add(flyRatPatrolPoint.transform);
        }
        flyRat.GetComponent<MonsterController>().patrolPoints.AddRange(flyRatPatrolPoints);
        flyRat.GetComponent<MonsterController>().monsterNomber = 1;
        GameObject amerNorth = GameObject.Instantiate(amebMonsterPref);
        foreach (GameObject amebPatrolPoint in GameObject.FindGameObjectsWithTag("amebNorthPatrolPoints"))
        {
            amebNorthPatrolPoints.Add(amebPatrolPoint.transform);
        }
        amerNorth.GetComponent<MonsterController>().patrolPoints.AddRange(amebNorthPatrolPoints);
        amerNorth.GetComponent<MonsterController>().monsterNomber = 2;
        GameObject amerSouth = GameObject.Instantiate(amebMonsterPref);
        foreach (GameObject amebPatrolPoint in GameObject.FindGameObjectsWithTag("amebSouthPatrolPoints"))
        {
            amebSouthPatrolPoints.Add(amebPatrolPoint.transform);
        }
        amerSouth.GetComponent<MonsterController>().patrolPoints.AddRange(amebSouthPatrolPoints);
        amerSouth.GetComponent<MonsterController>().monsterNomber = 3;
        flyRatPatrolPoints.Clear();
        amebNorthPatrolPoints.Clear();
        amebSouthPatrolPoints.Clear();
        //так наверное можно
        for (int i = 0; i < levels[levelNomber].levelDeadMonstersPos.Count; i++)
        {
            MonsterController _mc;
            switch (levels[levelNomber].levelMonstersNomber[i])
            {
                case 1:
                    _mc = flyRat.GetComponent<MonsterController>();
                    _mc.isDead = true;
                    _mc.isKilled = true;
                    _mc.currentPatrolPoint = levels[levelNomber].levelMonstersControlPoints[i];
                    _mc.pointsUpper = levels[levelNomber].levelMonstersMoveDirections[i];
                    flyRat.transform.position = levels[levelNomber].levelDeadMonstersPos[i];
                    break;
                case 2:
                    _mc = amerNorth.GetComponent<MonsterController>();
                    _mc.isDead = true;
                    _mc.isKilled = true;
                    _mc.currentPatrolPoint = levels[levelNomber].levelMonstersControlPoints[i];
                    _mc.pointsUpper = levels[levelNomber].levelMonstersMoveDirections[i];
                    amerNorth.transform.position = levels[levelNomber].levelDeadMonstersPos[i];
                    break;
                case 3:
                    _mc = amerSouth.GetComponent<MonsterController>();
                    _mc.isDead = true;
                    _mc.isKilled = true;
                    _mc.currentPatrolPoint = levels[levelNomber].levelMonstersControlPoints[i];
                    _mc.pointsUpper = levels[levelNomber].levelMonstersMoveDirections[i];
                    amerSouth.transform.position = levels[levelNomber].levelDeadMonstersPos[i];
                    break;
                default:
                    break;
            }
        }
        Debug.Log(nomberOfDeadMonsters);
    }
    public static void NewLevel()
    {
        GameObject flyRat = GameObject.Instantiate(flyRatMonsterPref);
        foreach (GameObject flyRatPatrolPoint in GameObject.FindGameObjectsWithTag("flyRatPatrolPoints"))
        {
            flyRatPatrolPoints.Add(flyRatPatrolPoint.transform);
        }
        flyRat.GetComponent<MonsterController>().patrolPoints.AddRange(flyRatPatrolPoints);
        flyRat.GetComponent<MonsterController>().monsterNomber = 1;
        GameObject amerNorth = GameObject.Instantiate(amebMonsterPref);
        foreach (GameObject amebPatrolPoint in GameObject.FindGameObjectsWithTag("amebNorthPatrolPoints"))
        {
            amebNorthPatrolPoints.Add(amebPatrolPoint.transform);
        }
        amerNorth.GetComponent<MonsterController>().patrolPoints.AddRange(amebNorthPatrolPoints);
        amerNorth.GetComponent<MonsterController>().monsterNomber = 2;
        GameObject amerSouth = GameObject.Instantiate(amebMonsterPref);
        foreach (GameObject amebPatrolPoint in GameObject.FindGameObjectsWithTag("amebSouthPatrolPoints"))
        {
            amebSouthPatrolPoints.Add(amebPatrolPoint.transform);
        }
        amerSouth.GetComponent<MonsterController>().patrolPoints.AddRange(amebSouthPatrolPoints);
        amerSouth.GetComponent<MonsterController>().monsterNomber = 3;
        flyRatPatrolPoints.Clear();
        amebNorthPatrolPoints.Clear();
        amebSouthPatrolPoints.Clear();
    }
}
