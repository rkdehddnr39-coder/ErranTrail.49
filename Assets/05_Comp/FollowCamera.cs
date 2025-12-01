using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform target;
    public float distance;
    public float height;
    public float rotateTime;

    private float basePitch = 45f;
    private float currentYaw = 0f;
    private float targetYaw = 0f;

    void Start()
    {
        //마우스 숨기기//
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            targetYaw -= 90f;

        if (Input.GetKeyDown(KeyCode.E))
            targetYaw += 90f;

        currentYaw = Mathf.Lerp(currentYaw, targetYaw, Time.deltaTime / rotateTime);
    }

    void LateUpdate()
    {
        if (!target) return;

        // 현재 회전 각도로 방향 벡터 계산
        Quaternion rot = Quaternion.Euler(basePitch, currentYaw, 0f);
        Vector3 offset = rot * new Vector3(0, height, -distance);

        // 카메라 위치 = 플레이어 위치 + 오프셋
        transform.position = target.position + offset;

        // 플레이어 바라보기
        transform.LookAt(target.position + Vector3.up * height);
    }
}
