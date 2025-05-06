using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;
    
    public void RecibirDanho(float danhoRecibido)
    {
        vidas = danhoRecibido;
        if (vidas <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
