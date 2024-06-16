using UnityEngine;
namespace FreeVoiceEffector
{


public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 85.0f;

    private float rotY = 0.0f; // Y축 회전
    private float rotX = 0.0f; // X축 회전

    void Start()
    {
        Vector3 rot = Camera.main.transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        // 마우스 회전
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        Camera.main.transform.rotation = localRotation;

        // WASD 이동
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * vertical + right * horizontal;
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }
}
}
