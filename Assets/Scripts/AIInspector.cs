using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIInspector : EditorWindow {
	Vector3 defaultCameraPos;
	Quaternion defaultCameraRot;

	[MenuItem("AI/Inspector")]
	static void ShowWindow() {
		EditorWindow.GetWindow(typeof(AIInspector));
	}

	private void OnEnable() {
		defaultCameraPos = Camera.main.transform.position;
		defaultCameraRot = Camera.main.transform.rotation;
	}

	private void OnGUI() {
		//GUILayout.BeginHorizontal();
		GUILayout.Label("Actions", EditorStyles.boldLabel);

		if(GUILayout.Button("View Agent")) {
			GameObject go = Selection.activeGameObject;
			Camera camera = Camera.main;
			if(go != null) {
				if(go.TryGetComponent(out AIAgent agent)) {
					camera.transform.parent = agent.transform;
					camera.transform.localPosition = (Vector3.back * 5) + (Vector3.up * 2);
					camera.transform.localRotation = Quaternion.identity;
				}
			} else {
				camera.transform.parent = null;
				camera.transform.position = defaultCameraPos;
				camera.transform.rotation = defaultCameraRot;
			}
		}
		//GUILayout.EndHorizontal();
	}
}
