using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject fireballPrefab;
    public int initialPoolSize = 10;
    private List<GameObject> fireballPool;


    private void Start()
    {
        // Object Pool �ʱ�ȭ
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject fireball = Instantiate(fireballPrefab);
            fireball.SetActive(false);
            fireballPool.Add(fireball);
        }
    }

    public GameObject GetFireball()
    {
        foreach (GameObject fireball in fireballPool)
        {
            if (!fireball.activeInHierarchy)
            {
                fireball.SetActive(true);
                return fireball;
            }
        }

        // Pool�� ��Ȱ��ȭ�� Fireball�� ���� ��� ���� ����
        GameObject newFireball = Instantiate(fireballPrefab);
        fireballPool.Add(newFireball);
        return newFireball;
    }


    public void ReturnFireball(GameObject fireball)
    {
        fireball.SetActive(false);
    }

}
