
using TMPro;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        // Cuando toca el suelo
        if (collider.CompareTag("suelo"))
        {
            if (gameObject.CompareTag("enemigo"))
            {
                JuegoSueloController.instanciaJuego
                    .choqueSueloEnemigo();

                JuegoSueloController.instanciaJuego
                    .CrearEfectoEnemigo(transform.position);
            }
            else if (gameObject.CompareTag("amigo"))
            {
                JuegoSueloController.instanciaJuego
                    .CrearEfectoAmigo(transform.position);
            }

            Destroy(gameObject);
        }

        // Cuando recibe un disparo
        if (collider.CompareTag("proyectil"))
        {
            if (gameObject.CompareTag("enemigo"))
            {
                JuegoSueloController.instanciaJuego
                    .SumarPuntos();

                JuegoSueloController.instanciaJuego
                    .CrearEfectoEnemigo(transform.position);

                Destroy(gameObject);
                Destroy(collider.gameObject);
            }
            else if (gameObject.CompareTag("amigo"))
            {
                JuegoSueloController.instanciaJuego
                    .RestarPuntos();

                JuegoSueloController.instanciaJuego
                    .CrearEfectoAmigo(transform.position);

                Destroy(gameObject);
                Destroy(collider.gameObject);
            }
        }
    }
}
