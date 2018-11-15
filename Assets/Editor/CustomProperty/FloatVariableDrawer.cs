using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatVariable))]
public class FloatVariableDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		Rect scriptableObject = new Rect(position.x, position.y, 235, position.height);
		Rect floatValue = new Rect(position.x + 239, position.y, 50, position.height);

		property.objectReferenceValue = EditorGUI.ObjectField(scriptableObject, property.name, property.objectReferenceValue, typeof(FloatVariable), false);
		if(property.objectReferenceValue != null)
		{
			SerializedObject so = new SerializedObject(property.objectReferenceValue);
			SerializedProperty prop = so.FindProperty("value");
			prop.floatValue = EditorGUI.FloatField(floatValue, prop.floatValue);
			so.ApplyModifiedProperties();
		}
	}

}
