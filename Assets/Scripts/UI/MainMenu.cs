using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool isShowing = false;

    public GameObject gameSettings;
    public GameObject gameMenuLanding;
    public GameObject menuUI;
    
    public void ToggleSettings()
    {
        isShowing = !isShowing;
        gameSettings.SetActive(isShowing);
        gameMenuLanding.SetActive(!isShowing);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        menuUI.SetActive(false);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
