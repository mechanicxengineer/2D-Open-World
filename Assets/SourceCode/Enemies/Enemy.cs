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
    public string enemyName; // ✅ Fixed typo
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePosition;

    [Header("Effects")]
    public GameObject deathEffect;
    private float deathEffectTime = 1f;
    public LootTable lootTable;

    [Header("Death Signals")]
    public SignalObject roomSignal;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    public virtual void OnEnable()
    {
        transform.position = homePosition;
        health = maxHealth.initialValue;
        currentState = EnemyState.idle;
    }

    protected virtual void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        if (health <= 0)
        {
            DeathEffect();
            roomSignal?.Raise();
            MakeLoot();
            gameObject.SetActive(false);
        }
    }

    private void MakeLoot()
    {
        if (lootTable != null)
        {
            PowerUp current = lootTable.LootPowerUp();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectTime);
        }
    }

    public void KnockEnemy(Rigidbody2D rigidBody, float knockTime)
    {
        if (rigidBody != null && gameObject.activeInHierarchy)
        {
            StartCoroutine(KnockCo(rigidBody, knockTime));
        }
    }

    private IEnumerator KnockCo(Rigidbody2D rigidBody, float knockTime)
    {
        if (rigidBody != null)
        {
            currentState = EnemyState.stagger;
            yield return new WaitForSeconds(knockTime);
            rigidBody.linearVelocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }
}
