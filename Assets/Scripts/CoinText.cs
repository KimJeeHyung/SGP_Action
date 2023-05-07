using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    private Text Coin_Text;
    private RecordCollector recordCollector;

    private void Start()
    {
        Coin_Text = GetComponent<Text>();
        recordCollector = GameObject.FindObjectOfType<RecordCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        Coin_Text.text = "" + recordCollector.Coin_Num;
    }
}
