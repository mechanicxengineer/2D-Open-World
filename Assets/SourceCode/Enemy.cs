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
	public EnemyState currentState;
	public FloatValue maxHealth;
	public float health;
	public string enmeyName;
	public int baseAttack;
	public float moveSpeed;

    private void Awake()
    {
		health = maxHealth.initialValue;
    }

	private void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			//	Die
			//	TODO: The log will sleep paramanent
			Debug.Log(enmeyName + " has died");
			this.gameObject.SetActive(false);
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