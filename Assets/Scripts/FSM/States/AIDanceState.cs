using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDanceState : AIState {
	public AIDanceState(AIStateAgent agent) : base(agent) {
		AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0.0f));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIFleeState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transitions.Add(transition);
	}

	public override void OnEnter() {
		agent.movement.Stop();
		agent.movement.Velocity = Vector3.zero;

		agent.timer.value = Random.Range(10.0f, 12.5f);

		agent.animator?.SetBool("Dance", true);
	}

	public override void OnUpdate() {
		//
	}

	public override void OnExit() {
		agent.animator?.SetBool("Dance", false);
	}
}
