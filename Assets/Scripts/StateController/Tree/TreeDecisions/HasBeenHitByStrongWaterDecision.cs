using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Decisions/Tree/HasBeenHitByStrongWaterDecision")]
public class HasBeenHitByStrongWaterDecision : Decision
{
    public override bool Decide<T>(T controller)
    {
        return HasBeenHitByStronWaterDecision(controller as TreeStateController);
    }

	private bool HasBeenHitByStronWaterDecision(TreeStateController controller)
	{
		return controller.isStronglyWatered;
	}
}