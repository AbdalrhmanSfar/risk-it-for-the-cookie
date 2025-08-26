using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;         

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject CreditsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");   
    }

    public void credits()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void BackToMenu2()
    {
        CreditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
