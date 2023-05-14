using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceData : MonoBehaviour
{
    static public DistanceData instance;

    public float data { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        data = 0.0f;
    }
}
