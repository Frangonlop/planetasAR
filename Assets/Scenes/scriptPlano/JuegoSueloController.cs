using UnityEngine;

public class JuegoSueloController : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemigo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarJuego()
    {
        Instantiate(prefabEnemigo);
        Instantiate(prefabEnemigo);
        Instantiate(prefabEnemigo);
    }
}
