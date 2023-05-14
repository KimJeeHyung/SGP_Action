using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreator : MonoBehaviour
{
    public GameObject monsterPrefab; // ���� ������.
    public int monster_count { get; set; } // ������ ������ ����.

    private void Start()
    {
        monster_count = 0;
    }

    public void createMonster(Vector3 monster_position)
    {
        // ������ �����ϰ� go�� �����Ѵ�.
        GameObject go = GameObject.Instantiate(this.monsterPrefab) as GameObject;
        go.transform.position = monster_position; // ����� ��ġ�� �̵�.
        monster_count++;
    }
}
