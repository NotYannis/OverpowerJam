using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerLevelStats))]
public class PlayerLevelStatsEditor : Editor
{
	private PlayerLevelStats playerStats;

	private void OnEnable()
	{
		playerStats = (PlayerLevelStats) target;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		Rect scale = GUILayoutUtility.GetLastRect();
		scale.y += scale.height + 5;
		scale.height = 30;
		if (GUI.Button(scale, "Update"))
		{
			if(playerStats.OnUpdate != null)
				playerStats.OnUpdate.Invoke();
		}
	}

}
