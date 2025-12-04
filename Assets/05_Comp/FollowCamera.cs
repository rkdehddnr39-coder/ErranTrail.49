using Unity.VisualScripting;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform target;
    public float distance;
    public float height;
    public float rotateTime;

    public float pitch;
    private float currentYaw = 0f;
    public float targetYaw;

    private float scrollSpeed = 10f;

    private float targetHeight;
    private float targetDistance;
    private float targetPitch;

    void Start()
    {
        //마우스 숨기기//
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentYaw = targetYaw;

        targetHeight = height;
        targetDistance= distance;
        targetPitch = pitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            targetYaw -= 45f;

        if (Input.GetKeyDown(KeyCode.E))
            targetYaw += 45f;

        currentYaw = Mathf.Lerp(currentYaw, targetYaw, Time.deltaTime * rotateTime);


        float scroll = Input.GetAxis("Mouse ScrollWheel");


        if (scroll != 0)
        {
            targetHeight -= scroll * 1f;     
            targetDistance -= scroll * 4f;   
            targetPitch -= scroll * 5f;

            targetHeight = Mathf.Clamp(targetHeight, 1f, 4f);
            targetDistance = Mathf.Clamp(targetDistance, 4f, 16f);
            targetPitch = Mathf.Clamp(targetPitch, 5f, 20f);
        }

        height = Mathf.Lerp(height, targetHeight, Time.deltaTime * scrollSpeed);
        distance = Mathf.Lerp(distance, targetDistance, Time.deltaTime * scrollSpeed);
        pitch = Mathf.Lerp(pitch, targetPitch, Time.deltaTime * scrollSpeed);

    }


    void LateUpdate()
    {
        if (!target) return;

        // 현재 회전 각도로 방향 벡터 계산
        Quaternion rot = Quaternion.Euler(pitch, currentYaw, 0f);
        Vector3 offset = rot * new Vector3(0f, height, -distance);

        // 카메라 위치 = 플레이어 위치 + 오프셋
        transform.position = target.position + offset;

        // 플레이어 바라보기
        transform.LookAt(target.position + Vector3.up * height);
    }
}
