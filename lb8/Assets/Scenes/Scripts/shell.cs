using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour
{
    public float detect_radius = 5;
    public float atk_radius = 5;
    public LayerMask playerLayer;
    public Transform[] waypoints;
    int ind = 0;
    int state = 0;

    Animator animator;
    
    UnityEngine.AI.NavMeshAgent agent;

    public float damage = 2f;
    public float hp = 15f;
    bool dead = false;
    float points = 30;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(waypoints[ind].position);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (dead == false)
        {
            if (agent.velocity.magnitude > 0.1f)
                animator.SetInteger("state", 1);

            if (Vector3.Distance(transform.position, waypoints[ind].position) < 1.5f)
            {
                ind++;
                if (ind >= waypoints.Length)
                {
                    ind = 0;
                }
                agent.SetDestination(waypoints[ind].position);
            }

            Collider[] cols = Physics.OverlapSphere(transform.position, detect_radius, playerLayer);

            if (cols.Length > 0)
            {
                if (Vector3.Distance(transform.position, cols[0].transform.position) <= atk_radius)
                {
                    agent.SetDestination(transform.position);
                    state = 2;
                    animator.SetInteger("state", state);
                }
                else
                {
                    agent.SetDestination(cols[0].transform.position);
                }
            }
            else
            {
                agent.SetDestination(waypoints[ind].position);
            }
        }
            
    }

    public void attack()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, atk_radius, playerLayer);

        if(cols.Length > 0)
        {
            player c = cols[0].transform.GetComponent<player>();
            if (c != null)
            {
                c.Hit(damage);
            }
        }
    }

    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            takeDamage();
        }
        //Debug.Log(hp);
    }

    public void takeDamage()
    {
        if (GameObject.FindWithTag("T2"))
            points = 30;

        GameObject.FindWithTag("ScoreBoard").GetComponent<ScoreboardScript>().score_up(points);

        dead = true;
        Destroy(this.gameObject, 2);
    }
}
