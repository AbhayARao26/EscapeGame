using UnityEngine;
using UnityEngine.SceneManagement; // Needed if changing scenes

public class StartGameScript : MonoBehaviour // Ensure it inherits from MonoBehaviour
{
    public void StartGame() // Function must be public
    {
        Debug.Log("Start Button Clicked!");
        SceneManager.LoadScene("GameScene"); // Change to your actual scene name
    }
}
