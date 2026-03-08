using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "New Generic Ability", menuName = "Scriptable Objects/Abilities/Generic Ability")]
public class GenericAbility : ScriptableObject
{
	public float magicCost;
	public float duration;

	public FloatValue playerMagic;
	public SignalObject usePlayerMagic;

	public virtual void Ability(Vector2 playerPosition, Vector2 playerFacingDirection = default,
		Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
	{

	}

}