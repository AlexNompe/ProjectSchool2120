using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float camScroll;

    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private float camInput = 0;
    public float camVelocity;

    private Rigidbody rb;
    private GameObject cam;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = transform.Find("Main Camera").gameObject;
    }

    private void FixedUpdate()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        rb.transform.position += rb.transform.right * (Greater0(moveVelocity.x) * Time.deltaTime) + rb.transform.right * (Lesser0(moveVelocity.x) * Time.deltaTime) + rb.transform.forward * (Greater0(moveVelocity.y) * Time.deltaTime) + rb.transform.forward * (Lesser0(moveVelocity.y) * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q)) camInput = -1;
        else if (Input.GetKey(KeyCode.E)) camInput = 1;
        else camInput = 0;

        camVelocity = camInput * camScroll;

        rb.transform.Rotate(new Vector3(0, 1, 0) * camVelocity * Time.deltaTime);
    }

    void Update()
    {
        
    }

    public float Greater0(float i)
    {
        if(i > 0) return i;
        else return 0;
    }

    public float Lesser0(float i)
    {
        if (i < 0) return i;
        else return 0;
    }
}
