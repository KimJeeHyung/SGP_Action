using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackCheck : MonoBehaviour
{

    private BossControl boss_control = null;
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
        if (other.CompareTag("Monster"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Boss"))
        {
            boss_control.Recovery();
        }
    }
}
