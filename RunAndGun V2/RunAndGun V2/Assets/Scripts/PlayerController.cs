using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Controller2D))]
public class PlayerController : MonoBehaviour {
    public static PlayerController instance = null;

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;

    float jumpVelocity = 8;
    float gravity = -20;

    float moveSpeed = 6;
    Vector3 velocity;
    float velocityXSmoothing;

    public float targetVelocityX = 0;
    public GameObject bullet;

    public int dir = 1;

    ///////////////
    //stats
    public float curHealth;
    public float maxHealth = 100;
    /// /////////////////


    Controller2D controller;

    void Awake() {
        //singleton behavior for the player
        //if (instance == null)
        {
            instance = this;
        }
        /*else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        */
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);
    }

    void Start()
    {
        curHealth = maxHealth;
    }

    void Update() {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.y > 0 && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        if (input.x > 0) dir = 1;
        else if (input.x < 0) dir = -1;

        targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Health
        if (curHealth <= 0)
        {
            Die();
        }

        //Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
    }

    void Die()
    {
        //Restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeDamage(float amt)
    {
        curHealth -= amt;
        GetComponentInChildren<Health>().healthBar.GetComponent<Health>().UpdateHealth(curHealth/maxHealth);
    }
}
