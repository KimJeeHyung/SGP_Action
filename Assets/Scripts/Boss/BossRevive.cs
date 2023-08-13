using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRevive : MonoBehaviour
{

    [SerializeField]
    private GameObject Skill_Slot;

    [SerializeField]
    private GameObject Revive_Prefab;

    bool ONE_FRAME = true;
    GameObject Revive = null;

    // Update is called once per frame
    void Update()
    {
        if (PlayerStat.Instance.GetReviveNum() >= 1 && ONE_FRAME)
        {
            Revive = Instantiate(Revive_Prefab);
            Revive.transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + PlayerStat.Instance.GetReviveNum();
            Revive.transform.SetParent(Skill_Slot.transform);
            ONE_FRAME = false;
        } 

        if (Revive != null)
           Revive.transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + PlayerStat.Instance.GetReviveNum();

        if (PlayerStat.Instance.GetReviveNum() <= 0 && Revive != null)
        {
            Destroy(Revive.gameObject);
            Revive = null;
        }

    }
}
