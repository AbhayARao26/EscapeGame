using UnityEngine;
using UnityEngine.UI;

public class ClueReveal2Manager : MonoBehaviour
{
    [Header("UI References")]
    public Button startButton;

    private void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void OnStartButtonClicked()
    {
        SceneTransitionManager.Instance.LoadConnectDots();
    }
} 