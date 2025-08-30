using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.Mathematics.math;

public class LogicScript : MonoBehaviour
{
    public GameObject spawner;
    // edit the variables on this script/this object only
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
    public int score = 0;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Escape)&& !paused) { pause();  }
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
        Debug.Log("GAME OVER!");
        fallingStuffSpeed = 0;
        spawner.SetActive(false);
        alive = false;
    }

    public void pause() {
        Debug.Log("Pause Screen");
        storeSpeed = fallingStuffSpeed; fallingStuffSpeed = 0;
        spawner.SetActive(false);
        alive = false; pauseScreen.SetActive(true);
        paused = true;
    }

    public void resume() {
        Debug.Log("Resume Game");
        fallingStuffSpeed = storeSpeed;
        spawner.SetActive(true);  alive = true;
        pauseScreen.SetActive(false);
        paused = false;
    }

    public void settings() {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(true); 
    }
    public void settingBackButton() {
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true); 
    }
}
