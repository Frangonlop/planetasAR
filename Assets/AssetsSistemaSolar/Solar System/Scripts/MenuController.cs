using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Jugar()
    {
        // Cargar escena con el juego
        SceneManager.LoadScene("Juego");
    }
    public void Salir()
    {
    Application.Quit();
    }
}

