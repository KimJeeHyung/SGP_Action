using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat
{
    private static PlayerStat instance = null;

    private float max_hp = 100f;  // 플레이어 최대 체력
    private float current_hp = 100f; // 플레이어 현재 체력


    private int revive_num = 0; // 부활 횟수

    public static PlayerStat Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new PlayerStat();
            }

            return instance;
        }
    }

    public void SetHP(float hp)
    {
        current_hp += hp;
    }

    public float GetHP()
    {
        return current_hp;
    }

    public void SetMaxHP(float value)
    {
        max_hp += value;
    }

    public float GetMaxHP()
    {
        return max_hp;
    }

    public void SetReviveNum(int num)
    {
        revive_num += num;
        Debug.Log("현재 남아있는 부활 수" + revive_num);
    }

    public int GetReviveNum()
    {
        return revive_num;
    }

    public void ResetStat()
    {
        current_hp = max_hp;
        revive_num = 0;
    }






}
