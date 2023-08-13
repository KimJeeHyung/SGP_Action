using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearText_Fadein_Fadeout : MonoBehaviour
{

    private Text text;


    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.StartCoroutine("PlayBGM","CLEAR");
        text = this.GetComponent<Text>();
        StartCoroutine("FadeTextToZeroAlpha");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("TitleScene");
    }

    public IEnumerator FadeTextToZeroAlpha()
    {
        
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }

        StartCoroutine(FadeTexttoFullAlpha());
    }

    public IEnumerator FadeTexttoFullAlpha()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }

        StartCoroutine(FadeTextToZeroAlpha());
    }
}
