using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    [Header("Room Doors")]
    public Door[] doors;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }

            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            CloseDoors();
            virtualCamera.SetActive(true);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }

            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            virtualCamera.SetActive(false);
        }
    }

    public void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy && i < enemies.Length - 1)
            {
                return;
            }
            OpenDoors();
        }
    }

    public void CloseDoors()
    {
        if (doors != null)
        {
            foreach (Door door in doors)
            {
                door.Close();
            }
        }
    }

    public void OpenDoors()
    {
        if (doors != null)
        {
            foreach (Door door in doors)
            {
                door.Open();
            }
        }
    }
}
