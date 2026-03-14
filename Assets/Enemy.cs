using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float attackDistance;
    [SerializeField] int damage;
    [SerializeField] int cooldown;
    [SerializeField] GameObject player;
    Animator anim;
    Rigidbody rb;
    float distance;
    float timer;
    bool dead;
    [SerializeField] float speed;
    [SerializeField] float detectiondistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (!dead)
        {
            Attack();
        }
    }
    private void FixedUpdate() 
    {
        if (!dead)
        {
            Move();
        }
    }
    void Attack()
    {
        timer += Time.deltaTime;
        if (attackDistance > distance && timer > cooldown)
        {
            timer = 0;
            player.GetComponent<WalkPlayer>().Changehealth(damage);
            anim.SetBool("Shoot", true);
        }
        else
        {
            anim.SetBool("Shoot", false);
        }
    }
    void Move()
    {
        if (distance < detectiondistance && distance > attackDistance)
        {
            transform.LookAt(player.transform);
            anim.SetBool("Run", true);
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }
    public void ChangeHealth(int count)
    {
        health -= count;
        if (health <= 0)
        {
            anim.SetBool("Die", true);
            dead = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
