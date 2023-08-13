using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperCreator : MonoBehaviour
{
    public GameObject shopperPrefab;

    public void createShopper(Vector3 power_position)
    {
        // 코인을 생성하고 go에 보관한다.
        GameObject go = GameObject.Instantiate(this.shopperPrefab) as GameObject;
        go.transform.position = power_position; // 블록의 위치를 이동.
    }
}
