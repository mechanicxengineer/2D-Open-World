using System.Collections;
using UnityEngine;

public enum EnemyState
{
	idle,
	walk,
	attack,
	stagger
}

public class Enemy : MonoBehaviour
{
	[Header("State Machine")]
	public EnemyState currentState;

	[Header("Enemy Stats")]
	public FloatValue maxHealth;
	public float health;
	public string enmeyName;
	public int baseAttack;
	public float moveSpeed;

	[Header("Effects")]
	public GameObject deathEffect;
	private float deathEffectTime = 1f;

    private void Awake() => health = maxHealth.initialValue;

    private void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			//	Die
			//	TODO: The log will sleep paramanent
			DeathEffect();
			this.gameObject.SetActive(false);
		}
	}

	private void DeathEffect()
	{
		if (deathEffect != null)
		{
			GameObject Effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
			Destroy(Effect, deathEffectTime);
		}
	}

	public void KnockEnemy(Rigidbody2D rigidBody, float knockTime, float damage)
	{
		StartCoroutine(KnockCo(rigidBody, knockTime));
		TakeDamage(damage);
	}

    private IEnumerator KnockCo(Rigidbody2D rigidBody, float knockTime)
	{
		if (rigidBody != null)
		{
			yield return new WaitForSeconds(knockTime);
			rigidBody.velocity = Vector2.zero;
			currentState = EnemyState.idle;
			rigidBody.velocity = Vector2.zero;
		}
	}
}