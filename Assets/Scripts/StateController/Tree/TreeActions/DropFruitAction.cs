using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Actions/Tree/DropFruitAction")]
public class DropFruitAction : Action
{
    public override void Act<T>(T controller)
    {
		DropFruits(controller as TreeStateController);
    }

	private void DropFruits(TreeStateController controller)
	{
		controller.DropFruits();
	}
}