using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ArrayElementTitleAttribute))]
public class ArrayElementTitleDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                    GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
    protected virtual ArrayElementTitleAttribute Attribute
    {
        get { return (ArrayElementTitleAttribute)attribute; }

    }
    SerializedProperty TitleNameProp;

    public override void OnGUI(Rect position,
                              SerializedProperty property,
                              GUIContent label)
    {
       string newlabel = Attribute.Varname;
        if (string.IsNullOrEmpty(newlabel))
            newlabel = label.text;
        EditorGUI.PropertyField(position, property, new GUIContent(newlabel, label.tooltip), true);
    }
}

[CustomPropertyDrawer(typeof(FloatVariableAttribute))]
public class FloatVariableDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label); 
    }
}