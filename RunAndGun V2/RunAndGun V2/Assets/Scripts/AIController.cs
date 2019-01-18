using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : MonoBehaviour {
    //Possible states of the AI
    public enum states { idle, chase, attack, death };
    //Current state of the AI
    [HideInInspector]
    private states curState;
    public states setState
    { get
        {
            return curState;
        }
        set
        {
            curState = value;
        }
    }
    //Enemy stats
    public int health;
    public int curHp;
    public int atk;
    public int spd;
    //Variables important for switching states/using states
    public float atkWait;
    public float atkRange;
    public float idleRange;
    //Functions for running enemy states
    public abstract void idle();
    public abstract void chase();
    public abstract void attack();
    public abstract void death();

    void Update()
    {
        switch (curState)
        {
            case states.idle:
                idle();
                break;
            case states.chase:
                chase();
                break;
            case states.attack:
                attack();
                break;
            case states.death:
                death();
                break;
        }

        if (health <= 0) curState = states.death;
    }
}
