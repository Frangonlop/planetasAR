
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JuegoSueloController : MonoBehaviour
{
    [Header("Interfaz")]
    [SerializeField] TextMeshProUGUI textoVidas;
    [SerializeField] TextMeshProUGUI textoPuntuacion;
    [SerializeField] TextMeshProUGUI puntuacionFinal;
    [SerializeField] GameObject pantallaGameOver;

    [Header("Prefabs")]
    [SerializeField] GameObject prefabEnemigo;
    [SerializeField] GameObject prefabAmigo;

    [Header("Efectos de particulas")]
    [SerializeField] GameObject particulasEnemigo;
    [SerializeField] GameObject particulasAmigo;

    [Header("Configuracion del juego")]
    [SerializeField] private int vidasIniciales = 3;
    [SerializeField] private float alturaEnemigos = 4.0f;
    [SerializeField] private float rangoSpawn = 0.75f;

    private int vidas;
    private int puntuacion = 0;

    private Coroutine corrutinaGeneracion;
    public bool juegoActivo = false;

    public static JuegoSueloController instanciaJuego;

    private void Awake()
    {
        if (instanciaJuego == null)
        {
            instanciaJuego = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        vidas = vidasIniciales;

        ActualizarVidas();

        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(false);
        }
    }

    void Update()
    {
    }

    public void IniciarJuego()
    {
        if (!juegoActivo)
        {
            vidas = vidasIniciales;
            puntuacion = 0;

            ActualizarVidas();
            ActualizarPuntuacion();

            if (pantallaGameOver != null)
            {
                pantallaGameOver.SetActive(false);
            }

            juegoActivo = true;
            corrutinaGeneracion = StartCoroutine(GenerarObjetos());
        }
    }

    IEnumerator GenerarObjetos()
    {
        while (juegoActivo)
        {
            yield return new WaitForSeconds(1);

            if (!juegoActivo)
                yield break;

            Vector3 posicionAleatoria = transform.position + new Vector3(
                Random.Range(-rangoSpawn, rangoSpawn),
                alturaEnemigos,
                3.0f
            );

            GameObject prefabAInstanciar =
                (Random.value > 0.3f) ? prefabEnemigo : prefabAmigo;

            Instantiate(
                prefabAInstanciar,
                posicionAleatoria,
                Quaternion.identity,
                transform
            );
        }
    }

    // Cuando un enemigo toca el suelo
    public void choqueSueloEnemigo()
    {
        if (!juegoActivo)
            return;

        vidas--;

        ActualizarVidas();

        if (vidas <= 0)
        {
            GameOver();
        }
    }
    // Sumar puntos al disparar a un enemigo
    public void SumarPuntos()
    {
        if (!juegoActivo)
            return;

        puntuacion++;

        ActualizarPuntuacion();
    }

    // Restar puntos al disparar a un amigo
    public void RestarPuntos()
    {
        if (!juegoActivo)
            return;

        puntuacion--;

        ActualizarPuntuacion();
    }

    private void ActualizarPuntuacion()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "Puntuación: " + puntuacion;
        }
    }

    // Actualizar el texto de vidas
    private void ActualizarVidas()
    {
        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + vidas;
        }
    }



    // Terminar la partida
    private void GameOver()
    {
        puntuacionFinal.text = "Puntuacion final:" + puntuacion;
        juegoActivo = false;

        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(true);
        }
    }

    // Efecto de destruccion de enemigo
    public void CrearEfectoEnemigo(Vector3 posicion)
    {
        if (particulasEnemigo == null)
            return;

        GameObject efecto = Instantiate(
            particulasEnemigo,
            posicion,
            Quaternion.identity
        );

        efecto.SetActive(true);

        ParticleSystem sistema =
            efecto.GetComponentInChildren<ParticleSystem>(true);

        if (sistema != null)
        {
            sistema.Play(true);
        }
        Destroy(efecto, 3f);
    }

    // Efecto de destruccion de amigo
    public void CrearEfectoAmigo(Vector3 posicion)
    {
        if (particulasAmigo == null)
            return;

        GameObject efecto = Instantiate(
            particulasAmigo,
            posicion,
            Quaternion.identity
        );

        efecto.SetActive(true);

        ParticleSystem sistema =
            efecto.GetComponentInChildren<ParticleSystem>(true);

        if (sistema != null)
        {
            sistema.Play(true);
        }
        Destroy(efecto, 3f);
    }
    public void VolverAJugar()
    {
        // Detener la generación anterior si sigue activa
        if (corrutinaGeneracion != null)
        {
            StopCoroutine(corrutinaGeneracion);
            corrutinaGeneracion = null;
        }

        // Restaurar vidas y puntuación
        vidas = vidasIniciales;
        puntuacion = 0;

        ActualizarVidas();
        ActualizarPuntuacion();

        // Ocultar pantalla Game Over
        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(false);
        }

        // Eliminar objetos anteriores
        foreach (Transform hijo in transform)
        {
            Destroy(hijo.gameObject);
        }

        // Reactivar el juego
        juegoActivo = true;

        // Iniciar una única corrutina
        corrutinaGeneracion = StartCoroutine(GenerarObjetos());
    }

    public void VolverAlInicio()
    {
        juegoActivo = false;

        // Cargar la escena inicial
        SceneManager.LoadScene("MenuPrincipalPlano");
    }
    public void Jugar()
    {
        // Cargar la escena de juego
        SceneManager.LoadScene("DeteccionPlano");
    }
    public void depurar(string msg)
    {
        if (textoVidas != null)
        {
            textoVidas.text = msg;
        }
    }
}
