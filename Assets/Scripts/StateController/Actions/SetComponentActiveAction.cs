using StateControllerManagement;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/General/Set Component Active")]
public class SetComponentActiveAction : Action
{
    [SerializeField]
    private bool activeState;

    [Tooltip("Name must be accurate.")]
    [SerializeField]
    private string componentName;

    public override void Act<T>(T controller)
    {
        Component component = controller.GetComponent(componentName);

        if (component is Component)
        {
            SetEnabled(component as Behaviour);
        }
    }
    
    public void SetEnabled<T>(T controller) where T : Behaviour
    {
        controller.enabled = activeState;
    }
}