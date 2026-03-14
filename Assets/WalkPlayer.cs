using UnityEngine;

public class WalkPlayer : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] public float speed;
    [SerializeField] float jumpForce = 5f;
    float currentspeed;
    Vector3 direction;
    public Animator anim;
    float shiftspeed = 10f;
    bool canjump = true;
    [SerializeField] public int health = 100;
    [SerializeField] float stamina = 5f;
    public void Changehealth(int count)
    {
        health -= count;
        if (health <= 0)
        {
            anim.SetBool("Die", true);
            this.enabled = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {
        canjump = true;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("Right", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Right", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("Left", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("Left", false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Front", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Front", false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("Back", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("Back", false);
        }
        if (canjump)
        {
            anim.SetBool("Jump", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canjump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            anim.SetBool("Jump", true);
            canjump = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0){
                anim.SetBool("Run", true);
                currentspeed = shiftspeed;
                stamina -= Time.deltaTime;
            }
            else
            {
                anim.SetBool("Run", false);
                currentspeed = speed;
            }
        
        }
        if (stamina < 0)
        {
            stamina = 0;
        }
        if (stamina > 5)
        {
            stamina = 5;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Run", false);
            currentspeed = speed;
            stamina += Time.deltaTime;
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position+direction*currentspeed*Time.deltaTime);
    }
}
