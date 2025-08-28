using UnityEngine;

public class robotScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float normalMove = 3;
    public float dashMove = 3;
    public float jumpHeight = 5;
    private bool ifDash = false;
    public LogicScript logic;
    private bool alive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (logic.health <= 0) alive = false; ;
        if (rigidBody.linearVelocity.x < 14 && rigidBody.linearVelocity.x > -14 && ifDash)
        {
            rigidBody.linearVelocity = new Vector3(0, 0, 0);
            ifDash = false;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && alive)
        {
            transform.position += Vector3.left * Time.deltaTime * normalMove;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && alive)
        {
            transform.position += Vector3.right * Time.deltaTime * normalMove;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < -3.3 && alive)
        {
            rigidBody.linearVelocity = Vector3.up * jumpHeight;
        }
        // dashing 
        if (Input.GetKeyDown(KeyCode.Z) && alive && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            rigidBody.linearVelocity = Vector3.left * dashMove;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            ifDash = true;
        }
        if (Input.GetKeyDown(KeyCode.C) && alive && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            rigidBody.linearVelocity = Vector3.right * dashMove;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            ifDash = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("fallingThingy"))
        {
            logic.health -= 15;
        }
    }
}
