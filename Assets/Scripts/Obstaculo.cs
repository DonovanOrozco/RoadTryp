using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.RestarVida();


            Destroy(gameObject); 
        }
    }
}