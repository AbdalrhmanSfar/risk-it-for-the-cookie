using UnityEngine;

public class fallingThingyScript : MonoBehaviour {
    public LogicScript logic; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update() {
        
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("terrain")) { ++logic.score;  }
        Destroy(gameObject); 
    }
}
