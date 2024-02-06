using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseState : AIState {
	private float initalSpeed;

	public AIChaseState(AIStateAgent agent) : base(agent) {
		//
	}

	public override void OnEnter() {
		agent.movement.Resume();

		initalSpeed = agent.movement.maxSpeed;
		agent.movement.maxSpeed *= 2;
	}

	public override void OnUpdate() {
		GameObject[] enemies = agent.enemyPerception.GetGameObjects();
		if(enemies.Length > 0) {
			GameObject enemy = enemies[0];

			agent.movement.Destination = enemy.transform.position;

			if(Vector3.Distance(agent.transform.position, enemy.transform.position) < 1.25f) {
				agent.stateMachine.SetState(nameof(AIAttackState));
			}
		} else {
			agent.stateMachine.SetState(nameof(AIIdleState));
		}
	}

	public override void OnExit() {
		agent.movement.maxSpeed = initalSpeed;
	}
}
