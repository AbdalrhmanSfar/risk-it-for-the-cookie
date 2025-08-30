using UnityEngine;

public class fallingThingyScript : MonoBehaviour
{
    public LogicScript logic;
    private float speed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update() {
        if (logic.destroyAllMalware && transform.tag == "Malware") Destroy(gameObject); 

        speed = logic.fallingStuffSpeed;
        transform.position += speed * Time.deltaTime * Vector3.down;

        if (transform.position.y < (-6)) Destroy(gameObject); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("terrain"))
        {
            Destroy(gameObject);
            //Debug.Log("Destoyed falling thingy");
        }
    }
}
