using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Product",menuName = "Product Data",order =1)]
public class ProductData : ScriptableObject
{ 
    // 상품 제목과 설명, 그리고 이미지

    public string Product_Title;
    public string Product_Explanation;
    public Sprite Product_Image;
    public ProductType Product_Type;
    // 코인 이미지와 가격
    public Sprite Coin_Image;
    public int Price_Text;
    //살때마다 가격을 계속 오르게 만드는 값
    public int Coin_Offset;
   //최대로 살 수 있는 횟수
    public int Buy_Num;
    // 증가 수치
    public int increase;

    public float Cool_Time;

    public float Current_Cool_Time;

    public string Key_Code;

    public enum ProductType
    {
        Passive,
        Use,
        Skill,
        ETC
    }




}
