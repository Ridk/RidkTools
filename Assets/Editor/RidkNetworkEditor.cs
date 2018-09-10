using UnityEditor;
using UnityEngine;

namespace Ridk
{

[CustomEditor(typeof(RidkNetwork))]
public class RidkNetworkEditor : Editor
{
	private RidkNetwork _ridkNetwork;
	private void Awake()
	{
		_ridkNetwork =(RidkNetwork)serializedObject.targetObject;
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginVertical();
		GUI.backgroundColor=Color.red;
	
		EditorGUILayout.EndVertical();
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}
}

}
