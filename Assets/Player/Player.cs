using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Criando vari�veis de velocidade, for�a do pulo, Rigidbody para ter fisica e a dire��o do movimento
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        //Pegando o rigidbody do objeto
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Coleta o input do teclado (cl�ssico)
        movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Console.WriteLine("espa�o");
            // Aplica for�a de pulo verticalmente
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // M�todo FixedUpdate � chamado em intervalos fixos, ideal para f�sica e movimenta��o com Rigidbody
    void FixedUpdate()
    {
        // Move o Rigidbody2D aplicando a dire��o e velocidade desejada
        // Multiplica pelo deltaTime para manter o movimento consistente em diferentes framerates
        rb.MovePosition(rb.position + new Vector2(movement.x, 0) * moveSpeed * Time.fixedDeltaTime);
    }
}
