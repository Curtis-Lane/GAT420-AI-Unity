using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDanceState : AIState {
	private float timer = 0.0f;

	public AIDanceState(AIStateAgent agent) : base(agent) {
		//
	}

	public override void OnEnter() {
		agent.movement.Stop();
		agent.movement.Velocity = Vector3.zero;

		timer = Time.time + Random.Range(10.0f, 12.5f);

		agent.animator?.SetBool("Dance", true);
	}

	public override void OnUpdate() {
		if(!(agent.enemyPerception.GetGameObjects().Length > 0)) {
			if(Time.time >= timer) {
				agent.stateMachine.SetState(nameof(AIIdleState));
			}
		} else {
			agent.stateMachine.SetState(nameof(AIFleeState));
		}
	}

	public override void OnExit() {
		agent.animator?.SetBool("Dance", false);
	}
}
