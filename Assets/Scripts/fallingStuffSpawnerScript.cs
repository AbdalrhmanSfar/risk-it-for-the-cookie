using UnityEngine;

public class fallingStuffSpawnerScript : MonoBehaviour
{
    private float timer = 0;
    public GameObject fallingStuff;
    public float spawnRate = 1;
    private float harderTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;     harderTimer += Time.deltaTime;
        if (harderTimer > 8) {
            spawnRate -= 0.1f;  if (spawnRate < 0.2) spawnRate = 0.2f;
            harderTimer = 0;
        }
        if (timer > spawnRate) {
            Instantiate(fallingStuff, new Vector3(Random.Range(-9, 9), transform.position.y, transform.position.z), transform.rotation);
            timer = 0;
        }
    }
}
