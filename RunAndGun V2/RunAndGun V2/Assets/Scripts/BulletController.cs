using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float spd;
    private Rigidbody2D bod;
    int dir = 1;
    int atk = 1;

	void Awake () {
        bod = GetComponent<Rigidbody2D>();
	}

    private void OnEnable()
    {
        bod.AddForce(transform.right * spd * PlayerController.instance.dir);
        Invoke("Disable", 2f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //Hurt enemy
            col.gameObject.GetComponent<AIController>().health -= atk;
            Invoke("Disable", 0.01f);
        }
    }
}
