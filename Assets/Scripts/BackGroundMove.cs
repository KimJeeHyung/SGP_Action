using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{

   
    public float scrollSpeed = 1.2f;
    private Material mat;

    [SerializeField]
    private Transform Cam_Transform;



    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newOffset = mat.mainTextureOffset;
        newOffset.Set(Cam_Transform.position.x * 0.001f, 0);
        mat.mainTextureOffset = newOffset;
    }
}
