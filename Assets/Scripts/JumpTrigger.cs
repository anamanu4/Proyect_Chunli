using System;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public bool canJump; 
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ground"))
        {
            Debug.Log("Puede saltar");
            canJump = true; 
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ground"))
        {
            Debug.Log("No puede saltar");
            canJump = false; 
        } 
    }
}
