using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Criando variáveis de velocidade, força do pulo, Rigidbody para ter fisica, direção do movimento e bool para checagem de pulo
    public float moveSpeed = 5f;
    public float jumpForce = 100f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        //Pegando o rigidbody do objeto
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Coleta o input do teclado (clássico)
        movement.x = Input.GetAxisRaw("Horizontal");
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
            //Console.WriteLine("espaço");
            // Aplica força de pulo verticalmente
            
            //rb.AddForce(new Vector2(rb.linearVelocity.x, jumpForce));

        }
    }

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

    // Método FixedUpdate é chamado em intervalos fixos, ideal para física e movimentação com Rigidbody
    void FixedUpdate()
    {
        // Move o Rigidbody2D aplicando a direção e velocidade desejada
        // Multiplica pelo deltaTime para manter o movimento consistente em diferentes framerates
        rb.MovePosition(rb.position + new Vector2(movement.x, 0) * moveSpeed * Time.fixedDeltaTime);
    }
}
