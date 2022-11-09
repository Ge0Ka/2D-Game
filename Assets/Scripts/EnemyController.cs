using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D enemyRigidbody;
    public Transform player;

    [SerializeField] private float vida;
    private Animator animacion;
    //private LifeBar lifeBar;
    private bool lookLeft = true;
    private SpriteRenderer sprite;


    private void Start()
    {
        animacion = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        //lifeBar.InicializarLifeBar(vida);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        LookPlayer();
    }

    public void GetDamage (float damage)
    {
        vida -= damage;
        //lifeBar.CambiarVidaActual(vida);

        if (vida <= 0)
        {
            animacion.Play("Death");
            Dead();
        }

    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    private void LookPlayer()
    {
        if((player.position.x > transform.position.x && lookLeft) || (player.position.x < transform.position.x && !lookLeft))
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
            lookLeft = !lookLeft;            
        }
    }
}
