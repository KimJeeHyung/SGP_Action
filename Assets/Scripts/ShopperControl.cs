using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopperControl : MonoBehaviour
{
    public MapCreator map_creator = null; // MapCreator�� �����ϴ� ����.
    public GameRoot Game_Root = null;

    private Canvas Shop_Canvas = null;
    private Text Shop_Text;



    // Start is called before the first frame update
    void Start()
    {
        Shop_Canvas = this.transform.GetChild(0).gameObject.GetComponent<Canvas>();

        Shop_Text = Shop_Canvas.transform.GetChild(0).gameObject.GetComponent<Text>();
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

        Shop_Text.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0.0f, 1.5f, 0.0f));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Game_Root.Shop_Open();
            
        }
    }
}
