using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordCollector : MonoBehaviour
{
    public float Run_Distance = 0.0f;
    public int Coin_Num = 0;

    private DistanceData distanceData;

    [SerializeField]
    private Transform Cam_Position;

    private void Start()
    {
        distanceData = GameObject.FindObjectOfType<DistanceData>();  
    }

    // Update is called once per frame
    void Update()
    {
        distanceData.data = Cam_Position.position.x;
    }
}
