using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	Camera cam;

	[SerializeField]
	NavMeshAgent agent;

	[SerializeField]
	Transform target = null;

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		if(target == null) {
			if(Input.GetMouseButton(0)) {
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray, out RaycastHit hit)) {
					agent.SetDestination(hit.point);
				}
			}
		} else {
			agent.SetDestination(target.position);
		}
	}
}