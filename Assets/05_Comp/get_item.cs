using UnityEngine;

public class get_itiem : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    private Vector3 startPosition;

    bool collected;
    float t;

    private float collectTime = 0.7f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if ((!collected))
        {
            float newY = Mathf.PingPong(Time.time * frequency, amplitude);

            var pos = transform.position;
            pos.y = startPosition.y + newY;
            transform.position = pos;

            return;
        }

        t += Time.deltaTime;
        float u = Mathf.Clamp01(t / collectTime);

        float riseDistance = 1.5f;
        Vector3 endPosition = startPosition + Vector3.up * riseDistance;
        transform.position = Vector3.Lerp(startPosition, endPosition, u);

        float startSpin = 0f;
        float endSpin = 1440f;
        float currentSpinAngle = Mathf.Lerp(startSpin, endSpin, u);

        transform.rotation = Quaternion.Euler(0, currentSpinAngle, 0);

        if (u >= 1f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (other.CompareTag("Player"))
        {
            collected = true;
            t = 0f;
            startPosition = transform.position;

            GetComponent<Collider>().enabled = false;
        }
    }
}