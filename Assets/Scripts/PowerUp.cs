using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 5f;
    public float speedMultiplier = 2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerHitBox")) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
            player.ActivarVelocidad(duration, speedMultiplier);

        Destroy(gameObject);   // Desaparece tras recogerlo
    }
}

