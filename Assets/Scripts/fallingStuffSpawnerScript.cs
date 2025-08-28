using UnityEngine;

public class fallingStuffSpawnerScript : MonoBehaviour
{
    private float timer = 0;
    public GameObject[] fallingStuffs;
    public int[] spawningChances; // LAST ARRAY ELEMENT IS ALWAYS CONSIDERED THE CHANCE FOR "NO SPAWN", assign the rest in the correct order you want with the fallingStuffs array
    // for {malware,cookie,energy,none}{30,30,30,10}: 1->30 is malware, 31->60 is cookie, 61->90 is energy, 91->100 is none
    public Transform leftMostSpawningPoint;
    public Transform rightMostSpawningPoint;
    public float spawnRate = 1;
    private float harderTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; harderTimer += Time.deltaTime;
        if (harderTimer > 8)
        {
            spawnRate -= 0.1f; if (spawnRate < 0.2) spawnRate = 0.2f;
            harderTimer = 0;
        }
        if (timer > spawnRate)
        {
            int sum = 0;
            for (int i = 0; i < spawningChances.Length; i++)
                sum += spawningChances[i];
            int num = Random.Range(1, sum + 1); // generate random number between 1 and sum of chances
            sum = 0;
            for (int i = 0; i < spawningChances.Length; i++)
            {
                sum += spawningChances[i];
                if (sum >= num)
                {
                    // index 0 for malware, index 1 for cookie, index 2 for energy, index 3 for none
                    float lmspx = leftMostSpawningPoint.position.x, rmspx = rightMostSpawningPoint.position.x;
                    float lmspy = leftMostSpawningPoint.position.y, rmspy = rightMostSpawningPoint.position.y;
                    if (i != spawningChances.Length - 1) // if not none
                    {
                        Instantiate(fallingStuffs[i], new Vector3(Random.Range(lmspx, rmspx), (lmspy + rmspy) / 2, transform.position.z), transform.rotation);
                        Debug.Log("Spawned " + i.ToString() + " for random num " + num.ToString());
                    }
                    break;
                }
            }
            timer = 0;
        }
    }
}
