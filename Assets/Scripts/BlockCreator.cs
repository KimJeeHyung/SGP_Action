using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    public GameObject[] blockPrefabs; // ����� ������ �迭.
    public int block_count { get; set; } // ������ ����� ����.

    // Start is called before the first frame update
    void Start()
    {
        block_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createBlock(Vector3 block_position)
    {
        // ������ �� ����� ����(����ΰ� �������ΰ�)�� ���Ѵ�.
        //int next_block_type = this.block_count % this.blockPrefabs.Length; // % : �������� ���ϴ� ������.
        
        // ����� �����ϰ� go�� �����Ѵ�.
        GameObject go = GameObject.Instantiate(this.blockPrefabs[1]) as GameObject;
        go.transform.position = block_position; // ����� ��ġ�� �̵�.
        this.block_count++; // ����� ������ ����. 
    }

    public void createClearBlock(Vector3 block_position)
    {
        // ����� �����ϰ� go�� �����Ѵ�.
        GameObject go = GameObject.Instantiate(this.blockPrefabs[2]) as GameObject;
        go.transform.position = block_position; // ����� ��ġ�� �̵�.
        this.block_count++; // ����� ������ ����. 
    }
}
