using System;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class FxManager : MonoBehaviour
{
    public static FxManager instance;

    // -------- OnStart/Init Methods --------
    #region OnStart/Init

    void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    // -------- Play Fx ---------
    #region Play Fx

    public static GameObject PlayFx(Fx fxToPlay, Vector3 startPos)
    {
        GameObject effect = Instantiate(fxToPlay.prefab, startPos + fxToPlay.offsetSpawn, Quaternion.identity);
        if (fxToPlay.lifeTime > 0)
            Destroy(effect, fxToPlay.lifeTime);

        Debug.Log("Play effect : " + fxToPlay.prefab.name);
        return effect;
    }

    public static GameObject PlayFx(Fx fxToPlay, Transform transformTarget)
    {
        GameObject effect = Instantiate(fxToPlay.prefab, transformTarget.position + fxToPlay.offsetSpawn,
            Quaternion.identity, transformTarget);
        // effect.transform.localScale /= transformTarget.transform.localScale.x;
        if (fxToPlay.lifeTime > 0)
            Destroy(effect, fxToPlay.lifeTime);

        return effect;
    }

    #endregion

    // -------- Fx Class -------
    #region FxClass
    [Serializable]
    public class Fx
    {
        public GameObject prefab;
        public Vector3 offsetSpawn;
        public float lifeTime;

        public Fx()
        {
        }

        public Fx(GameObject fx, Vector3 offsetPos, float lifeTime)
        {
            prefab = fx;
            offsetSpawn = offsetPos;
            this.lifeTime = lifeTime;
        }
    }
    #endregion
}
