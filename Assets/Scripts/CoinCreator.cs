using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCreator : MonoBehaviour
{
    public GameObject coinPrefab; // ���� ������.

    public void createCoin(Vector3 coin_position)
    {
        // ������ �����ϰ� go�� �����Ѵ�.
        GameObject go = GameObject.Instantiate(this.coinPrefab) as GameObject;
        go.transform.position = coin_position; // ����� ��ġ�� �̵�.
    }
}
