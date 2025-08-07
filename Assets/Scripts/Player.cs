using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Criando vari�veis de velocidade, for�a do pulo, Rigidbody para ter fisica, dire��o do movimento e bool para checagem de pulo
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 20f;
    private bool isGrounded = false;
    private bool isSprinting = false;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        //Pegando o rigidbody do objeto
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
        walkAndSprint();
        Jump();
    }

    // Checagem de ch�o sendo feita para controle de pulo do player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    // M�todo FixedUpdate � chamado em intervalos fixos, ideal para f�sica e movimenta��o com Rigidbody
    // Defini��o da velocidade do jogador, calculando entre Correr e Andar
    void FixedUpdate()
    {
        float speed = isSprinting ? sprintSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        // Faz o personagem pular ao apertar Espa�o
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded == true)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

    }

    private void walkAndSprint()
    // Coleta o input do teclado (cl�ssico)
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    
}

