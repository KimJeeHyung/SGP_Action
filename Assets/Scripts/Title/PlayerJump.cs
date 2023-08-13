using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = false;

    [SerializeField]
    private float bounceForce = 7.5f; // Ƣ�� ��

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
            // Sphere�� y���� �������� �ٴ��� ���� Ƣ�� ��
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = bounceForce;
            rb.velocity = newVelocity;

            isGrounded = false;
        }
    }
}
