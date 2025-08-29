using UnityEngine;

public class fallingStuffSpawnerScript : MonoBehaviour
{
    public LogicScript logic;
    private float timer = 0;
    public GameObject[] fallingStuffs;
    public int[] spawningChances; // LAST ARRAY ELEMENT IS ALWAYS CONSIDERED THE CHANCE FOR "NO SPAWN", assign the rest in the correct order you want with the fallingStuffs array
    // for {malware,cookie,energy,none}{30,30,30,10}: 1->30 is malware, 31->60 is cookie, 61->90 is energy, 91->100 is none
    public Transform leftMostSpawningPoint;
    public Transform rightMostSpawningPoint;
    private float spawnRate;
    private float spawnDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnRate = logic.score / 5f + 1;
        spawnDelay = 1 / spawnRate;
        timer += Time.deltaTime;
        if (timer > spawnDelay)
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
