using UnityEngine;
using UnityEngine.UI;

public class CongratulationsGameManager : MonoBehaviour
{
    [Header("UI References")]
    public Button nextGameButton;

    private void Start()
    {
        if (nextGameButton != null)
            nextGameButton.onClick.AddListener(OnNextGameButtonClicked);
    }

    public void OnNextGameButtonClicked()
    {
        SceneTransitionManager.Instance.LoadClueReveal2();
    }
} 