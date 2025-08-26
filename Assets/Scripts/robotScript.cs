using UnityEngine;

public class robotScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float normalMove = 3;
    public float jumpHeight = 5;
    private bool ifDash = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        if (rigidBody.linearVelocity.x < 14 && rigidBody.linearVelocity.x > -14 && ifDash) {
            rigidBody.linearVelocity = new Vector3(0, 0, 0);
            ifDash = false;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            rigidBody.linearVelocity = Vector3.left * normalMove;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)) {
            rigidBody.linearVelocity = Vector3.right * normalMove;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < -2.8 ) {
            rigidBody.linearVelocity = Vector3.up * jumpHeight; 
        }
        // dashing 
        if (Input.GetKeyDown(KeyCode.Z)) {
            rigidBody.linearVelocity = Vector3.left * normalMove * 5;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            ifDash = true;
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            rigidBody.linearVelocity = Vector3.right * normalMove * 5;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            ifDash = true;
        }
    }
}
