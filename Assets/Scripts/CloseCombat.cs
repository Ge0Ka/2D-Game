using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
    [SerializeField] private Transform hitControl;
    [SerializeField] private float hitRadio;
    [SerializeField] private float hitDamage;
    private Animator animacion;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Hit();         
        }
    }
    public void Hit()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(hitControl.position, hitRadio);       

        foreach(Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                colisionador.transform.GetComponent<EnemyController>().GetDamage(hitDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hitControl.position, hitRadio);
    }
}
