using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIState {
	Vector3 destination;

	public AIPatrolState(AIStateAgent agent) : base(agent) {
		//
	}

	public override void OnEnter() {
		AINavNode navNode = AINavNode.GetRandomAINavNode();
		destination = navNode.transform.position;
	}

	public override void OnUpdate() {
		agent.movement.MoveTowards(destination);
		if(Vector3.Distance(agent.transform.position, destination) < 1.0f) {
			agent.stateMachine.SetState(nameof(AIIdleState));
		}

		GameObject[] enemies = agent.enemyPerception.GetGameObjects();

		if(enemies.Length > 0) {
			agent.stateMachine.SetState(nameof(AIChaseState));
		}
	}

	public override void OnExit() {
		//
	}
}
