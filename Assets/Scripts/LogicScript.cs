using UnityEngine;
using static Unity.Mathematics.math;

public class LogicScript : MonoBehaviour
{
    // edit the variables on this script/this object only
    public float maxHealth = 100;
    public float health;
    public float damageLerpSpeed;
    public float lerpThreshold;

    public int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health = max(0f, health - amount);
        health = min(100f, health);
    }

    public void RecoverHealth(float amount)
    {
        health = min(100f, health + amount);
        health = max(0f, health);
    }
}
