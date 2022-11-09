using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public int jumpForce;
    public int rollVelocity;
    public float rollDuration;
    
    private Rigidbody2D fisica;
    private SpriteRenderer sprite;
    private Animator animacion;
    private bool canRoll = true;
    private bool canMove = true;
    public GameObject player;
    

    private void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animacion = GetComponentInChildren<Animator>();
        player = GetComponent<GameObject>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            float entradaX = Input.GetAxis("Horizontal");
            fisica.velocity = new Vector2(entradaX * speed, fisica.velocity.y);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TouchFloor())
        {
            fisica.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        // TODO : Arreglar, si le das al A-D mientras roll, no la capta y se mueve de espaldas
        //if (canMove)
        //{
        //    if (Input.GetKey(KeyCode.A)) transform.localScale = new Vector3(-1, 1, 1); 
        //    else if (Input.GetKey(KeyCode.D)) transform.localScale = new Vector3(1, 1, 1);
        //}

        if(canMove && Mathf.Abs(fisica.velocity.x)!=0) transform.localScale = new Vector3(Mathf.Sign(fisica.velocity.x), 1, 1); 

        
        //Otra manera de hacer que gire el sprite
        //if (Input.GetKeyDown(KeyCode.A)) sprite.flipX = false;
        //else if (Input.GetKeyDown(KeyCode.A)) sprite.flipX = true;

        AnimarJugador();

        Roller();
    }

    // TODO: añadir la animacion de bajada en el salto
    private void AnimarJugador()
    {
        // if (!TouchFloor() && fisica.velocity.y > 0) animacion.Play("HeroKnight_Jump");
        //else if ((fisica.velocity.x > 1 || fisica.velocity.x < -1) && fisica.velocity.y == 0 && canMove == false)
        //    animacion.Play("HeroKnight_Roll");
        //else if ((fisica.velocity.x > 1 || fisica.velocity.x < -1) && fisica.velocity.y == 0)
        //    animacion.Play("HeroKnight_Run");
        //else if (fisica.velocity.y == 0)
        //    animacion.Play("HeroKnight_Idle");
        //else if (fisica.velocity.y < 0 && !TouchFloor())
        //    animacion.Play("HeroKnight_Fall");
        animacion.SetBool("isRunning", Mathf.Abs(fisica.velocity.x) > 0.1f);
        animacion.SetBool("isJumping", Mathf.Abs(fisica.velocity.y) > 0.1f && !TouchFloor());
        animacion.SetBool("isFalling", (fisica.velocity.y) < 0.1f && !TouchFloor());
        animacion.SetBool("isRolling", canMove==false && TouchFloor());
    }

    private void Roller()
    {
        if (Input.GetMouseButtonDown(1) && TouchFloor() && canRoll)
        {
            StartCoroutine(Roll());            
        }
    }

    private IEnumerator Roll()
    {
        canMove = false;
        canRoll = false;
        fisica.velocity = new Vector2(rollVelocity * transform.localScale.x, 0) ;       

        yield return new WaitForSeconds(rollDuration);

        canMove = true;
        canRoll = true;
    }
    private bool TouchFloor()
    {
        RaycastHit2D toca = Physics2D.Raycast(transform.position + new Vector3(0,-0.2f,0), Vector2.down, 0.1f);
        return toca.collider != null;
    }
}
