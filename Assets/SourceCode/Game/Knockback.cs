using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    [SerializeField] private string otherTag;
    //public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.CompareTag("Breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }
        */

        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            Rigidbody2D Hit = other.GetComponent<Rigidbody2D>();
            if (Hit != null)
            {
                Vector3 difference = (Hit.transform.position - transform.position).normalized;
                difference = difference.normalized * thrust;
                Hit.DOMove(Hit.transform.position + difference, knockTime);
                //Hit.AddForce(difference, ForceMode2D.Impulse);
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    Hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().KnockEnemy(Hit, knockTime);
                }

                PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
                if (player != null && player.currentState != PlayerState.stagger)
                {
                    Debug.Log("Player is not staggered");

                    player.currentState = PlayerState.stagger;
                    player.KnockPlayer(knockTime);
                }

            }
        }
    }
}
