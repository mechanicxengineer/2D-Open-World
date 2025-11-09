using UnityEngine;

public class HealthReaction : MonoBehaviour
{
	public FloatValue playerHealth;
	public SignalObject healthSignal;

	public void Use(int amountToIncrease)
    {
		playerHealth.runtimeValue = amountToIncrease;
		healthSignal.Raise();
    }
}