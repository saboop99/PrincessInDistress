using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform doorTeleport;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            other.transform.position = doorTeleport.position;
            /*
            if (Input.GetKeyDown(KeyCode.T))
            {
                other.transform.position = doorTeleport.position;
            }*/
        }
    }

}
