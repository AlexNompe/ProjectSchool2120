using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        rb.transform.position += new Vector3(moveVelocity.x, 0, moveVelocity.y) * Time.deltaTime;
    }

    void Update()
    {
        
    }
}
