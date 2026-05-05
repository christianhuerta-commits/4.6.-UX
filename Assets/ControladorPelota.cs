using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Añadido para que el reinicio funcione bien

public class ControladorPelota : MonoBehaviour
{
    private Rigidbody rb;
    public float fuerzaImpulso = 5f; 
    public float aumentoVelocidad = 1.5f; 
    
    private int contadorRebotes = 0;
    private int nivelActual = 1;

    public TMP_Text textoPrincipal; 
    public TMP_Text textoNivel;     

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(textoNivel != null) textoNivel.text = ""; 
        ActualizarInterfaz();
        LanzarPelota();
    }

    void Update()
    {
        // BUCLE AL PERDER: Si la pelota cae del plano
        if (transform.position.y < -5f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            contadorRebotes++;
            
            // CURVA DE DIFICULTAD: Lógica de cada 10 rebotes
            if (contadorRebotes % 10 == 0)
            {
                if(textoNivel != null) textoNivel.text = "¡NIVEL " + nivelActual + " SUPERADO!";
                
                nivelActual++;
                fuerzaImpulso += aumentoVelocidad; 
                
                Invoke("LimpiarMensaje", 2f);
            }

            // CORRECCIÓN PARA MOVIMIENTO LATERAL:
            // 1. Mantenemos la velocidad en X y Z, pero ponemos la Y en 0 para un rebote limpio.
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

            // 2. Creamos una dirección hacia ARRIBA con un poquito de variación aleatoria a los lados.
            Vector3 direccionRebote = new Vector3(Random.Range(-0.1f, 0.1f), 1f, Random.Range(-0.1f, 0.1f)).normalized;
            
            // 3. Aplicamos la fuerza.
            rb.AddForce(direccionRebote * fuerzaImpulso, ForceMode.Impulse);

            ActualizarInterfaz();
        }
    }

    void ActualizarInterfaz()
    {
        if(textoPrincipal != null) 
            textoPrincipal.text = "Rebotes: " + contadorRebotes;
    }

    void LimpiarMensaje()
    {
        if(textoNivel != null) textoNivel.text = "";
    }

    void LanzarPelota()
    {
        // Lanzamiento inicial con dirección aleatoria
        Vector3 direccion = new Vector3(Random.Range(-0.5f, 0.5f), -1f, Random.Range(-0.5f, 0.5f)).normalized;
        rb.AddForce(direccion * fuerzaImpulso, ForceMode.Impulse);
    }
}