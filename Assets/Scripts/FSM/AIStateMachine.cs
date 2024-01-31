using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine {
	private Dictionary<string, AIState> states = new Dictionary<string, AIState>();
	public AIState CurrentState {get; private set;} = null;

	public void Update() {
		CurrentState?.OnUpdate();
	}

	public void AddState(string name, AIState state) {
		Debug.Assert(!states.ContainsKey(name), "State machine already conatins state " + name);

		states[name] = state;
	}

	public void SetState(string name) {
		Debug.Assert(states.ContainsKey(name), "State machine doesn't conatin a state with name: " + name);

		AIState newState = states[name];

		// Don't re-enter state
		if(newState == CurrentState) {
			return;
		}

		CurrentState?.OnExit();
		CurrentState = newState;
		CurrentState?.OnEnter();
	}
}
