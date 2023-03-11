using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float camScroll;
    [Range(0f, 2f)]
    public float sence;
    private float senseMultiplyer = 90;

    public Vector2 mousePos;
    public Vector2 windowSize;
    public float cameraYRotation;
    public float cameraXRotation;
    public float realCameraXrot;
    public float normalizedCameraXrot;

    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private float camInput = 0;
    private float camVelocity;

    private Rigidbody rb;
    private GameObject cam;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = transform.Find("Main Camera").gameObject;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        //rb.transform.rotation = Quaternion.Euler(new Vector3(0, cameraYRotation, 0));
        //cam.transform.localRotation = Quaternion.Euler(new Vector3(360 - realCameraXrot, 0, 0));
        //rb.transform.Rotate(new Vector3(0, 1, 0) * camVelocity * Time.deltaTime);
    }

    void Update()
    {
        mousePos = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        windowSize = new Vector2(UnityEngine.Screen.width, UnityEngine.Screen.height);
        cameraYRotation = rb.transform.localEulerAngles.y;
        cameraXRotation = cam.transform.localEulerAngles.x;

        normalizedCameraXrot = NormalizeCameraXRotation(cam.transform.localRotation.eulerAngles.x);
        realCameraXrot = cam.transform.rotation.normalized.eulerAngles.x;

        rb.transform.Rotate(new Vector3(0, mousePos.y, 0) * Time.deltaTime * sence * senseMultiplyer);
        cam.transform.Rotate(new Vector3(-mousePos.x, 0, 0) * Time.deltaTime * sence * senseMultiplyer);
        cam.transform.localRotation = Quaternion.Euler(PreventProperCameraXRotation(NormalizeCameraXRotation(cam.transform.localRotation.eulerAngles.x)), cam.transform.localRotation.eulerAngles.y, cam.transform.localRotation.eulerAngles.z);
    }

    public float NormalizeCameraXRotation(float input)
    {
        if (input > 90)
        {
            return input - 360;
        }
        else return input;
        return input;
    }
    public float PreventProperCameraXRotation(float input)
    {
        if (input > 90)
        {
            return 90;
        }
        else if (input < -90)
        {
            return -90;
        }
        else return input;
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
