using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject logicManager;
    private LogicScript logic;

    public GameObject healthBar;
    public GameObject easeDamageBar;
    private Slider healthSlider;
    private Slider easeDamageSlider;
    // edits to these from LogicScript only
    private float health; // edits from LogicScript only
    private float lerpSpeed; // lerpSpeed must be in range [0,1], if 0.5 Mathf.Lerp() returns halfway point, etc
    private float lerpThreshold;

    public GameObject scoreText;
    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = logicManager.GetComponent<LogicScript>();
        healthSlider = healthBar.GetComponent<Slider>();
        easeDamageSlider = easeDamageBar.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        health = logic.health;
        lerpSpeed = logic.damageLerpSpeed;
        lerpThreshold = logic.lerpThreshold;
        score = logic.score;
        // healthbar ------
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }
        if (easeDamageSlider.value > health)
        {
            if (easeDamageSlider.value - health >= lerpThreshold)
                easeDamageSlider.value = Mathf.Lerp(easeDamageSlider.value, health, lerpSpeed); // (startval, endval, lerpSpeed)
            else
                easeDamageSlider.value = health;
        }
        else if (easeDamageSlider.value < health)
            easeDamageSlider.value = health;
        // score counter ------
        scoreText.GetComponent<Text>().text = "SCORE: " + score.ToString();
    }
}
