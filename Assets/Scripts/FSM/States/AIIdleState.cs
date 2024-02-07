using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState {

	public AIIdleState(AIStateAgent agent) : base(agent) {
		AIStateTransition transition = new AIStateTransition(nameof(AIPatrolState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0.0f));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIChaseState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transitions.Add(transition);
	}

	public override void OnEnter() {
		Debug.Log("Idle Enter");

		agent.movement.Stop();
		agent.movement.Velocity = Vector3.zero;

		agent.timer.value = Random.Range(1, 2);
	}

	public override void OnUpdate() {
		Debug.Log("Idle Update");

		if(Random.Range(0, 2500) == 0) {
			agent.stateMachine.SetState(nameof(AIDanceState));
		}

		GameObject[] friends = agent.friendPerception.GetGameObjects();

		if(friends.Length > 0 && (Random.Range(0, 1000) == 0)) {
			agent.stateMachine.SetState(nameof(AIWaveState));
		}

		GameObject[] enemies = agent.enemyPerception.GetGameObjects();

		if(enemies.Length > 3) {
			// NIGERUNDAIYO, SMOKEY!
			agent.stateMachine.SetState(nameof(AIFleeState));
			return;
		}
	}

	public override void OnExit() {
		Debug.Log("Idle Exit");
	}
}
