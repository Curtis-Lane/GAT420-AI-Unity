using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState {
	private float timer = 0.0f;

	public AIAttackState(AIStateAgent agent) : base(agent) {
		//
	}

	public override void OnEnter() {
		Debug.Log("Attack Enter");

		agent.animator?.SetTrigger("Attack");
		timer = Time.time + 2;
	}

	public override void OnUpdate() {
		Debug.Log("Attack Update");

		if(Time.time >= timer) {
			agent.stateMachine.SetState(nameof(AIIdleState));
		}
	}

	public override void OnExit() {
		Debug.Log("Attack Exit");
	}
}
