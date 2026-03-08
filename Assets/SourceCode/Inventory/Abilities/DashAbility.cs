using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New Dash Ability", menuName = "Scriptable Objects/Abilities/Dash Ability")]

public class DashAbility : GenericAbility
{
	public float dashForce;

	public override void Ability(Vector2 playerPosition, Vector2 playerFacingDirection = default,
		Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
	{
		//	make sure the player has enough magic
		if (playerMagic.runtimeValue >= magicCost)
		{
			playerMagic.runtimeValue -= magicCost;
			usePlayerMagic.Raise();
		}
		else
		{
			return;
		}

		//	dash
		if (playerRigidbody != null)
		{
			Vector3 dashVector = playerRigidbody.transform.position + (Vector3) playerFacingDirection.normalized * dashForce;
			playerRigidbody.DOMove(dashVector, duration).SetEase(Ease.OutQuint);
		}
	}
}