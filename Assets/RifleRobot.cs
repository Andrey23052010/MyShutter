using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RifleRobot : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] Camera cam;
    float cooldown = 0;
    float timer = 0;
    float speedPlayer;
    Animator anim;
    [SerializeField] int bullets = 20;
    int bulletmax = 20;
    [SerializeField] int bulletAll = 200;
    int health;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<WalkPlayer>().health;
        speedPlayer = 5;
        anim = GetComponent<Animator>();
        cooldown = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            this.enabled = false;
        }
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && speedPlayer == 5 && bullets > 0)
        {
            Shoot();
            anim.SetBool("Shoot", true);
        }
        if (!Input.GetMouseButton(0))
        {
            anim.SetBool("Shoot", false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
                speedPlayer = 10;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
                speedPlayer = 5;
        }
        if (bullets != bulletmax && bulletAll != 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Invoke("ReloadF", 1);
                anim.SetBool("Reload", true);
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
            {
                anim.SetBool("Reload", false);
            }
        
    }
    void Shoot()
    {
        if(Input.GetMouseButtonDown(0)){
            if(timer > cooldown)
            {
                Vector3 startposition = new Vector3(Screen.width/2, Screen.height/2, 0);
                Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(startposition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject bullet = Instantiate(particle, hit.point, hit.transform.rotation);
                    Destroy(bullet, 1);
                    if (hit.collider.CompareTag("enemy"))
                    {
                        hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(10);
                    }
                }
                bullets -= 1;
                timer = 0;
            }
        }
    }
    void ReloadF()
    {
        int need = bulletmax - bullets;
        if (need < bulletAll)
        {
            bullets += need;
            bulletAll -= need;
        }
        else
        {
            bullets += bulletAll;
            bulletAll = 0;
        }
    }
}
