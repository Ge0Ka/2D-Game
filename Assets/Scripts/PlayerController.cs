using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int velocidad;
    public int fuerzaSalto;
    
    private Rigidbody2D fisica;
    private SpriteRenderer sprite;
    private Animator animacion;

    private void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animacion = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        
        float entradaX = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(entradaX * velocidad, fisica.velocity.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TouchFloor())
        {
            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.D)) sprite.flipX = false;
        else if(Input.GetKeyDown(KeyCode.A)) sprite.flipX = true;

        animarJugador();
    }

    //TO DO: añadir la animacion de bajada en el salto
    private void animarJugador()
    {
        if (!TouchFloor()) animacion.Play("HeroKnight_Jump");
        else if ((fisica.velocity.x > 1 || fisica.velocity.x < -1) && fisica.velocity.y == 0) 
            animacion.Play("HeroKnight_Run");
        else if ( fisica.velocity.y == 0) 
            animacion.Play("HeroKnight_Idle");
    }

    private bool TouchFloor()
    {
        RaycastHit2D toca = Physics2D.Raycast(transform.position + new Vector3(0,-0.2f,0), Vector2.down, 0.1f);
        return toca.collider != null;
    }
}
