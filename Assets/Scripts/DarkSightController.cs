using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DarkSightController : MonoBehaviour
{
    public PostProcessProfile ppf;

    Vignette tmp;

    [SerializeField]
    private GameObject m_processing;

    [SerializeField]
    private PlayerControl playerControl;




    // Start is called before the first frame update
    void Start()
    {
        ppf = m_processing.GetComponent<PostProcessVolume>().profile;
        ppf.TryGetSettings<Vignette>(out tmp);
        tmp.intensity.value = 0.0f;
        tmp.enabled.value = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl.current_level == 1 && playerControl.clearBlockCount >= 40)
        {
            tmp.enabled.value = true;
            tmp.intensity.value += (Time.deltaTime * 0.15f);
            tmp.intensity.value = Mathf.Clamp(tmp.intensity.value, 0.0f, 0.35f);
            //Debug.Log("시야를 어둡게 해보겠습니다");
        }
    }
}
