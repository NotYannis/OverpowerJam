using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Actions/Tree/UngrowAction")]
public class UngrowAction : Action
{
    public override void Act<T>(T controller)
    {
		UngrowTree(controller as TreeStateController);
    }

	private void UngrowTree(TreeStateController controller)
	{
		controller.Ungrow();
	}
}