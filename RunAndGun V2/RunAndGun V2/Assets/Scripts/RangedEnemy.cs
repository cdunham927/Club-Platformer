using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : AIController
{
    AIController AI;
    Controller2D controller;
    Animator anim;

    public LayerMask playerMask;
    [SerializeField]
    float size = 1;
    [SerializeField]
    int direction = 1;

    float count = 0;
    Vector3 velocity;
    bool hit;

    void Awake()
    {
        controller = GetComponent<Controller2D>();
        AI = GetComponent<AIController>();
        anim = GetComponent<Animator>();
    }

    public override void idle()
    {
        hit = Physics2D.OverlapCircle(transform.position, size, playerMask);
        if (hit)
        {
            anim.SetInteger("state", 1);
            AI.setState = states.chase;
        }

        Debug.DrawRay(transform.position, transform.right * size * direction);
    }

    public override void chase()
    {
        Debug.DrawRay(transform.position, transform.right * size * direction);

        /*When player is in sight:
         * 1. Get within melee range
         * 2. Switch to attack state(animation)
         * 3. Detect collision with player(if hit)
         * 4. Reduce player health
         * 5. Turn on player iframes
         * 6. Reset to idle/chase state
         * 7. Repeat
         */
        float distToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        //Debug.Log(distToPlayer);
        if (PlayerController.instance.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
            velocity = new Vector3(spd, 0f);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            velocity = new Vector3(-spd, 0f);
        }
        controller.Move(velocity * Time.deltaTime);

        if (distToPlayer < atkRange)
        {
            anim.SetInteger("state", 2);
            AI.setState = states.attack;
        }

        if (distToPlayer > idleRange)
        {
            /*When player is out of sight range:
             * 1. Start counter
             * 2. If counter reaches... 2.5 seconds:
             * 3. Reset to idle state
             */
            count += Time.deltaTime;
        }
        else count = 0;

        if (count > 2)
        {
            count = 0;
            anim.SetInteger("state", 0);
            AI.setState = states.idle;
        }
    }

    public override void attack()
    {
        Debug.DrawRay(transform.position, transform.right * size * direction);

        Invoke("attackEnd", atkWait);
    }

    void attackEnd()
    {
        anim.SetInteger("state", 0);
        AI.setState = states.idle;
    }

    public override void death()
    {
        Destroy(gameObject);
    }
}
