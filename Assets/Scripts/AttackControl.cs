using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    [SerializeField]
    GameObject hitBox;

    Animation attackAnimClip;

    private bool isAttack = true;


    // Start is called before the first frame update
    void Start()
    {
        attackAnimClip = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAttack)
        {
            attackAnimClip.Play();
            StartCoroutine("Attack");
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
}
