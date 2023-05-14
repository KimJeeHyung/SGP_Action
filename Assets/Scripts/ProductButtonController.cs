using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductButtonController : MonoBehaviour
{

    private int one_frame = 0;

    [SerializeField]
    private ProductData[] product_datas;

    public GameObject Product_Button_Prefab;
    public GameObject[] newObjects;

    private Text product_title;
    private Text product_explanation;
    private Image product_image;
    private Image coin_image;
    private Text price_text;

    // Start is called before the first frame update
    void Start()
    {
        newObjects = new GameObject[product_datas.Length];

        for (int i = 0; i < product_datas.Length; ++i)
        {
            newObjects[i] = Instantiate(Product_Button_Prefab);
            product_title = newObjects[i].transform.Find("Product_Title").GetComponent<Text>();
            product_explanation = newObjects[i].transform.Find("Product_Explanation").GetComponent<Text>();
            price_text = newObjects[i].transform.Find("Price_Text").GetComponent<Text>();
            product_image = newObjects[i].transform.Find("Product_Image").GetComponent<Image>();
            coin_image = newObjects[i].transform.Find("Coin_Image").GetComponent<Image>();

            product_title.text = product_datas[i].Product_Title;
            product_explanation.text = product_datas[i].Product_Explanation;
            price_text.text = product_datas[i].Price_Text.ToString();
            coin_image.sprite = product_datas[i].Coin_Image;
            product_image.sprite = product_datas[i].Product_Image;

            newObjects[i].transform.SetParent(this.gameObject.transform);
        }
    }

    void Update()
    {
        if (one_frame == 0)
        {
            for (int i = 0; i < product_datas.Length; ++i)
            {
                newObjects[i].GetComponent<ButtonClickController>().Product_Data = product_datas[i];
                newObjects[i].GetComponent<ButtonClickController>().Buy_Offset = product_datas[i].Coin_Offset;
                newObjects[i].GetComponent<ButtonClickController>().Buy_Num = product_datas[i].Buy_Num;
            }
            one_frame += 1;
        }
        
    }
}
