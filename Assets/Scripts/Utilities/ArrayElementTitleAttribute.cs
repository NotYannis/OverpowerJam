using UnityEngine;

public class ArrayElementTitleAttribute : PropertyAttribute
{
    public string Varname;
    public ArrayElementTitleAttribute(string ElementTitleVar)
    {
        Varname = ElementTitleVar;
    }
}

public class FloatVariableAttribute : PropertyAttribute
{
    public FloatVariableAttribute()
    {
    }
}