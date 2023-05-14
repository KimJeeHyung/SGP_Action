using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCreator : MonoBehaviour
{
    public GameObject coinPrefab; // 코인 프리팹.

    public void createCoin(Vector3 coin_position)
    {
        // 코인을 생성하고 go에 보관한다.
        GameObject go = GameObject.Instantiate(this.coinPrefab) as GameObject;
        go.transform.position = coin_position; // 블록의 위치를 이동.
    }
}
