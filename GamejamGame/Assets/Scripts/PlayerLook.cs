using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Camera cam;
    private float xRot  = 0f;

    public float xSens = 30f;

    public float ySens  = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void processLook(Vector2 input)
    {
         float mouseX = input.x;
         float mouseY = input.y;

         xRot -= (mouseY * Time.deltaTime) * ySens;
         xRot = Mathf.Clamp(xRot, -80, 80);
         cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
         transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSens);
    }
}
