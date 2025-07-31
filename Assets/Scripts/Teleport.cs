using UnityEngine;

public class Teleport : MonoBehaviour
{
    /* 
    Esse código faz o jogador se transportar automaticamente de um ponto para o outro ao entrar em uma colisão do tipo Trigger em determinadas portas e ao apertar a Tecla T,
    com uma lógica usando OnTriggerEnter/Exit e Bool
    */

    public Transform doorTeleport;
    private Transform playerTeleport;
    public bool playerInside;

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.T))
        {
            playerTeleport.position = doorTeleport.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            playerInside = true;
            //other.transform.position = doorTeleport.position;
            playerTeleport = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            playerInside = false;
            playerTeleport = null;
        }
    }
}
