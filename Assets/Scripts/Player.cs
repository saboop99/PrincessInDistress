using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Criando variáveis de velocidade, força do pulo, Rigidbody para ter fisica, direção do movimento e bool para checagem de pulo
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 20f;
    private bool isGrounded = false;
    private bool isSprinting = false;
    private bool isMoving = false;
    private bool isDucking = false;
    private bool facingRight = true;
    private bool isAttacking = false;
    private bool isJumping = false;
    public Collider2D standingCollider;
    public Collider2D duckCollider;
    public Collider2D attackCollider;
    private Animator anim;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
        // ----------- Move -----------
        walkAndSprint();

        // ----------- Jump -----------
        Jump();

        anim.SetFloat("jumpSpeed", rb.linearVelocityY);
        // se isGrounded = true, isJumping = false, e vice versa
        anim.SetBool("isJumping", !isGrounded);

        // ----------- Duck -----------
        Duck();

        Attack();
    }

    // Checagem de chão sendo feita para controle de pulo do player
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
    // Definição da velocidade do jogador, calculando entre Correr e Andar
    void FixedUpdate()
    {
        // Se isSpriting = true, setar sprintSpeed, se = false, setar moveSpeed
        float speed = isSprinting ? sprintSpeed : moveSpeed;

        // só se move se não estiver agachado ou atacando
        if (!isDucking && !isAttacking)
        {
            rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);

            float currentSpeed = Mathf.Abs(movement.x * speed);
            isMoving = movement.x != 0;
            anim.SetFloat("speedAnim", currentSpeed);
            anim.SetBool("isSprinting", isSprinting && isMoving);
            // Ativa o collider normal
            standingCollider.enabled = true;
            duckCollider.enabled = false;
        }

        // se agachar ou atacar, zera o movimento todo
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // zera o movimento horizontal
            isMoving = false;
            anim.SetFloat("speedAnim", 0);
            anim.SetBool("isSprinting", false);

            if (isDucking)
            {
                standingCollider.enabled = false;
                // Ativa o collider do Duck
                duckCollider.enabled = true;
            }
        }
    }

    // Método Attack dá start ao método attackRoutine, que é um padrão usado para dar uma "pausa" sem o jogo travar, estou usando isso para deixar o ataque do personagem mais fluído e mais real, diferente de usar apenas Bool
    private void Attack()
    {
        // Ataca ao apertar o botão esquerdo do mouse, porém só se estiver em contato com o chão
        if (Input.GetMouseButtonDown(0) && isGrounded && !isDucking)
        {
            StartCoroutine(attackRoutine());
        }   
    }

    private IEnumerator attackRoutine()
    {
        attackCollider.enabled = true;
        anim.SetBool("isAttacking", true);
        isAttacking = true;
        
        yield return new WaitForSeconds(0.2f);

        attackCollider.enabled = false;
        anim.SetBool("isAttacking", false);
        isAttacking = false;
    }

    private void Jump()
    {
        // Faz o personagem pular ao apertar Espaço
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded == true)
            { 
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetBool("isJumping", true);
                isJumping = true;

                // Cancelando ataque caso pule
                attackCollider.enabled = false;
                anim.SetBool("isAttacking", false);
                isAttacking = false;
            }   
        }

        if (isGrounded)
        {
            isJumping = false;
            anim.SetBool("isJumping", false);           
        } 
    }

    private void Duck()
    {
        if (Input.GetKeyDown(KeyCode.S) )
        {
            isDucking = true;
            anim.SetBool("isDucking", true);

            // Cancelando ataque caso agache
            attackCollider.enabled = false;
            anim.SetBool("isAttacking", false);

            //rb.linearVelocity = Vector2.zero;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isDucking = false;
            anim.SetBool("isDucking", false);
        }
    }

    private void walkAndSprint()
    // Coleta o input do teclado (clássico)
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (movement.x > 0 && facingRight == false)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight == true)
        {
            Flip();
        }


    }

    // Método flip usado para virar o personagem, cada vez que é chamado, muda onde o personagem está apontando conforme a boolean facingRight
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x = scale.x * -1;
        transform.localScale = scale;
    }


}

