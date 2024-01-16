using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Range(50f, 200f)]
    public float walkSpeed = 100f;
    [Range(100f, 500f)]
    public float runSpeed = 200f;

    private float mSpeed;

    public Camera cam;
    public Camera cam2;
    public new GameObject camera;
    public GameObject camera2;
    float baseFOV = 75;
    public float sprintFOV = 1.5f;

    [Range(100f, 2000f)]
    public float jumpForce = 300f;

    public LayerMask ground;
    public Transform groundDetector;

    Rigidbody rb;

    public int state = 0;
    public bool onGround = true;
    //public bool attack = false;

    Animator anim;

    bool dead = false;
    UnityEngine.AI.NavMeshAgent agent;
    public float detect_radius = 10;
    public float atk_radius = 10;
    public LayerMask targetLayer;
    public LayerMask talkLayer;

    public float damage = 5f;
    public float hp = 20f;

    public GameObject panel;

    void Start()
    {
        mSpeed = walkSpeed;
        rb = GetComponent<Rigidbody>();
        baseFOV = cam.fieldOfView;

        anim = GetComponent<Animator>();
        camera2.SetActive(true);

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        panel.SetActive(false);
    }


    void FixedUpdate()
    {
        if(dead == false)
        {
            anim.SetBool("onGround", onGround);

            bool groundCheck = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
            bool jump = Input.GetKey(KeyCode.Space) && groundCheck;
            if (onGround == true && jump == true)
            {
                rb.AddForce(Vector3.up * jumpForce * 10);
                onGround = false;
            }
            else
            {
                onGround = true;
            }

            //Debug.Log(onGround);
            //Debug.Log(state);

            bool sprint = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

            float xMove = Input.GetAxisRaw("Horizontal");
            float zMove = Input.GetAxisRaw("Vertical");

            if (sprint == true && zMove > 0)
            {
                mSpeed = runSpeed;
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, baseFOV * sprintFOV, Time.fixedDeltaTime * 8f);

            }
            else
            {
                mSpeed = walkSpeed;
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, baseFOV, Time.fixedDeltaTime * 8f);

            }
            Vector3 dir = new Vector3(xMove, 0, zMove);
            dir.Normalize();

            Vector3 v = transform.TransformDirection(dir) * mSpeed * Time.fixedDeltaTime;
            v.y = rb.velocity.y;
            rb.velocity = v;

            if (onGround == true && (zMove > 0 || zMove < 0))
            {
                anim.SetInteger("state", 1);

                if (sprint == true)
                {
                    anim.SetInteger("state", 2);
                }

            }
            else if (onGround == true && zMove <= 0)
            {
                anim.SetInteger("state", 0);
            }

            if (xMove > 0)
            {
                transform.Rotate(Vector3.up, mSpeed * Time.deltaTime, Space.Self);
                if (onGround == true)
                {
                    anim.SetInteger("state", 1);
                }

            }

            if (xMove < 0)
            {
                transform.Rotate(Vector3.up, -mSpeed * Time.deltaTime, Space.Self);
                if (onGround == true)
                {
                    anim.SetInteger("state", 1);
                }

            }



            if (Input.GetKey(KeyCode.Tab))
            {
                camera.SetActive(false);
                camera2.SetActive(true);
            }
            else
            {
                camera2.SetActive(false);
                camera.SetActive(true);
            }


            
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000f, targetLayer))
                {
                    if (Vector3.Distance(transform.position, hit.transform.position) <= atk_radius)
                    {
                        anim.SetInteger("state", 3);
                        
                    }

                }

                if (Physics.Raycast(ray, out hit, 1000f, talkLayer))
                {
                    if (Vector3.Distance(transform.position, hit.transform.position) <= detect_radius)
                    {
                        anim.SetInteger("state", 4);

                    }

                }

            }


            //if (GameObject.FindWithTag("Player"))
                //hp = 15;
            GameObject.FindWithTag("ScoreBoard").GetComponent<ScoreboardScript>().hp_up(hp);

        }
    }

    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            takeDamage();
        }
        GameObject.FindWithTag("ScoreBoard").GetComponent<ScoreboardScript>().hp_up(hp);
        //Debug.Log(hp);
    }

    public void takeDamage()
    {
        dead = true;
        agent.SetDestination(transform.position);
        state = 4;
        Destroy(this.gameObject, 2);

        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void attack()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, atk_radius, targetLayer);

        if (cols.Length > 0)
        {
            eat c = cols[0].transform.GetComponent<eat>();
            if (c != null)
            {
                c.Hit(damage);
            }

            eat1 c0 = cols[0].transform.GetComponent<eat1>();
            if (c0 != null)
            {
                c0.Hit(damage);
            }

            shell c1 = cols[0].transform.GetComponent<shell>();
            if (c1 != null)
            {
                c1.Hit(damage);
            }
        }
    }

    public void talk()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, detect_radius, talkLayer);

        if (cols.Length > 0)
        {
            Guard c = cols[0].transform.GetComponent<Guard>();
            if (c != null)
            {
                c.interact();
            }
        }
    }

}
