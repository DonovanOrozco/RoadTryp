using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Referencia al vehículo que la cámara seguirá
    public Vector3 offset;          // Distancia entre cámara y vehículo
    public float smoothSpeed = 0.125f; // Velocidad del suavizado

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Opcional: hacer que la cámara mire hacia el vehículo constantemente
        transform.LookAt(target);
    }
}