using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerAttackControl : MonoBehaviour
{
    [SerializeField]
    GameObject hitBox;

    Animation attackAnimClip;

    private BossPlayerControl player_control = null;
    private BossControl boss_control = null;

    // Start is called before the first frame update
    void Start()
    {
        player_control = GetComponent<BossPlayerControl>();
        attackAnimClip = GetComponent<Animation>();
        boss_control = FindObjectOfType<BossControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackAnimClip.Play();
            StartCoroutine("Attack");
        }

        if(Input.GetMouseButtonDown(1))
        {
            if(player_control.current_power == player_control.max_power)
            {
                player_control.current_power = 0;
                player_control.use_skill = true;
                boss_control.SkillDamage();
            }
        }
    }

    IEnumerator Attack()
    {
        hitBox.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        hitBox.SetActive(false);
    }
}
