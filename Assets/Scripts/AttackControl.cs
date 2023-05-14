using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    [SerializeField]
    GameObject hitBox;

    Animation attackAnimClip;

    // Start is called before the first frame update
    void Start()
    {
        attackAnimClip = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackAnimClip.Play();
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        hitBox.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        hitBox.SetActive(false);
    }
}
