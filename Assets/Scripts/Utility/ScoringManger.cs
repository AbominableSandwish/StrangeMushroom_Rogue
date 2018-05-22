using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringManger : MonoBehaviour {
    private int Level = 0;
    private float speed_boost_enemy = 0;
    [SerializeField] private int Slimes = 5;

    [SerializeField] private float level_up_speed_boost_add = 0.05f;
    [SerializeField] private int level_up_Slimes_add = 2;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
	
    public float GetSpeedBoost()
    {
        return speed_boost_enemy;
    }

    public int GetSlimes()
    {
        return Slimes;
    }

    public void Level_Up()
    {
        Level++;
        speed_boost_enemy += level_up_speed_boost_add;
        Slimes += level_up_Slimes_add;
    }

    public int GetLevel()
    {
        return Level;
    }
}
