using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InspectorReadOnlyAttribute))]
public class InspectorReadOnlyDrawer : PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		//base.OnGUI(position, property, label);
		GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label);
		GUI.enabled = true;
	}
}
