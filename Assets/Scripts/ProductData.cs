using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Product",menuName = "Product Data",order =1)]
public class ProductData : ScriptableObject
{ 
    // ��ǰ ����� ����, �׸��� �̹���

    public string Product_Title;
    public string Product_Explanation;
    public Sprite Product_Image;
    public ProductType Product_Type;
    // ���� �̹����� ����
    public Sprite Coin_Image;
    public int Price_Text;
    //�춧���� ������ ��� ������ ����� ��
    public int Coin_Offset;
   //�ִ�� �� �� �ִ� Ƚ��
    public int Buy_Num;
    // ���� ��ġ
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
