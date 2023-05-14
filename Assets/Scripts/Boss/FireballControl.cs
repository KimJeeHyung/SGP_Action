using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballControl : MonoBehaviour
{
    public BossMapCreator map_creator = null; // MapCreator를 보관하는 변수.

    // Start is called before the first frame update
    void Start()
    {
        // MapCreator를 가져와서 멤버 변수 map_creator에 보관.
        map_creator = GameObject.Find("GameRoot").GetComponent<BossMapCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라에게 나 안보이냐고 물어보고 안 보인다고 대답하면,
        if (this.map_creator.isDelete(this.gameObject))
        {
            GameObject.Destroy(this.gameObject); // 자기 자신을 삭제.
        }
    }
}
