using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float vel;

    public float jumpForce;

    private JumpTrigger _jumpTrigger; 
   
    private Rigidbody2D _rb;

    private Animator _animController;

    public GameObject attackTrigger; 


    private void Start()
    {
        // Se redeclara la variable rb para obtener el componente rigidbody del gameobject al que este asignado este script 
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _jumpTrigger = gameObject.GetComponentInChildren<JumpTrigger>();
        _animController = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // Variable que se actualiza con el valor del input horizontal donde la tecla A = -1 y la tecla D = 1
        var horizontalMov = Input.GetAxis("Horizontal");
       
       
        // X = 1, D, HorizontalMov = 1, transform.position.x = 1 + 1 por frame, * 1, cada frame 1 unidad en x a tu position en x, transform.position = (0, 0, 0) = nuevo vector (1 + 1 cada frame, 0)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = vel * (isSprinting ? 2f : 1f);
        _rb.linearVelocity = new Vector2(horizontalMov * speed, _rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _animController.SetBool("running", true);
            _animController.SetBool("isWalking", false);
        }
        else if (!isSprinting)
        {
            _animController.SetBool("running", false);
        }


        // Se valida si se esta presionando el space 
        if (Input.GetKeyDown(KeyCode.Space) && _jumpTrigger != null && _jumpTrigger.canJump)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animController.SetBool("jumping", true);
            _animController.SetBool("falling", false);
        }
        else if (_jumpTrigger != null && !_jumpTrigger.canJump)
        {
            _animController.SetBool("falling", _rb.linearVelocity.y < -0.01f);
        }
        else if (_jumpTrigger != null && _jumpTrigger.canJump)
        {
            // Aterrizado
            _animController.SetBool("jumping", false);
            _animController.SetBool("falling", false);
            _animController.SetBool("landing", true);
        }


        // Rotacion del personaje 
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            attackTrigger.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _animController.SetBool("isWalking", true);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
            attackTrigger.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _animController.SetBool("isWalking", true);

        }
        else
        {
            _animController.SetBool("isWalking", false);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q) && attackTrigger.GetComponent<AttackTrigger>().enemyInRange && !_animController.GetBool("attack"))
        {
            Debug.Log("Se hizo daño al enemigo");
            _animController.SetBool("attack", true);
            Destroy(attackTrigger.GetComponent<AttackTrigger>().enemy);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !attackTrigger.GetComponent<AttackTrigger>().enemyInRange && !_animController.GetBool("attack"))
        {
            _animController.SetBool("attack", true);
            Debug.Log("Se golpeo a legarda");
        }

        yield return new WaitForSecondsRealtime(0.3f);
            
        _animController.SetBool("attack", false);
    }
    
    
}
