using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Ridk
{

[CustomEditor(typeof(RidkNetwork))]
public class RidkNetworkEditor : Editor
{
	private RidkNetwork _ridkNetwork;
	private void Awake()
	{
		_ridkNetwork=target as RidkNetwork;
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.SelectableLabel("开始布局");
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
