using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust = 10f;
    public float knockTime = 0.3f;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }

        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            Rigidbody2D Hit = other.GetComponent<Rigidbody2D>();
            if (Hit != null)
            {
                Vector2 difference = (Hit.transform.position - transform.position).normalized;
                difference = difference.normalized * thrust;
                Hit.AddForce(difference, ForceMode2D.Impulse);
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    Hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().KnockEnemy(Hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        Hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().KnockPlayer(knockTime, damage);
                    }
                }

            }
        }
    }


}
