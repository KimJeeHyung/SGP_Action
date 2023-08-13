using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileController : MonoBehaviour
{
    
    private LineRenderer missile_line;
    private float currentTime = 0.0f;


    [SerializeField]
    private PlayerControl Player_Control;


    [SerializeField]
    private float missile_random = 0.0f;


    [SerializeField]
    private Image warningSign;


    private float missileSpeed = 5.0f;

    [SerializeField]
    private float warningTime = 11.0f;

    [SerializeField]
    private Transform player;


     
    [SerializeField]
    private GameObject missile;

    public MapCreator map_creator = null;
    public float power = 5f;

    // Start is called before the first frame update
    void Start()
    {
        map_creator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
        missile_line = this.GetComponent<LineRenderer>();
        missile_line.startWidth = 0.1f;
        missile_line.endWidth = 0.07f;
        missile_line.enabled = false;
        warningSign.enabled = false;
 
        
    }

    // Update is called once per frame
    void Update()
    {
        missile_line.SetPosition(0, player.position + new Vector3(20.0f,missile_random,0.0f));
        missile_line.SetPosition(1, player.position);
        warningSign.transform.position = Camera.main.WorldToScreenPoint(player.position + new Vector3(10.0f, missile_random, 0.0f));

        if(Player_Control.current_level >= 1)
        TryWarning();

        if (Player_Control.current_level == 2)
            missileSpeed = 7.0f;
    }

    void TryWarning()
    {
        currentTime += Time.deltaTime;
        if(currentTime > warningTime)
        {
            missile_random = Random.Range(0.0f, 2.0f);
            StartCoroutine("Warning",missile_random);
            currentTime = 0.0f;
        }

      
    }

    public IEnumerator Warning(float random)
    {
        /*int count = 0;
        while (count < 5)
        {
            missile_line.enabled = !missile_line.enabled;
            yield return new WaitForSeconds(1.0f);
            count++;
        }*/


        missile_line.enabled = !missile_line.enabled;
        warningSign.enabled = !warningSign.enabled;
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 9; ++i)
        {          
            warningSign.enabled = !warningSign.enabled;
            missile_line.enabled = !missile_line.enabled;
            yield return new WaitForSeconds(0.25f);
        }

        Fire(random);
    }

    public void Fire(float random)
    {
        GameObject fire = Instantiate(missile, player.position + new Vector3(12, random, 0), Quaternion.identity);

        Rigidbody fRigid = fire.GetComponent<Rigidbody>();

        Vector3 player_original = player.transform.position;

        Vector3 dir = (player_original - fire.transform.position).normalized;

        fRigid.AddForce(dir.normalized * power, ForceMode.Impulse);

        SoundManager.instance.PlaySE("Fire");

        //while (!this.map_creator.isDelete(fire))
        //{
        //    fire.transform.Translate(dir * Time.deltaTime * missileSpeed);
        //    yield return null;

        //    if (this.map_creator.isDelete(fire))
        //        Destroy(fire.gameObject);
        //}
    }
}
