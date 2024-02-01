using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState {
	private float timer = 0.0f;

	public AIIdleState(AIStateAgent agent) : base(agent) {
		//
	}

	public override void OnEnter() {
		Debug.Log("Idle Enter");
		timer = Time.time + Random.Range(1, 2);
	}

	public override void OnUpdate() {
		Debug.Log("Idle Update");

		if(Time.time > timer) {
			agent.stateMachine.SetState(nameof(AIPatrolState));
		}

		GameObject[] enemies = agent.enemyPerception.GetGameObjects();

		if(enemies.Length > 0) {
			agent.stateMachine.SetState(nameof(AIAttackState));
		}
	}

	public override void OnExit() {
		Debug.Log("Idle Exit");
	}
}
