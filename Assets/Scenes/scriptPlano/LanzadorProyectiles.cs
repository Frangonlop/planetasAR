using UnityEngine;
using UnityEngine.InputSystem;

public class LanzadorProyectiles : MonoBehaviour
{
    [SerializeField] GameObject prefabProyectil;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posicionPantalla;
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame) 
        {
            posicionPantalla = Touchscreen.current.primaryTouch.position.ReadValue();

            Ray rayo = Camera.main.ScreenPointToRay(posicionPantalla);
            Vector3 posicionSalida = Camera.main.transform.position + (rayo.direction);
            GameObject bala = Instantiate(prefabProyectil, posicionSalida, Quaternion.LookRotation(rayo.direction));
            
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            rb.AddForce(rayo.direction * 15, ForceMode.Impulse);
            //JuegoSueloController.instanciaJuego.depurar("Toco pantalla " + posicionPantalla.x);
        }
    }
}
