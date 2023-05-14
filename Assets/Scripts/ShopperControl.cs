using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperControl : MonoBehaviour
{
    public MapCreator map_creator = null; // MapCreator�� �����ϴ� ����.
    public GameRoot Game_Root = null;


    // Start is called before the first frame update
    void Start()
    {
        Game_Root = GameObject.Find("GameRoot").GetComponent<GameRoot>();
        // MapCreator�� �����ͼ� ��� ���� map_creator�� ����.
        map_creator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶󿡰� �� �Ⱥ��̳İ� ����� �� ���δٰ� ����ϸ�,
        if (this.map_creator.isDelete(this.gameObject))
        {
            GameObject.Destroy(this.gameObject); // �ڱ� �ڽ��� ����.
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Game_Root.Shop_Open();
            
        }
    }
}
