using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCreator : MonoBehaviour
{
    public GameObject powerPrefab;

    public void createPower(Vector3 power_position)
    {
        // ������ �����ϰ� go�� �����Ѵ�.
        GameObject go = GameObject.Instantiate(this.powerPrefab) as GameObject;
        go.transform.position = power_position; // ����� ��ġ�� �̵�.
    }
}
