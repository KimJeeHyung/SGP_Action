using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackCheck : MonoBehaviour
{
    private BossControl boss_control = null;

    [SerializeField]
    private int collision_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            boss_control = FindObjectOfType<BossControl>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") || other.CompareTag("Fireball"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Boss"))
        {
            collision_count++;
            boss_control.Recovery();
            if (collision_count == 2)
            {
                boss_control.HP -= 20f;
                Debug.Log("보스 체력 : " + boss_control.HP);
                collision_count = 0;
            }
        }
    }
}
