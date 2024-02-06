using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateAgent : AIAgent {
	public Animator animator;

	public AIPerception enemyPerception;
	public AIPerception friendPerception;

	// Parameters
	public ValueRef<float> health = new ValueRef<float>();
	public ValueRef<float> timer = new ValueRef<float>();
	public ValueRef<float> destinationDistance = new ValueRef<float>();

	public ValueRef<bool> enemySeen = new ValueRef<bool>();
	public ValueRef<float> enemyDistance = new ValueRef<float>();
	public ValueRef<float> enemyHealth = new ValueRef<float>();

	public AIStateMachine stateMachine = new AIStateMachine();
	public AIStateAgent enemy;

	private void Start() {
		// Add states to state machine
		stateMachine.AddState(nameof(AIIdleState), new AIIdleState(this));
		stateMachine.AddState(nameof(AIDeathState), new AIDeathState(this));
		stateMachine.AddState(nameof(AIPatrolState), new AIPatrolState(this));
		stateMachine.AddState(nameof(AIAttackState), new AIAttackState(this));
		stateMachine.AddState(nameof(AIChaseState), new AIChaseState(this));
		stateMachine.AddState(nameof(AIFleeState), new AIFleeState(this));
		stateMachine.AddState(nameof(AIDanceState), new AIDanceState(this));
		stateMachine.AddState(nameof(AIWaveState), new AIWaveState(this));
		stateMachine.AddState(nameof(AIHitState), new AIHitState(this));

		stateMachine.SetState(nameof(AIIdleState));

		health.value = 100.0f;
	}

	private void Update() {
		// Update parameters
		timer.value -= Time.deltaTime;
		destinationDistance.value = Vector3.Distance(transform.position, movement.Destination);

		GameObject[] enemies = enemyPerception.GetGameObjects();
		enemySeen.value = enemies.Length > 0;
		if(enemySeen) {
			enemy = enemies[0].TryGetComponent(out AIStateAgent stateAgent) ? stateAgent : null;
			enemyDistance.value = Vector3.Distance(transform.position, enemy.transform.position);
			enemyHealth.value = enemy.health;
		}

		// From any state (health -> death)
		if(health <= 0.0f) {
			stateMachine.SetState(nameof(AIDeathState));
		}

		animator?.SetFloat("Speed", this.movement.Velocity.magnitude);

		stateMachine.Update();
	}

	private void OnGUI() {
		// Draw label of current state above agent
		GUI.backgroundColor = Color.black;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		Rect rect = new Rect(0, 0, 100, 20);
		// Get point above agent
		Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
		rect.x = point.x - (rect.width / 2);
		rect.y = Screen.height - point.y - rect.height - 20;
		// Draw label with current state name
		GUI.Label(rect, stateMachine.CurrentState.name);
	}

	public void ApplyDamage(float damage) {
		health.value -= damage;
		if(health > 0) {
			stateMachine.SetState(nameof(AIHitState));
		}
	}

	private void Attack() {
		Debug.Log("Attack");

		// Check for collision with surroundings
		var colliders = Physics.OverlapSphere(transform.position, 1);
		foreach(var collider in colliders) {
			// Don't hit self or objects with the same tag
			if(collider.gameObject == gameObject || collider.gameObject.CompareTag(gameObject.tag)) {
				continue;
			}

			// Check if collider object is a state agent, reduce health
			if(collider.gameObject.TryGetComponent<AIStateAgent>(out var stateAgent)) {
				stateAgent.ApplyDamage(Random.Range(20, 50));
			}
		}
	}
}
