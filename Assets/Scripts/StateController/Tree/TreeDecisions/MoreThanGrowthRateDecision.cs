using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(menuName = "Decisions/Tree/MoreThanGrowthRate")]
public class MoreThanGrowthRateDecision : Decision
{
#pragma warning disable 649
	[SerializeField] private FloatVariable growthRate;
#pragma warning restore 649
	public override bool Decide<T>(T controller)
    {
	    return IsMoreThanGrowthRate(controller as TreeStateController);
    }

	private bool IsMoreThanGrowthRate(TreeStateController controller)
	{
		if (controller.growthPercentage > growthRate.value)
		{
			controller.growthPercentage = 0;
			return true;
		}

		return false;
	}
}