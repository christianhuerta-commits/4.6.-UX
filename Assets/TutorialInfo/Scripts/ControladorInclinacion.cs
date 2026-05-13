using UnityEngine;

public class ControlInclinacion : MonoBehaviour
{
    public float sensibilidad = 20f; // Qué tanto se inclina el plano
    public float suavizado = 5f;    // Qué tan suave es el movimiento

    void Update()
    {
        // 1. Obtener la inclinación del acelerómetro
        // x = inclinación lateral, y = inclinación adelante/atrás
        Vector3 inclinacion = Input.acceleration;

        // 2. Calculamos la rotación deseada
        // Invertimos 'y' para que al inclinar adelante, el plano baje de adelante
        float rotacionX = -inclinacion.y * sensibilidad;
        float rotacionZ = -inclinacion.x * sensibilidad;

        // 3. Creamos el objetivo de rotación
        Quaternion targetRotation = Quaternion.Euler(rotacionX, 0, rotacionZ);

        // 4. Aplicamos la rotación suavemente
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * suavizado);
    }
}