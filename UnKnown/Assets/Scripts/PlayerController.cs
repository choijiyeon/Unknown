using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;
    public Transform mainCamera;

    private Rigidbody2D rigid;
    private Vector3 movement;
    private bool isJumping = false;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
            moveVelocity = Vector3.left;
        else if (Input.GetAxisRaw("Horizontal") > 0)
            moveVelocity = Vector3.right;

        transform.position += moveVelocity * movePower * Time.deltaTime;
        //mainCamera.position += transform.position;
    }

    private void Jump()
    {
        if (isJumping != true)
        {
            rigid.AddForce(Vector3.up * jumpPower);

            isJumping = true;
        }
    }

    private void OnCollisionEnter2D()
    {
        Debug.LogError("jump fal;");
        isJumping = false;
    }
	
}
