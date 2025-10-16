using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Contents")]
    public Enemy[] enemies;
    public Pot[] pots;
    public GameObject virtualCamera;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            ActivateRoom(true);
            virtualCamera.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            ActivateRoom(false);
            virtualCamera.SetActive(false);
        }
    }

    private void ActivateRoom(bool activate)
    {
        if (enemies != null)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                    ChangeActivation(enemy, activate);
            }
        }

        if (pots != null)
        {
            foreach (Pot pot in pots)
            {
                if (pot != null)
                    ChangeActivation(pot, activate);
            }
        }
    }

    public void Onsable()
    {
        virtualCamera.SetActive(false);
    }

    public void ChangeActivation(Component component, bool activate)
    {
        component.gameObject.SetActive(activate);
    }
}
