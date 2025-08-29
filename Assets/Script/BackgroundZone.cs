using UnityEngine;

public class BackgroundZone : MonoBehaviour
{
    public bool isCave;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            AudioManager.Instance?.ZoneEnter(isCave);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            AudioManager.Instance?.ZoneExit(isCave);
    }
}
