using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public float xRotation = 0f;

    public float xSensitivity, ySensitivity = 30f;
    
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime)* xSensitivity);

    }
}
