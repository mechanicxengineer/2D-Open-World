using UnityEngine;

[System.Serializable]
public class Loot
{
	public PowerUp powerUpLoot;
	public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
	public Loot[] loots;

	public PowerUp LootPowerUp()
    {
		int Prob = 0;
		int currentProb = Random.Range(0, 100);
		for (int i = 0; i < loots.Length; i++)
		{
			Prob += loots[i].lootChance;
			if (currentProb <= Prob)
			{
				return loots[i].powerUpLoot;
			}
		}
		return null;
    }
}