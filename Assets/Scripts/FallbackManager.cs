using UnityEngine;
using UnityEngine.UI;

public class FallbackManager : MonoBehaviour
{
    [Header("UI References")]
    public Button mainMenuButton;

    private void Start()
    {
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneTransitionManager.Instance.LoadMainMenu();
    }
} 