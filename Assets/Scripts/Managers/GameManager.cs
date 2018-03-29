using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameStats gameStats;
    [HideInInspector]
    public PlayerController playerController;
    [HideInInspector]
    public PlayerStats playerStats;
    [HideInInspector]
    public PlayerSkills playerSkills;
    public CanvasController canvasController;
    public SmoothFollow smoothFollow;
    [HideInInspector]
    public CoroutineHandler coroutineHandler;
   // [HideInInspector]
   // public GameObject bulletPool;


    private void Start()
    {
        instance = gameObject.GetComponent<GameManager>();
        coroutineHandler = gameObject.GetComponent<CoroutineHandler>();
        //    bulletPool = transform.Find("BulletPool").gameObject;
    }
}


public class GameStats
{
    public int kills;
    public int deaths;
    public int curXp;
    public int curHealth;

}
