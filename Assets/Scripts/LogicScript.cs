using Dan.Main;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.Mathematics.math;

public class LogicScript : MonoBehaviour
{
    public GameObject spawner;
    private const string highScoreKey = "Highscore";
    private const string nameKey = "Name";
    // edit the variables on this script/this object only
    public string userName;
    public float gainedMalwareDamage = 20;
    public float gainedCookieHealth = 10;
    public float gainedEnergyCharge = 10;
    public float maxHealth = 100;
    private float health; // made private because we want to edit it exclusively from the functions
    public float maxEnergy = 100;
    private float energy; // made private because we want to edit it exclusively from the functions
    public float energyNeededforDash;
    public float healthLerpSpeed;
    public float healthLerpThreshold;
    public float energyLerpSpeed;
    public float energyLerpThreshold;
    public int score = 0, highScore;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        userName = PlayerPrefs.GetString(nameKey, "nullName");
        health = maxHealth;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
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
        health = min(100f, health);
        if (health == 0)
        {
            GameOver();
        }
    }

    public void RecoverHealth(float amount)
    {
        health = min(100f, health + amount);
        health = max(0f, health);
    }

    public void DrainEnergy(float amount)
    {
        energy = min(100f, energy - amount);
        energy = max(0f, energy);
    }

    public void ChargeEnergy(float amount)
    {
        energy = min(100f, energy + amount);
        energy = max(0f, energy);
    }

    public void GameOver()
    {
        highScore = max(score, highScore);
        PlayerPrefs.SetInt(highScoreKey, highScore);
        Debug.Log("GAME OVER!");
        fallingStuffSpeed = 0;
        spawner.SetActive(false);
        alive = false;
        Leaderboards.Cookie.UploadNewEntry(userName, highScore);
    }

}
