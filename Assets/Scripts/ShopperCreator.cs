using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperCreator : MonoBehaviour
{
    public GameObject shopperPrefab;

    public void createShopper(Vector3 power_position)
    {
        // ������ �����ϰ� go�� �����Ѵ�.
        GameObject go = GameObject.Instantiate(this.shopperPrefab) as GameObject;
        go.transform.position = power_position; // ����� ��ġ�� �̵�.
    }
}
