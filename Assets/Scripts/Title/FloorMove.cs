using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    private Material mat;
    [SerializeField]
    private float Move_Speed = 4.0f;

    private float off_set = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        off_set += (Time.deltaTime * Move_Speed);
        Vector2 newOffset = mat.mainTextureOffset;
        newOffset.Set(0, off_set);
        mat.mainTextureOffset = newOffset;
    }

}
