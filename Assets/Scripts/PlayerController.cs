using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int velocidad;
    public int fuerzaSalto;
    
    private Rigidbody2D fisica;

    private void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float entradaX = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(entradaX * velocidad, fisica.velocity.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && tocandoSuelo())
        {
            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    private bool tocandoSuelo()
    {
        RaycastHit2D toca = Physics2D.Raycast(transform.position + new Vector3(0,-2f,0), Vector2.down, 0.2f);
        return toca.collider != null;
    }
}
