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

		agent.movement.Stop();
		agent.movement.Velocity = Vector3.zero;

		timer = Time.time + Random.Range(1, 2);
	}

	public override void OnUpdate() {
		Debug.Log("Idle Update");

		if(Time.time > timer) {
			agent.stateMachine.SetState(nameof(AIPatrolState));
		}

		if(Random.Range(0, 2500) == 0) {
			agent.stateMachine.SetState(nameof(AIDanceState));
		}

		GameObject[] friends = agent.friendPerception.GetGameObjects();

		if(friends.Length > 0 && (Random.Range(0, 1000) == 0)) {
			agent.stateMachine.SetState(nameof(AIWaveState));
		}

		GameObject[] enemies = agent.enemyPerception.GetGameObjects();

		if(enemies.Length > 3) {
			// NIGERUNDAIYO, SMOKEY
			agent.stateMachine.SetState(nameof(AIFleeState));
			return;
		}

		if(enemies.Length > 0) {
			agent.stateMachine.SetState(nameof(AIChaseState));
		}
	}

	public override void OnExit() {
		Debug.Log("Idle Exit");
	}
}
