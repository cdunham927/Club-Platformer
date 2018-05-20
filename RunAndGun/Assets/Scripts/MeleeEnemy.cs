using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : AIController {
    AIController AI;
    
    int direction = 1;

	void Awake () {
        AI = GetComponent<AIController>();
	}
	
	public override void idle () {
        /**if (Vector3.Distance(transform, transform) <  ) {
            AI.setState = states.attack;
                }*/
	}

    public override void attack()
    {

    }

    public override void death()
    {
        
    }
}
