using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject CreditsPanel;
    public GameObject LeaderBoardPanel;
    public TMP_InputField nameField;
    public GameObject nameFieldObject;
    private const string nameKey = "Name";
    private string userName;

    void Start()
    {
        userName = PlayerPrefs.GetString(nameKey, "nullName");
        if (userName == "nullName") nameFieldObject.SetActive (true);
        Debug.Log(PlayerPrefs.GetString("Name", "nullName"));
        Debug.Log(PlayerPrefs.GetInt("Highscore", 0));
    }
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
        settingsPanel.SetActive(true);
    }
    public void OpenLeaderBoard()
    {
        LeaderBoardPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        settingsPanel.SetActive(false);
    }
    public void BackToMenu2()
    {
        CreditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void BackToMenu3()
    {
        LeaderBoardPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StoreInputText()
    {
        PlayerPrefs.SetString(nameKey, nameField.text);
        userName = PlayerPrefs.GetString(nameKey, "nullName");
        if (userName != "nullName") nameFieldObject.SetActive(false);
    }
}
