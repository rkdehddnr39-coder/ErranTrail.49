using UnityEngine;
using System.Collections;
public class TeleportDoor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CanvasGroup fadeGroup;
    public float fadeDuration = 1f;
    public Transform player;
    public Transform teleportTarget;
    
    bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(FadeAndTeleport());
        }
    }

    IEnumerator FadeAndTeleport()
    {
        isTeleporting = true;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.position = teleportTarget.position;
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            player.position = teleportTarget.position;
        }

        yield return new WaitForSeconds(0.5f);

        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;

        }

        isTeleporting = false;
    }
}
