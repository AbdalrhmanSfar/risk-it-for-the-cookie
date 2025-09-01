using Dan.Main;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static Unity.Mathematics.math;

public class LogicScript : MonoBehaviour
{
    public GameObject spawner;
    [SerializeField] private AudioSource musicSource;

    // edit the variables on this script/this object only
    public float gainedMalwareDamage;
    public float gainedCookieHealth;
    public float gainedEnergyCharge;
    public float maxHealth;
    private float health; // made private because we want to edit it exclusively from the functions
    public float maxEnergy;
    private float energy; // made private because we want to edit it exclusively from the functions
    public float energyNeededforDash;
    public float healthLerpSpeed;
    public float healthLerpThreshold;
    public float energyLerpSpeed;
    public float energyLerpThreshold;
    public int score = 0;
    public int Highscore;
    private string userName;
    public float fallingStuffSpeed;
    public bool gameStarted = false;
    private float startTimer = 0;
    public float startDuration; // edit start animation's length too if you edit this shi
    public float hurtDuration; // edit hurt animation's length too if you edit this shi
    public bool isHurt = false;
    public bool ifDash = false;
    public bool alive = true;
    public bool isJumping = false;
    public bool isWalking = false;
    public float storeSpeed; // to freeze stuff when the game is paused 
    public GameObject pauseScreen;
    private bool paused = false;
    public GameObject settingsScreen;
    public GameObject gameOverScreen;
    public GameObject quitConfirmScreen;
    public bool destroyAllMalware = false;
    public float stopDestroying = 0;

    public float chocoAntiVirusDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Highscore = PlayerPrefs.GetInt("Highscore");
        userName = PlayerPrefs.GetString("Name");
        health = maxHealth;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        musicSource.pitch = 1 + 0.001f * score;
        if (!gameStarted)
        {
            if (startTimer >= startDuration)
            {
                startTimer = 0;
                gameStarted = true;
            }
            else
                startTimer += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused) { Pause(); }
            else { Resume(); }
        }

        if (destroyAllMalware)
        {
            stopDestroying += Time.deltaTime;
            if (stopDestroying > chocoAntiVirusDuration) { destroyAllMalware = false; stopDestroying = 0; }
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetEnergy()
    {
        return energy;
    }
    public void TakeDamage(float amount)
    {
        health = max(0f, health - amount);
        health = min(maxHealth, health);
        if (health == 0)
        {
            GameOver();
        }
    }

    public void RecoverHealth(float amount)
    {
        health = min(maxHealth, health + amount);
        health = max(0f, health);
    }

    public void DrainEnergy(float amount)
    {
        energy = min(maxEnergy, energy - amount);
        energy = max(0f, energy);
    }

    public void ChargeEnergy(float amount)
    {
        energy = min(maxEnergy, energy + amount);
        energy = max(0f, energy);
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");
        SFXScript.instance.deathSFX();
        Highscore = max(Highscore, score);
        Leaderboards.Cookie.UploadNewEntry(userName, Highscore);
        fallingStuffSpeed = 0;
        spawner.SetActive(false);
        alive = false;
        gameOverScreen.SetActive(true);
    }

    public void Pause()
    {
        Debug.Log("Pause Screen");
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        Debug.Log("Resume Game");
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Restart()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene("GameScene");
    }

    public void BackToMenu()
    {
        Highscore = max(Highscore, score);
        Leaderboards.Cookie.UploadNewEntry(userName, Highscore);
        Time.timeScale = 1f; // unpause, in case paused
        Debug.Log("Back to menu");
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Settings()
    {
        settingsScreen.SetActive(true);
    }
    public void SettingsBackButton()
    {
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }
    public void ShowQuitConfirm()
    {
        quitConfirmScreen.SetActive(true);
    }
    public void QuitCancelButton()
    {
        quitConfirmScreen.SetActive(false);
    }
}
