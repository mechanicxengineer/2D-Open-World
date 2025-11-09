using UnityEngine;

public class PlayerHealth : GenericHealth
{
	[SerializeField] private SignalObject healthSignal;

	public override void Damage(float damage)
	{
		base.Damage(damage);
		maxHealth.runtimeValue = currentHealth;
		healthSignal.Raise();
	}
}