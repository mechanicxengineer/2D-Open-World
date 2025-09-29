using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust = 10f;
    public float knockTime = 0.3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 difference = (enemyRb.transform.position - transform.position).normalized;
                enemyRb.AddForce(difference * thrust, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemyRb));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemyRb)
    {
        if (enemyRb != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemyRb.velocity = Vector2.zero;

        }
    }
}
