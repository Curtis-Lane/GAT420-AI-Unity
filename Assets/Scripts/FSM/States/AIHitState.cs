using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitState : AIState {
	private float timer = 0.0f;

	public AIHitState(AIStateAgent agent) : base(agent) {
		//
	}

	public override void OnEnter() {
		agent.movement.Stop();
		agent.movement.Velocity = Vector3.zero;

		timer = Time.time + 1.2f;

		agent.animator?.SetTrigger("Hit");
	}

	public override void OnUpdate() {
		if(Time.time >= timer) {
			agent.stateMachine.SetState(nameof(AIIdleState));
		}
	}

	public override void OnExit() {
		//
	}
}
