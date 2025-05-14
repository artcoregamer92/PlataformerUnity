using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;
    [Header("Sistema de movimiento")]
    [SerializeField] private Transform pies;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float distanciaDeteccionSuelo;
    [SerializeField] private LayerMask queEsSaltable;

    [Header("Sistema de combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private LayerMask queEsDanhable;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        Jump();

        LanzarAtaque();
    }

    private void LanzarAtaque()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    //Se ejecuta desde evento de animaci�n.
    private void Attack()
    {
        //Lanzar trigger instantaneo.
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);
        foreach (Collider2D item in collidersTocados)
        {
            SistemaVidas sistemaVidas = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDanho(danhoAtaque);
        }

    }


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstoyEnSuelo())
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }


    private bool EstoyEnSuelo()
    {
        // Physics2D.Raycast devuelve un RaycastHit2D.
        // Ese tipo tiene un operador implícito a bool que es true si golpeó algo.
        return Physics2D.Raycast(
            pies.position,
            Vector2.down,
            distanciaDeteccionSuelo,
            queEsSaltable
        );
    }


    private void Movimiento()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(inputH * velocidadMovimiento, rb.linearVelocity.y);
        if (inputH != 0) //Hay movimiento
        {
            anim.SetBool("Running", true);
            if (inputH > 0) //Dcha
            {
                transform.eulerAngles = Vector3.zero;
            }
            else //Izq
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else //inputH = 0
        {
            anim.SetBool("Running", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }
}
