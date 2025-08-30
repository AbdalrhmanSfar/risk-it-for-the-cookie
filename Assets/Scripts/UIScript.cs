using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject logicManager;
    private LogicScript logic;

    public Sprite[] healthBarFullness;
    public Image healthWifiBar;

    public Slider energySlider;
    public Slider easeEnergySlider;
    // edits to these from LogicScript only
    private float health; // edits from LogicScript only

    private float energy; // edits from LogicScript only
    private float energyLerpSpeed; // lerpSpeed must be in range [0,1], if 0.5 Mathf.Lerp() returns halfway point, etc
    private float energyLerpThreshold;


    public GameObject scoreText;
    private int score;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = logicManager.GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // healthbar ------
        health = logic.GetHealth();
        if (health == 0)
            healthWifiBar.GetComponent<Image>().sprite = healthBarFullness[0]; // empty bar
        else if (health < 1f)
            healthWifiBar.sprite = healthBarFullness[1]; // less than one bar but not dead
        else
        {
            healthWifiBar.sprite = healthBarFullness[(int)Mathf.Floor(health) + 1]; // +1 to account for shift by the "less than one" sprite
        }
        // energy bar ------
        energy = logic.GetEnergy();
        energyLerpSpeed = logic.energyLerpSpeed;
        energyLerpThreshold = logic.energyLerpThreshold;
        if (easeEnergySlider.value != energy)
        {
            easeEnergySlider.value = energy;
        }
        if (energySlider.value < energy)
        {
            if (energy - energySlider.value >= energyLerpThreshold)
                energySlider.value = Mathf.Lerp(energySlider.value, energy, energyLerpSpeed); // (startval, endval, lerpSpeed)
            else
                energySlider.value = energy;
        }
        else if (energySlider.value > energy)
            energySlider.value = energy;
        // score counter ------
        score = logic.score;
        scoreText.GetComponent<Text>().text = "SCORE: " + score.ToString();
    }


}
