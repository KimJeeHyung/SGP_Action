using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChanger : MonoBehaviour
{
    public LevelControl level_control;

    [SerializeField]
    private float fadeDuration = 2.0f;

    [SerializeField]
    private GameObject transparent_quad;


    // Update is called once per frame
    void Update()
    {
        switch (level_control.level)
        {
            case 1:
                this.transform.GetChild(0).gameObject.SetActive(false);
                if (this.transform.GetChild(0).gameObject.activeSelf == false &&
                    this.transform.GetChild(1).gameObject.activeSelf == false)
                {
                    this.transform.GetChild(1).gameObject.SetActive(true);
                    StartCoroutine("ChangeBackGround");
                }
                break;
            case 2:
                this.transform.GetChild(1).gameObject.SetActive(false);
                if (this.transform.GetChild(1).gameObject.activeSelf == false &&
                    this.transform.GetChild(2).gameObject.activeSelf == false)
                {
                    this.transform.GetChild(2).gameObject.SetActive(true);
                    StartCoroutine("ChangeBackGround");
                }
                break;
            case 3:
                this.transform.GetChild(2).gameObject.SetActive(false);
                if (this.transform.GetChild(2).gameObject.activeSelf == false &&
                    this.transform.GetChild(3).gameObject.activeSelf == false)
                {
                    this.transform.GetChild(3).gameObject.SetActive(true);
                    StartCoroutine("ChangeBackGround");
                }
                break;

            default:
                break;
        }
    }


    /*public IEnumerator ChangeBackGround(GameObject BackGrounds)
    {
        int backgroundCount = BackGrounds.transform.childCount;
        float elapsedTime = 0f;
        float startCutoff = 1f;
        float targetCutoff = 0f;

        Material[] materials = new Material[backgroundCount];
        GameObject[] background = new GameObject[backgroundCount];

        for(int i = 0; i < backgroundCount; ++i)
        {
            background[i] = BackGrounds.transform.GetChild(i).gameObject;
            materials[i] = background[i].GetComponent<Renderer>().material;
        }

        BackGrounds.SetActive(true);

        while(elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;

            foreach (Material material in materials)
            {
                material.SetFloat("_Cutoff", Mathf.Lerp(targetCutoff, startCutoff, t));
                Debug.Log("현재 alpha 값은 :" + Mathf.Lerp(targetCutoff, startCutoff, t));
            }
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        foreach (Material material in materials)
        {
            material.SetFloat("_Cutoff", startCutoff);
        }

    }*/

    public IEnumerator ChangeBackGround()
    {

        float elapsedTime = 0.0f;
        transparent_quad.SetActive(true);
        Material material = transparent_quad.GetComponent<Renderer>().material;
        Color originColor = material.color;
        Color targetColor = new Color(originColor.r, originColor.g, originColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            material.color = Color.Lerp(originColor, targetColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transparent_quad.SetActive(false);
        material.color = new Color(originColor.r, originColor.g, originColor.b, 1f);
    }

}
