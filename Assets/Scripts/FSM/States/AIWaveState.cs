using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaveState : AIState {
	public AIWaveState(AIStateAgent agent) : base(agent) {
		AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0.0f));
		transitions.Add(transition);
	}

	public override void OnEnter() {
		agent.movement.Stop();
		agent.movement.Velocity = Vector3.zero;

		agent.timer.value = 4.733f;

		agent.animator?.SetTrigger("Wave");
	}

	public override void OnUpdate() {
		//
	}

	public override void OnExit() {
		//
	}
}
