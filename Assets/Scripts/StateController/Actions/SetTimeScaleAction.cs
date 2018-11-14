using UnityEngine;
using StateControllerManagement;

[CreateAssetMenu(fileName = "SetTimeScale_Action", menuName = "Actions/General/Set Time Scale Action")]
public class SetTimeScaleAction : Action 
{
    [SerializeField]
    float timeScale;

	public override void Act<T>(T controller)
	{
        Time.timeScale = timeScale;
	}
}