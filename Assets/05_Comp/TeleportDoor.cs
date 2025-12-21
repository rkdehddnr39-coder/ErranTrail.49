using UnityEngine;
using System.Collections;
public class TeleportDoor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CanvasGroup fadeGroup;
    public float fadeDuration = 1f;
    public Transform player;
    public Transform teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeAndTeleport());
        }
    }

    IEnumerator FadeAndTeleport()
    {
        // 1. 화면 어둡게 만들기 (Fade Out)
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        // 2. 위치 이동
        player.position = teleportTarget.position;

        // 잠시 대기 (이동 후 로딩 시간을 연출하고 싶다면)
        yield return new WaitForSeconds(0.5f);

        // 3. 화면 밝게 만들기 (Fade In)
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }
    }
}
