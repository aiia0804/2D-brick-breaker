using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level : MonoBehaviour
{
    [SerializeField] int levelUpPoints;
    int currentBreak = 0;
    SceneLoader sceneloader;
    public int currentLevel = 1;

    GameStatus gamestatus;

    private void Start()
    {
        gamestatus = FindObjectOfType<GameStatus>();
        sceneloader = FindObjectOfType<SceneLoader>();
    }

    public void BlockDestroyed()
    {
        currentBreak++;
        if (currentBreak >= levelUpPoints)
        {
            currentLevel += 1;
            gamestatus.LevelUpdate();
            FindObjectOfType<Ball>().LevelCheck();
            currentBreak = 0;
        }
    }

}
