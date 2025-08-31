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
    private bool blueCookieActve = false;
    private bool noJumpDash = false;
    private float blueCookieTimer = 0;

    public LogicScript logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SFXScript.instance.awakeSFX();
    }

    // Update is called once per frame
    void Update() {
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
        if (Input.GetKeyDown(KeyCode.Space) && !logic.isJumping && !noJumpDash)
        {
            rigidBody.linearVelocity = Vector3.up * jumpHeight;
            logic.isJumping = true;
        }
        // dashing 
        if ((Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Z)) && !noJumpDash)
        {
            if (logic.GetEnergy() >= logic.energyNeededforDash)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    SFXScript.instance.dashSFX();
                    rigidBody.linearVelocity = Vector3.left * dashMove;
                    logic.DrainEnergy(logic.energyNeededforDash);
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    logic.ifDash = true;
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    SFXScript.instance.dashSFX();
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

        if (blueCookieActve) {
            blueCookieTimer += Time.deltaTime; 
            if (blueCookieTimer > 3) {
                blueCookieTimer = 0; blueCookieActve = false;
                noJumpDash = false;  normalMove = 7.5f;
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
            SFXScript.instance.hit1SFX();
        }
        else if (collision.gameObject.CompareTag("Cookie"))
        {
            logic.RecoverHealth(logic.gainedCookieHealth);
            logic.score++;
            logic.fallingStuffSpeed += 0.05f;
            SFXScript.instance.boopSFX();
        }
        else if (collision.gameObject.CompareTag("Energy")) {
            logic.ChargeEnergy(logic.gainedEnergyCharge);
            SFXScript.instance.redbullSFX();
        }
        else if (collision.gameObject.CompareTag("chocoCookie")) {
            logic.destroyAllMalware = true;
            SFXScript.instance.chocoSFX();
        }
        else if (collision.gameObject.CompareTag("weirdCookie")) {
            blueCookieActve = true;  noJumpDash = true;
            normalMove = 1.5f;
            SFXScript.instance.hit2SFX();
        }
        else if (collision.gameObject.CompareTag("terrain"))
        {
            if (logic.isJumping) // little guy just landed
                logic.isJumping = false;
        }
    }
}
