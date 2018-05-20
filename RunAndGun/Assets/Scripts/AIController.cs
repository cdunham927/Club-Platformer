using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : MonoBehaviour {
    //Possible states of the AI
    public enum states { idle, attack, death };
    //Current state of the AI
    [HideInInspector]
    private states curState;
    public states setState { get; set; }
    //Enemy stats
    public int health;
    public int curHp;
    public int atk;
    public int spd;
    //Variables important for switching states/using states
    public float atkWait;
    public float atkRange;
    //Functions for running enemy states
    public abstract void idle();
    public abstract void attack();
    public abstract void death();

    void Update()
    {
        switch (curState)
        {
            case states.idle:
                idle();
                break;
            case states.attack:
                attack();
                break;
            case states.death:
                death();
                break;
        }
    }
}
