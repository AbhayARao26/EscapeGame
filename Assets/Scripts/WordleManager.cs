using UnityEngine;

public class WordleManager : MonoBehaviour
{
    private bool isGameCompleted = false;

    public void OnGameCompleted(bool success)
    {
        isGameCompleted = success;
        if (success)
        {
            SceneTransitionManager.Instance.LoadCongratulationsGame();
        }
        else
        {
            SceneTransitionManager.Instance.LoadFallback();
        }
    }
} 