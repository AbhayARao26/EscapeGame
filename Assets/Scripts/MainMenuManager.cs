using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI gameTitleText;
    public Button startGameButton;
    public Button quitGameButton;
    public TextMeshProUGUI gameDescriptionText;

    private void Start()
    {
        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);
            
        if (quitGameButton != null)
            quitGameButton.onClick.AddListener(QuitGame);

        SetupUI();
    }

    private void SetupUI()
    {
        if (gameTitleText != null)
            gameTitleText.text = "ESCAPE ROOM CHALLENGE";

        if (gameDescriptionText != null)
            gameDescriptionText.text = "Can you escape all four rooms?\n\n" +
                                     "• You have 5 lives\n" +
                                     "• Each room has a unique puzzle\n" +
                                     "• Collect keys to progress\n" +
                                     "• Find clues for the next challenge";
    }

    public void StartGame()
    {
        SceneTransitionManager.Instance.LoadClueReveal1();
    }

    public void QuitGame()
    {
        SceneTransitionManager.Instance.QuitGame();
    }
} 