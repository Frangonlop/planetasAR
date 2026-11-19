
using System.Collections;
using TMPro;
using UnityEngine;

public class JuegoSueloController : MonoBehaviour
{
    [Header("Interfaz")]
    [SerializeField] TextMeshProUGUI textoVidas;
    [SerializeField] GameObject pantallaGameOver;

    [Header("Prefabs")]
    [SerializeField] GameObject prefabEnemigo;
    [SerializeField] GameObject prefabAmigo;

    [Header("Efectos de particulas")]
    [SerializeField] GameObject particulasEnemigo;
    [SerializeField] GameObject particulasAmigo;

    [Header("Configuracion del juego")]
    [SerializeField] private int vidasIniciales = 3;
    [SerializeField] private float alturaEnemigos = 3.0f;
    [SerializeField] private float rangoSpawn = 1.0f;

    private int vidas;
    private int puntuacion = 0;

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

            if (pantallaGameOver != null)
            {
                pantallaGameOver.SetActive(false);
            }

            juegoActivo = true;
            StartCoroutine(GenerarObjetos());
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

    // Cuando un amigo toca el suelo
    public void choqueSueloAmigo()
    {
        if (!juegoActivo)
            return;

        puntuacion++;
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
        juegoActivo = false;

        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(true);
        }
    }

    // Efecto de destruccion de enemigo
    public void CrearEfectoEnemigo(Vector3 posicion)
    {
        if (particulasEnemigo != null)
        {
            GameObject efecto = Instantiate(
                particulasEnemigo,
                posicion,
                Quaternion.identity
            );
        }
    }

    // Efecto de destruccion de amigo
    public void CrearEfectoAmigo(Vector3 posicion)
    {
        if (particulasAmigo != null)
        {
            GameObject efecto = Instantiate(
                particulasAmigo,
                posicion,
                Quaternion.identity
            );
        }
    }

    public void depurar(string msg)
    {
        if (textoVidas != null)
        {
            textoVidas.text = msg;
        }
    }
}
