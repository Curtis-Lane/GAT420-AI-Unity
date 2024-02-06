using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFleeState : AIState {
	private float initalSpeed;

	private Vector3 destination;

	private float fleeTime;

	public AIFleeState(AIStateAgent agent) : base(agent) {
		//
	}

	public override void OnEnter() {
		agent.movement.Resume();

		initalSpeed = agent.movement.maxSpeed;
		agent.movement.maxSpeed *= 2.5f;

		destination = AINavNode.GetRandomAINavNode().transform.position;

		fleeTime = Time.time + 10;
	}

	public override void OnUpdate() {
		if(Vector3.Distance(agent.transform.position, destination) < 1.0f) {
			destination = AINavNode.GetRandomAINavNode().transform.position;
		}

		GameObject[] enemies = agent.enemyPerception.GetGameObjects();
		if(Time.time >= fleeTime) {
			if(enemies.Length > 0) {
				if(enemies.Length < 3) {
					agent.stateMachine.SetState(nameof(AIAttackState));
				} else {
					agent.movement.MoveTowards(destination);
				}
			} else {
				agent.stateMachine.SetState(nameof(AIIdleState));
			}
		} else {
			agent.movement.MoveTowards(destination);
		}
	}

	public override void OnExit() {
		agent.movement.maxSpeed = initalSpeed;
	}
}
