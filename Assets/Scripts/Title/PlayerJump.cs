using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = false;

    [SerializeField]
    private float bounceForce = 7.5f; // 튀는 힘

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TitleFloor"))
        {
            isGrounded = true;
        }
    }

    private void Update()
    {
        if (isGrounded)
        {
            // Sphere의 y값을 증가시켜 바닥을 통통 튀게 함
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = bounceForce;
            rb.velocity = newVelocity;

            isGrounded = false;
        }
    }
}
