using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUtilityWaypoints : MonoBehaviour {
	[SerializeField] AIUtilityAgent agent;
	[SerializeField] float moveTimer;
	[SerializeField] Transform[] waypoints;

	Coroutine timerCR = null;

	void Update() {
		// stop coroutine if using utility object
		if(agent.activeUtilityObject != null && timerCR != null) {
			StopCoroutine(timerCR);
			timerCR = null;
		} else if(timerCR == null) {
			// start coroutine if not using utility object and coroutine has not been started
			timerCR = StartCoroutine(MoveToRandomWaypoint(moveTimer));
		}
	}

	IEnumerator MoveToRandomWaypoint(float timer) {
		// wait for seconds (timer)
		yield return new WaitForSeconds(timer);

		Vector3 waypoint = waypoints[Random.Range(0, waypoints.Length)].position;
		while(waypoint == agent.movement.Destination) {
			waypoint = waypoints[Random.Range(0, waypoints.Length)].position;
		}

		agent.movement.MoveTowards(waypoint);

		// wait until distance between agent position to agent movement destination is < 1
		yield return new WaitUntil(() => Vector3.Distance(agent.transform.position, agent.movement.Destination) < 1.0f);

		timerCR = null;
	}
}
