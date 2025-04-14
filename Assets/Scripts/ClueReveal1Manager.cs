using UnityEngine;
using UnityEngine.UI;

public class ClueReveal1Manager : MonoBehaviour
{
    [Header("UI References")]
    public Button startButton;

    private void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(LoadWordle);
    }

    // Making this public so it appears in Unity's Inspector
    public void LoadWordle()
    {
        SceneTransitionManager.Instance.LoadWordle();
    }
} 