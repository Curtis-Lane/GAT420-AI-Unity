using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AINavAgent))]
public class AINavPath : MonoBehaviour {
	public enum PathType {
		Waypoint,
		Dijkstra,
		AStar
	}

	[SerializeField]
	private AINavAgent agent;

	[SerializeField]
	private PathType pathType;

	List<AINavNode> path = new List<AINavNode>();

	public AINavNode targetNode {get; set;} = null;
	public Vector3 destination {
		get {
			return (targetNode != null) ? targetNode.transform.position : Vector3.zero;
		} set {
			if(pathType == PathType.Waypoint) {
				targetNode = agent.GetNearestAINavNode(value);
			} else if(pathType == PathType.Dijkstra || pathType == PathType.AStar) {
				AINavNode startNode = agent.GetNearestAINavNode();
				AINavNode endNode = agent.GetNearestAINavNode(value);

				GeneratePath(startNode, endNode);
				targetNode = startNode;
			}
		}
	}

	private void Start() {
		//agent = GetComponent<AINavAgent>();
		//targetNode = (startNode != null) ? startNode : AINavNode.GetRandomAINavNode();
	}

	public bool HasTarget() {
		return targetNode != null;
	}

	public AINavNode GetNextAINavNode(AINavNode node) {
		if(pathType == PathType.Waypoint) {
			return node.GetRandomNeighbor();
		} else if(pathType == PathType.Dijkstra || pathType == PathType.AStar) {
			return GetNextPathAINavNode(node);
		}

		return null;
	}

	private void GeneratePath(AINavNode startNode, AINavNode endNode) {
		AINavNode.ResetNodes();
		if(pathType == PathType.Dijkstra) {
			AINavDijkstra.Generate(startNode, endNode, ref path);
		} else if(pathType == PathType.AStar) {
			AINavAStar.Generate(startNode, endNode, ref path);
		}
	}

	private AINavNode GetNextPathAINavNode(AINavNode node) {
		if(path.Count == 0) {
			return null;
		}

		int index = path.FindIndex(pathNode => pathNode == node);

		// If not found or past the end, return null
		if(index == -1 || ((index + 1) == path.Count)) {
			return null;
		}

		// Get next node in path
		AINavNode nextNode = path[index + 1];
		
		return nextNode;
	}

	private void OnDrawGizmosSelected() {
		if(path.Count == 0) {
			return;
		}

		var pathArray = path.ToArray();

		for(int i = 1; i < path.Count - 1; i++) {
			Gizmos.color = Color.black;
			Gizmos.DrawSphere(pathArray[i].transform.position + Vector3.up, 1);
		}

		Gizmos.color = Color.green;
		Gizmos.DrawSphere(pathArray[0].transform.position + Vector3.up, 1);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(pathArray[pathArray.Length - 1].transform.position + Vector3.up, 1);
	}

	/*
	public AINavNode GetNearestAINavNode()
	{
		var nodes = AINavNode.GetAINavNodes().ToList();
		SortAINavNodesByDistance(nodes);

		return (nodes.Count == 0) ? null : nodes[0];
	}

	public void SortAINavNodesByDistance(List<AINavNode> nodes)
	{
		nodes.Sort(CompareDistance);
	}

	public int CompareDistance(AINavNode a, AINavNode b)
	{
		float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
		float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
		return squaredRangeA.CompareTo(squaredRangeB);
	}
	*/
}
