using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [Range(0.2f, 0.8f)]
    public float slowcoef;
    public bool isSlowMode; 

    #region Update
    void Update()
    {
        UpdateOnSlowmo();
    }
    void UpdateOnSlowmo()
    {
    }
    #endregion

    public void SwitchSlowmoMode()
    {
        isSlowMode = !isSlowMode;
        if (isSlowMode)
            ActiveSlowmo();
        else
            DisableSlowmo();
    }
    
    public void ActiveSlowmo()
    {
        isSlowMode = true;
        Time.timeScale = slowcoef;
    }

    public void DisableSlowmo()
    {
        Time.timeScale = 1f;
        isSlowMode = false;
    }
}