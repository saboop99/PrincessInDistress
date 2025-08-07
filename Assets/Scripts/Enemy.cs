using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform patrolPointA;
    public Transform patrolPointB;
    public float patrolSpeed = 2f;

    private Transform target;

    void Start()
    {
        target = patrolPointB; // Começa indo para o ponto B
    }

    void Update()
    {
        // Move o inimigo na direção do alvo
        transform.position = Vector2.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);

        // Verifica se chegou no ponto e inverte o alvo
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            if (target == patrolPointA)
            {
                target = patrolPointB;
            }

            else
            {
                target = patrolPointA;
            }

            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = scale.x * -1;
        transform.localScale = scale;
    }
}
