using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private SlowMotion slowmo;
    public Slider sliderEnergy;
    public WallPusher WallPusher;
    public PlayableDirector cinematic;
    
    [Range(1,5)] public float energyMax = 3f;
    [Range(0.1f, 10f)] public float wallSpeed = 2f;
    public LevelValues[] levels;
    int currentLevel;
    int nbrLevels;
    
    public float slowmoGameOverDuration = 1.5f;
    private float slowmoGameOverTimer;
    private bool isGameOver;

    private void Awake()
    {
        instance = this;
        InitVars();
    }

    private void Start()
    {
        UpdateLevel();
        if(cinematic.state != PlayState.Playing)
            GameManager.instance.state = State.INGAME;
    }

    private void Update()
    {
        UpdateOnGameOver();
    }

    void InitVars()
    {
        nbrLevels = levels.Length;
        slowmoGameOverTimer = slowmoGameOverDuration;
        sliderEnergy.maxValue = energyMax;
        cinematic.played += OnCinematicStart;
        cinematic.stopped += OnCinematicEnd;
    }

    #region LevelComplete
    public void LevelComplete()
    {
        GameManager.instance.ChangeState(State.TRANSI);
        Time.timeScale = 1;
        
        currentLevel++;
        if (currentLevel == levels.Length)
        {
            Win();
            return;
        }
        AudioManager.instance.Play("nextLevel", currentLevel);
        UpdateLevel();
        cinematic.Play();
        StartLevel();
    }

    private void Win()
    {
        Debug.Log("Game won");
    }

    void UpdateLevel()
    {
        PlayerController.instance.slowmo.slowcoef = levels[currentLevel].slowmoCoef;
        // + Change ambiant of the level to create different ambiance per level
    }

    void OnCinematicStart(PlayableDirector cinematic)
    {
        if (this.cinematic != cinematic) return;
        
        GameManager.instance.state = State.TRANSI;
        RestartPositions();
    }
    
    void OnCinematicEnd(PlayableDirector cinematic)
    {
        if (this.cinematic != cinematic) return;

        StartLevel();
    }

    void StartLevel()
    {
        AudioManager.instance.Play("startLevel", currentLevel);
        GameManager.instance.state = State.INGAME;
        Debug.Log("New lvl started ! ");
    }

    void RestartPositions()
    {
        PlayerController.instance.ResetPos();
        WallPusher.ResetPos();
    }
    
    #endregion
    
    #region GameOver
    public void GameOver()
    {
        if(isGameOver) return;
        Debug.Log("GAME OVER");
        GameManager.instance.ChangeState(State.TRANSI);
        PlayerController.instance.slowmo.slowmode = Slowmode.IDLE;
        Time.timeScale = 1f;
        isGameOver = true;
        slowmoGameOverTimer = slowmoGameOverDuration;
        AudioManager.instance.Play("gameOver");
    }
    
    private void UpdateOnGameOver()
    {
        if (!isGameOver) return;

        slowmoGameOverTimer -= Time.unscaledDeltaTime;
        float lerp = Mathf.Clamp(slowmoGameOverTimer/slowmoGameOverDuration, 0f, 1f);
        Time.timeScale = lerp;
        PlayerController.instance.UpdateOnDeath(lerp);
        if (slowmoGameOverTimer < 0)
            GameManager.instance.ReloadScene();
    }
    #endregion

}

[Serializable]
public class LevelValues
{
    [Range(0.2f, 0.9f)] public float slowmoCoef;
    public Color ambiantColor;

    public LevelValues(){}
    public LevelValues(float slowmoCoef, Color ambiantColor)
    {
        slowmoCoef = slowmoCoef;
        ambiantColor = ambiantColor;
    }
}
