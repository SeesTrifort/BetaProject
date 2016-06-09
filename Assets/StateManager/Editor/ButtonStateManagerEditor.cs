using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(ButtonStateManager))]
public class ButtonStateManagerEditor : StateManagerEditor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();
	}
}