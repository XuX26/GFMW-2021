using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public SoundEffect[] soundEffects;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (SoundEffect sfx in soundEffects)
        {
            sfx.InitSource(gameObject);
        }
    }

    private void Start()
    {
        Play("music");
    }

    private void Update()
    {
        CheckIfMusicIsDone();
        GetSfx("music").UpdatePitchToTimeScale(0.5f);
    }

    // --- Play ---
    #region Play
    public void Play(string name)
    {
        SoundEffect sfx = GetSfx(name);

        if (sfx == null)
        {
            Debug.LogWarning("/!\\ Sound : " + name + "not found /!\\");
            return;
        }
        sfx.source.clip = sfx.clip[UnityEngine.Random.Range(0, sfx.clip.Length)];
        sfx.source.Play();
    }
    
    public void Play(string name, int clipIndex)
    {
        SoundEffect sfx = GetSfx(name);

        if (sfx == null || clipIndex >= sfx.clip.Length)
        {
            Debug.LogWarning("/!\\ Sound : " + name + "not found /!\\");
            return;
        }
        sfx.source.clip = sfx.clip[clipIndex];
        sfx.source.Play();
    }

    public void PlaySfx(SoundEffect sfx)
    {
        if (sfx == null || sfx.clip.Length == 0)
        {
            Debug.LogWarning("/!\\ Sound : " + sfx.clipName + "not found /!\\");
            return;
        }
        sfx.source.clip = sfx.clip[UnityEngine.Random.Range(0, sfx.clip.Length)];
        sfx.source.Play();
    }
    
    public void PlaySfx(SoundEffect sfx, int clipIndex)
    {
        if (sfx == null || clipIndex >= sfx.clip.Length)
        {
            Debug.LogWarning("/!\\ Sound : " + sfx.clipName + "not found /!\\");
            return;
        }
        sfx.source.clip = sfx.clip[clipIndex];
        sfx.source.Play();
    }
    #endregion

    public void Stop(string name)
    {
        SoundEffect sfx = GetSfx(name);

        if (sfx == null)
        {
            Debug.Log("/!\\ Sound : " + name + "not found /!\\");
            return;
        }
        sfx.source.Stop();
    }

    private SoundEffect GetSfx(string sfxName)
    {
        for (int i = 0; i < soundEffects.Length - 1; i++)
        {
            if (soundEffects[i].clipName == sfxName && soundEffects[i].clip.Length > 0)
                return soundEffects[i];
        }
        return null;
    }

    public float GetClipLength(string name)
    {
        SoundEffect sfx = GetSfx(name);

        if (sfx == null)
        {
            Debug.Log("/!\\ Sound : " + name + "not found /!\\");
            return 0;
        }
        return sfx.source.clip.length;
    }

    public void SpeedMusicUp(float pitchWanted)
    {
        StartCoroutine(IncreasePitch(pitchWanted));
    }

    public void SlowMusicDown(float pitchWanted)
    {
        StartCoroutine(DecreasePitch(pitchWanted));
    }

    private IEnumerator IncreasePitch(float pitchWanted)
    {
        SoundEffect sfx = GetSfx(name);
        if (sfx == null)
        {
            Debug.Log("/!\\ Sound : " + name + "not found /!\\");
        }
        if (sfx.source.pitch + 0.05f <= pitchWanted)
        {
            sfx.source.pitch += 0.05f;
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(IncreasePitch(pitchWanted));
        }
        else
        {
            sfx.source.pitch = pitchWanted;
        }
    }

    private IEnumerator DecreasePitch(float pitchWanted)
    {
        SoundEffect sfx = GetSfx(name);
        if (sfx == null)
        {
            Debug.Log("/!\\ Sound : " + name + "not found /!\\");
        }
        if (sfx.source.pitch - 0.05f >= pitchWanted)
        {
            sfx.source.pitch -= 0.05f;
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(DecreasePitch(pitchWanted));
        }
        else
        {
            sfx.source.pitch = pitchWanted;
        }
    }

    #region Music
    void CheckIfMusicIsDone()
    {
        if (!soundEffects[0].source.isPlaying && !soundEffects[1].source.isPlaying) return;

        Play("loopedMusic");
    }
    #endregion
}

[System.Serializable]
public class SoundEffect
{
    public string clipName;

    public AudioClip[] clip;

    [Range(0f, 1f)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public void InitSource(GameObject gameObject)
    {
        source = gameObject.AddComponent<AudioSource>();
        source.volume = volume;
        source.loop = loop;
    }

    public void UpdatePitchToTimeScale()
    {
        source.pitch = Time.timeScale;
    }
    public void UpdatePitchToTimeScale(float ratioAdd)
    {
        source.pitch = Time.timeScale + (1-Time.timeScale) * ratioAdd;
    }
}
