using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCreator : MonoBehaviour
{
    public GameObject powerPrefab;

    public void createPower(Vector3 power_position)
    {
        // 코인을 생성하고 go에 보관한다.
        GameObject go = GameObject.Instantiate(this.powerPrefab) as GameObject;
        go.transform.position = power_position; // 블록의 위치를 이동.
    }
}
