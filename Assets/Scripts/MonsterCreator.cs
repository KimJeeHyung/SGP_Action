using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreator : MonoBehaviour
{
    public GameObject monsterPrefab; // 몬스터 프리팹.
    public int monster_count { get; set; } // 생성한 몬스터의 개수.

    private void Start()
    {
        monster_count = 0;
    }

    public void createMonster(Vector3 monster_position)
    {
        // 코인을 생성하고 go에 보관한다.
        GameObject go = GameObject.Instantiate(this.monsterPrefab) as GameObject;
        go.transform.position = monster_position; // 블록의 위치를 이동.
        monster_count++;
    }
}
