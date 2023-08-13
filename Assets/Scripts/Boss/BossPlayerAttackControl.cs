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
    private CameraControl camera_control = null;

    private bool isAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        player_control = GetComponent<BossPlayerControl>();
        attackAnimClip = GetComponent<Animation>();
        boss_control = FindObjectOfType<BossControl>();
        camera_control = FindObjectOfType<CameraControl>();

        hitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAttack)
        {
            if(Time.timeScale > 0.0f)
          Time.timeScale = 1f;

            camera_control.ResetPosition();
            attackAnimClip.Play();
            StartCoroutine("Attack");
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (player_control.current_power == player_control.max_power)
            {
                player_control.current_power = 0;
                player_control.use_skill = true;
                boss_control.SkillDamage();
            }
        }
    }

    IEnumerator Attack()
    {
        isAttack = false;
        hitBox.SetActive(true);

        int index = Random.Range(0, 2);
        switch (index)
        {
            case 0:
                SoundManager.instance.PlaySE("Attack1");
                break;

            case 1:
                SoundManager.instance.PlaySE("Attack2");
                break;
        }

        yield return new WaitForSeconds(0.3f);

        hitBox.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        isAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BossAttackSequence"))
        {
            camera_control.BossAttackSequence();
        }
    }
}
