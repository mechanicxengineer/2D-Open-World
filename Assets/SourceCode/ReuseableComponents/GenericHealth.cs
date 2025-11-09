using UnityEngine;

public class GenericHealth : MonoBehaviour
{
	public FloatValue maxHealth;
	public float currentHealth;

	void Start()
	{
		currentHealth = maxHealth.runtimeValue;
	}

	public virtual void Heal(float amountToHeal)
	{
		currentHealth += amountToHeal;
		if (currentHealth > maxHealth.runtimeValue)
		{
			currentHealth = maxHealth.runtimeValue;
		}
	}

	public virtual void FullHeal()
	{
		currentHealth = maxHealth.runtimeValue;
	}

	public virtual void Damage(float amountToDamage)
	{
		currentHealth -= amountToDamage;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			//Die();
		}
	}
	
	public virtual void InstantDeath()
    {
		currentHealth = 0;
    }
}