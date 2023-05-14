using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShop : MonoBehaviour
{
    [SerializeField]
    private GameObject Shop;


    public void Show_Shop()
    {
        Shop.SetActive(false);
        Time.timeScale = 1.0f;
        
    }
}
