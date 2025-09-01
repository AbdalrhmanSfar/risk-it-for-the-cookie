using Unity.VisualScripting;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private LogicScript logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position); // in pixels

        float rightSideOfScreenX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x; // in world units
        float leftSideOfScreenX = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x; // in world units

        if (playerScreenPos.x <= 0 && logic.isWalking)
        {
            Debug.Log("teleport to right");
            transform.position = new Vector2(rightSideOfScreenX, transform.position.y);
        }
        if (playerScreenPos.x >= Screen.width && logic.isWalking)
        {
            Debug.Log("teleport to left");
            transform.position = new Vector2(leftSideOfScreenX, transform.position.y);
        }
    }
}
