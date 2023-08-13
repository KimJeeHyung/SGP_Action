using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public static RankingTimer instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
           
    }

    [SerializeField]
    private float time = 0.0f;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "BossScene")
        {
            time += Time.deltaTime;
        }
    }

    public float GetTime()
    {
        return time;
    }

    public void InitTime()
    {
        time = 0.0f;
    }


}
