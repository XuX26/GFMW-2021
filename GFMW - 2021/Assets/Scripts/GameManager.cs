using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State {
    PAUSE,
    INGAME,
    TRANSI,
}

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public State state = State.INGAME;
    public System.Action onStateChange;

    private void Awake()
    {
        if (instance) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        ChangeState(State.INGAME);
    }

    // Update is called once per frame
    private void Update()
    {
        // if (Input.GetKeyUp(KeyCode.Escape) && state != State.PAUSE) {
        //     ChangeState(State.PAUSE);
        // } else if (Input.GetKeyUp(KeyCode.Escape) && state == State.PAUSE) {
        //     ChangeState(State.INGAME);
        // }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ReloadScene();
        }
    }

    public void ChangeState(State newState)
    {
        state = newState;
        switch (state)
        {
            case State.PAUSE:
                Cursor.visible = true;
                break;
            case State.INGAME:
                Cursor.visible = false;
                break;
            case State.TRANSI:
                Cursor.visible = false;
                break;
        }
        onStateChange?.Invoke();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}