using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Slowmode {
    IDLE,
    ACTIVE,
    FADEIN,
    FADEOUT
}
public class SlowMotion : MonoBehaviour
{
    [Range(0.2f, 1f)]public float fadeDuration = 0.5f;
    public float slowcoef = 0.5f;
    public Slowmode slowmode = Slowmode.IDLE;
    private float fadeSpeed;
    private float energyMax;
    float energy;
    public float currentcoef;

    private void Start()
    {
        energyMax = LevelManager.instance.energyMax;
        energy = energyMax;
    }

    #region Update
    void Update()
    {
        UpdateOnIdle();
        UpdateOnSlowmo();
        energy = Mathf.Clamp(energy, 0f, energyMax);
        ComputeCoef();
        Time.timeScale = currentcoef;
        LevelManager.instance.sliderEnergy.value = energy;
    }

    private void UpdateOnIdle()
    {
        if(slowmode != Slowmode.IDLE) return;

        if (energy < energyMax)
            energy += Time.unscaledDeltaTime;
    }

    void UpdateOnSlowmo()
    {
        if(slowmode == Slowmode.IDLE) return;

        if (slowmode == Slowmode.ACTIVE)
            energy -= Time.unscaledDeltaTime;
        else
            energy -= Time.unscaledDeltaTime/3;
        
        if (energy < fadeDuration/3)
            slowmode = Slowmode.FADEOUT;
        
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    #endregion

    void ComputeCoef()
    {
        if(slowmode != Slowmode.FADEIN && slowmode != Slowmode.FADEOUT) return;
        
        FadeToTarget();
        if (currentcoef <= slowcoef)
            slowmode = Slowmode.ACTIVE;
        
        else if (currentcoef >= 1f)
        {
            slowmode = Slowmode.IDLE;
            Time.fixedDeltaTime = 0.02f;
        }
    }
  

    void FadeToTarget()
    {
        fadeSpeed = Time.unscaledDeltaTime / fadeDuration * (slowmode==Slowmode.FADEOUT? 1:-1);
        currentcoef += fadeSpeed;
        currentcoef = Mathf.Clamp(currentcoef, slowcoef, 1f);
    }
    
    public void SwitchSlowmoMode()
    {
        Debug.Log("Switch slowMode");
        if (slowmode == Slowmode.IDLE || slowmode == Slowmode.FADEOUT)
            slowmode = Slowmode.FADEIN;
        else if(slowmode == Slowmode.ACTIVE || slowmode == Slowmode.FADEIN)
            slowmode = Slowmode.FADEOUT;
    }

    public void RefreshEnergy()
    {
        energy = energyMax;
    }
}