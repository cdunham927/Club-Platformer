using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour {
    private int atk;

    private void Awake()
    {
        atk = GetComponentInParent<AIController>().atk;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
            col.gameObject.GetComponent<PlayerController>().curHealth -= atk;
    }
}
