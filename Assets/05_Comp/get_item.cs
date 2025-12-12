using UnityEngine;

public class get_itiem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float amplitude;
    public float frequency; 
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.PingPong(Time.time * frequency, amplitude);

        var pos = transform.position;
        pos.y = startPosition.y + newY;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
