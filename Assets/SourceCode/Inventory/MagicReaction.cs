using UnityEngine;

public class MagicReaction : MonoBehaviour
{
	public FloatValue playerMagic;
	public SignalObject magicSignal;

	public void Use(int amountToIncrease)
    {
		playerMagic.runtimeValue = amountToIncrease;
		magicSignal.Raise();
    }
}