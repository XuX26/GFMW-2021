using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private SlowMotion slowmo;

    private bool isGameOver;
    public float slowmoGameOverDuration = 1.5f;
    private float slowmoGameOverTimer;
    
    int levelCount;
    int nbrLevels;
    [Range(0.2f, 1f)] public float[] coefPerLevel;

    private void Awake()
    {
        // if (instance) {
        //     Destroy(gameObject);
        //     return;
        // }
        instance = this;
        InitVars();
    }

    private void Update()
    {
        UpdateOnGameOver();
    }

    void InitVars()
    {
        nbrLevels = coefPerLevel.Length;
        slowmoGameOverTimer = slowmoGameOverDuration;
    }

    #region LevelComplete
    public void LevelComplete()
    {
        GameManager.instance.ChangeState(State.TRANSI);
        Time.timeScale = 1;
        
        levelCount++;
        if (levelCount == coefPerLevel.Length)
        {
            Win();
            return;
        }
        UpdateLevel();
        ResetPlayerAndWallPos();
    }

    private void Win()
    {
        Debug.Log("Game won");
    }

    void UpdateLevel()
    {
        PlayerController.instance.slowmo.slowcoef = coefPerLevel[levelCount];
        // + Change ambiant of the level to create different ambiance per level
    }

    void ResetPlayerAndWallPos()
    {
        PlayerController.instance.StartRun();
    }
    #endregion
    
    #region GameOver
    public void GameOver()
    {
        if(isGameOver) return;
        Debug.Log("GAME OVER");
        GameManager.instance.ChangeState(State.TRANSI);
        isGameOver = true;
        slowmoGameOverTimer = slowmoGameOverDuration;
    }
    
    private void UpdateOnGameOver()
    {
        if (!isGameOver) return;

        Debug.Log(slowmoGameOverTimer);
        slowmoGameOverTimer -= Time.unscaledDeltaTime;
        Debug.Log(slowmoGameOverTimer);
        float lerp = Mathf.Clamp(slowmoGameOverTimer/slowmoGameOverDuration, 0f, 1f);
        Time.timeScale = lerp;
        PlayerController.instance.UpdateOnDeath(lerp);
        if (slowmoGameOverTimer < 0)
            GameManager.instance.ReloadScene();
    }
    #endregion

}
