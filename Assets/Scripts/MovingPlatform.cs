using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip("Puntos entre los que se moverá la plataforma")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool pingPong = true;   // ¿Vuelta atrás?

    private int current = 0;          // Índice del waypoint destino
    private int dir = 1;              // Dirección (+1 / –1)

    private void Update()
    {
        if (waypoints.Length < 2) return;

        // Desplazamiento
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[current].position,
            speed * Time.deltaTime);

        // Llegada al destino
        if (Vector2.Distance(transform.position, waypoints[current].position) < .05f)
        {
            if (pingPong)
            {
                if (current == waypoints.Length - 1) dir = -1;
                else if (current == 0) dir = 1;
                current += dir;
            }
            else
            {
                current = (current + 1) % waypoints.Length;
            }
        }
    }

    /* ---------- Transporte del jugador ---------- */
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("PlayerHitBox"))
            col.collider.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("PlayerHitBox"))
            col.collider.transform.SetParent(null);
    }
}

