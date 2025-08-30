using UnityEngine;
using UnityEngine.Animations;

public class robotScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Animator animator;
    public float normalMove = 3;
    public float dashMove = 3;
    public float jumpHeight = 5;
    private float hurtTimer = 0;


    public LogicScript logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (logic.isHurt)
        {
            if (hurtTimer >= logic.hurtDuration)
            {
                hurtTimer = 0;
                logic.isHurt = false;
            }
            else
                hurtTimer += Time.deltaTime;
        }
        animator.SetBool("isDashing", logic.ifDash);
        animator.SetBool("isHurt", logic.isHurt);
        animator.SetBool("isAlive", logic.alive);
        animator.SetBool("isJumping", logic.isJumping);
        animator.SetBool("isWalking", logic.isWalking);
        if (rigidBody.linearVelocity.x < 14 && rigidBody.linearVelocity.x > -14 && logic.ifDash)
        {
            rigidBody.linearVelocity = new Vector3(0, 0, 0);
            logic.ifDash = false;
        }
        if (!logic.alive || !logic.gameStarted || logic.isHurt)
            return;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Time.deltaTime * normalMove;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            logic.isWalking = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Time.deltaTime * normalMove;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            logic.isWalking = true;
        }
        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
            logic.isWalking = false;
        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y < -3.3)
        {
            rigidBody.linearVelocity = Vector3.up * jumpHeight;
            logic.isJumping = true;
        }
        // dashing 
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Z))
        {
            if (logic.GetEnergy() >= logic.energyNeededforDash)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    rigidBody.linearVelocity = Vector3.left * dashMove;
                    logic.DrainEnergy(logic.energyNeededforDash);
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    logic.ifDash = true;
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    rigidBody.linearVelocity = Vector3.right * dashMove;
                    logic.DrainEnergy(logic.energyNeededforDash);
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    logic.ifDash = true;
                }
            }
            else
            {
                //idk, flash the energy bar or smth?
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Malware") && !logic.ifDash)
        {
            if (logic.isHurt)
                return;
            logic.TakeDamage(logic.gainedMalwareDamage);
            logic.isHurt = true;
        }
        else if (collision.gameObject.CompareTag("Cookie"))
        {
            logic.RecoverHealth(logic.gainedCookieHealth);
            logic.score++;
            logic.fallingStuffSpeed += 0.05f;
        }
        else if (collision.gameObject.CompareTag("Energy")) {
            logic.ChargeEnergy(logic.gainedEnergyCharge);
        }
        else if (collision.gameObject.CompareTag("chocoCookie")) {
            logic.destroyAllMalware = true;
        }
        else if (collision.gameObject.CompareTag("terrain"))
        {
            if (logic.isJumping) // little guy just landed
                logic.isJumping = false;
        }
    }
}
