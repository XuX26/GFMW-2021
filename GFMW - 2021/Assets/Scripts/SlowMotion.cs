using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    #region Update
    void Update()
    {
        UpdateOnSlowmo();
    }

    void UpdateOnSlowmo()
    {
    }
    #endregion

    public void ActiveSlowmo(float coef)
    {
        Time.timeScale = Mathf.Clamp(coef, 0.1f, 0.9f);
    }

    public void DisableSlowmo()
    {
        Time.timeScale = 1f;
    }
}