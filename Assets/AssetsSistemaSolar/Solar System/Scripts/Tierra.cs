using UnityEngine;
using Vuforia;
using TMPro;

public class Tierra : MonoBehaviour
{
    private ObserverBehaviour observer;

    [SerializeField] TextMeshPro textoPantalla;
    public GameController controladorJuego;
    public string planetaReconocer;

    private bool planetaDetectado = false;


    void Awake()
    {
        observer = GetComponent<ObserverBehaviour>();
    }

    private void OnEnable()
    {
        if (observer != null)
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    private void OnDisable()
    {
        if (observer != null)
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour,TargetStatus status)
    {
        bool detectado = status.Status == Status.TRACKED;
        

        // Solo actuar cuando empieza una nueva detección
        if (detectado && !planetaDetectado)
        {
            planetaDetectado = true;

            if (controladorJuego != null)
            {
                controladorJuego.chequear(planetaReconocer);
            }
        }

        // Cuando Vuforia pierde el seguimiento, permitir detectar otra vez el mismo planeta.
        if (!detectado)
        {
            planetaDetectado = false;
        }
    }
}