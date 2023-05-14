using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceText : MonoBehaviour
{
    private Text Distance_Text;
    private DistanceData distanceData;

    public float Distance { get; set; }

    private void Start()
    {
        Distance_Text = GetComponent<Text>();
        distanceData = GameObject.FindObjectOfType<DistanceData>();
    }

    // Update is called once per frame
    void Update()
    {
       Distance_Text.text = "" + distanceData.data.ToString("0.00") + 'm';
    }
}
