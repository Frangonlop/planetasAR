using UnityEngine;
using TMPro;
using Vuforia;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] float tiempoTotal = 10f;
    [SerializeField] TextMeshProUGUI tiempoTxt;
    [SerializeField] TextMeshProUGUI PlanetaAdivinarTxt;
    [SerializeField] int puntos;
    [SerializeField] TextMeshProUGUI puntuacion;
    [SerializeField] string[] opcionesMostrar= {"Tierra","Sol","Mercurio","Venus","Marte"};
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject uiJuego;
    [SerializeField] AudioSource efectoSonidoGameOver;
    [SerializeField] AudioSource efectoAcierto;
    [SerializeField] AudioSource efectoError;
    [SerializeField] TextMeshProUGUI puntuacionFinal;
    private bool partidaTerminada;
    private int errores = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ElegirNuevoPlaneta(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (partidaTerminada) return;
        
            
        
        // Controlo que no se acaba el tiempo
        tiempoTotal -= Time.deltaTime;
        if ( tiempoTotal < 0)
        {
            // Ha acabado la partida
            puntuacionFinal.text = "Tu puntuación ha sido de: " + puntos.ToString();
            gameOver.SetActive(true);
            uiJuego.SetActive(false);
            partidaTerminada = true;

            if(efectoSonidoGameOver != null){
                efectoSonidoGameOver.Play();

            }
        }
        else
        {
            tiempoTxt.text = tiempoTotal.ToString("F1");
        }
    }

    private void ElegirNuevoPlaneta()
    {
        string planetaAnterior = PlanetaAdivinarTxt.text;
        int posAleatoria;

        if (opcionesMostrar.Length > 1)
        {
            do
            {
                posAleatoria = Random.Range(0, opcionesMostrar.Length);
            }
            while (opcionesMostrar[posAleatoria] == planetaAnterior);
        }
        else
        {
            posAleatoria = 0;
        }

        PlanetaAdivinarTxt.text = opcionesMostrar[posAleatoria];
    }
    

    public void chequear(string planetaEncontrado)
    {
       if (planetaEncontrado == PlanetaAdivinarTxt.text)
        { 
            // Acierto -> Contar puntos y generar nuevo elemento a buscar
            puntos++;
            puntuacion.text = puntos.ToString();
            tiempoTotal = 10f;

            // Reproducir un sonido cuando se acierte el planeta
            if (efectoAcierto != null)
            {
                efectoAcierto.Play();
            }

            ElegirNuevoPlaneta();

        }
        else
        {
            // Error -> Si se equivoca de imagen 3 veces termina el juego
            if (puntos > 0)
            {
                puntos--;
                puntuacion.text = puntos.ToString();
            }
            errores++;
       
            if (errores >= 3)
            {
                // Ha acabado la partida
                puntuacionFinal.text = "Tu puntuación ha sido de: " + puntos.ToString();
                gameOver.SetActive(true);
                uiJuego.SetActive(false);
                partidaTerminada = true;

                if (efectoSonidoGameOver != null)
                {
                    efectoSonidoGameOver.Play();
                }
                return;
            }

            if (efectoError != null)
            {

                efectoError.Play();
            }
            tiempoTotal = 10f;
            ElegirNuevoPlaneta();
        }
    }
    public void reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void volverInicio()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
