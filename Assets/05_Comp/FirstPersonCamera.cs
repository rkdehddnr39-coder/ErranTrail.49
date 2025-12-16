using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    private float mouseSensitivity = 100f;
    public Transform target;
    private float cameraPostionY = 2.25f;
    private float cameraPostionZ = 1.5f;
    private float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (target == null) return;


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);


        //마우스X에 따라서 Y축기준으로 회전//
        target.Rotate(Vector3.up * mouseX);

        transform.position = target.position + Vector3.up * cameraPostionY;
        transform.position += target.forward * cameraPostionZ;
    }
}
