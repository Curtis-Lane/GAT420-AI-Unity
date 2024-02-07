using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState {
	private float timer = 0.0f;

	public AIDeathState(AIStateAgent agent) : base(agent) {
		//
	}

	public override void OnEnter() {
		agent.movement.Stop();
		agent.movement.Velocity = Vector3.zero;

		agent.animator?.SetTrigger("Death");

		timer = Time.time + 4.6f;
	}

	public override void OnUpdate() {
		if(Time.time > timer) {
			GameObject.Destroy(agent.gameObject);
		}
	}

	public override void OnExit() {
		//
	}
}
