using UnityEngine;


public class ItemPickup : MonoBehaviour
{
    public int puntos = 2;  // Puedes cambiar este valor según el item.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Incrementa la puntuación
            GameManager.instance.AddScore(puntos);

            //Instantiate(efecto, transform.position, Quaternion.identity);

            // Destruye el objeto después de recogerlo
            Destroy(gameObject);
        }
    }
}