using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaveState : AIState {
	private float timer = 0.0f;

	public AIWaveState(AIStateAgent agent) : base(agent) {
		//
	}

	public override void OnEnter() {
		timer = Time.time + 4.733f;

		agent.animator?.SetTrigger("Wave");
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
