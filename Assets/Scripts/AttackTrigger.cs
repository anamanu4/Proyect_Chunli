using System;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public bool enemyInRange;
    public GameObject enemy; 
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyInRange = true;
            enemy = other.gameObject; 
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        enemyInRange = false; 
    }

  
}
