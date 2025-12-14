using UnityEngine;

public class get_itiem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float amplitude;
    public float frequency; 
    private Vector3 startPosition;

    bool collected;
    float t;

    private float collectTime = 1f;
    private float rise = 1f;
    private float duration = 1f;


    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
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
        transform.position += Vector3.up * (rise * Time.deltaTime);

        float spin = Mathf.Lerp(360f , 1440f, u);
        transform.Rotate(Vector3.up, spin * Time.deltaTime, Space.World);

        if (t >= duration)
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
