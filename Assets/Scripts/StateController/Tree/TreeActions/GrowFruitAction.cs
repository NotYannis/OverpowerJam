using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Actions/Tree/GrowFruitAction")]
public class GrowFruitAction : Action
{
    public override void Act<T>(T controller)
    {
		GrowFruit(controller as TreeStateController);
    }

	private void GrowFruit(TreeStateController controller)
	{
		controller.GrowFruits();
	}
}