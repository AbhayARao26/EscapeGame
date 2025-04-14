using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    // Scene names as constants to avoid typos
    public const string MAIN_MENU = "MainMenu";
    public const string CLUE_REVEAL_1 = "ClueReveal-1";
    public const string WORDLE = "Wordle";
    public const string KEY_COLLECTED = "KeyCollected";
    public const string CLUE_REVEAL_2 = "ClueReveal-2";
    public const string CONNECT_DOTS = "ConnectDots";
    public const string FALLBACK = "Fallback";
    public const string CONGRATULATIONS_GAME = "CongratulationsGame";
    public const string GAME_FINISH = "GameFinish";

    private static SceneTransitionManager _instance;
    public static SceneTransitionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneTransitionManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SceneTransitionManager");
                    _instance = go.AddComponent<SceneTransitionManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void LoadClueReveal1()
    {
        SceneManager.LoadScene(CLUE_REVEAL_1);
    }

    public void LoadWordle()
    {
        SceneManager.LoadScene(WORDLE);
    }

    public void LoadKeyCollected()
    {
        SceneManager.LoadScene(KEY_COLLECTED);
    }

    public void LoadClueReveal2()
    {
        SceneManager.LoadScene(CLUE_REVEAL_2);
    }

    public void LoadConnectDots()
    {
        SceneManager.LoadScene(CONNECT_DOTS);
    }

    public void LoadFallback()
    {
        SceneManager.LoadScene(FALLBACK);
    }

    public void LoadCongratulationsGame()
    {
        SceneManager.LoadScene(CONGRATULATIONS_GAME);
    }

    public void LoadGameFinish()
    {
        SceneManager.LoadScene(GAME_FINISH);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 