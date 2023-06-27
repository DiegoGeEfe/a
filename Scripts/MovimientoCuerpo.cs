using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebXR;
using static UnityEngine.AudioSettings;

/// <summary>
/// En este script se recoge las posibles formas de 
/// interactuar que puede llevar a cabo un usuario 
/// con los distintos dispositivos, ya sean en el 
/// navegador web desde pc, en navegador web desde
/// el movil, desde dispositivos VR con mandos especiales
/// o mandos de xbox . En este caso para poder llevar a
/// cabo el movimiento.
/// </summary>
public class MovimientoCuerpo : MonoBehaviour
{
    [SerializeField]
    private float velocidad;
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private Canvas mobileController;
    [SerializeField]
    private TextMeshProUGUI prueba;

    //Esto servirá para poder asociar que hace referencia a la mano izquierda con este script
    [SerializeField]
    private GameObject lHand;

    private Rigidbody rb;

    //Esta variable nos dara la rotacion de la camara que esté en uso para que podamos saber hacia donde mira y entorn a eso
    //llevar a cabo nuestro movimiento hacia delante.
    private Camera camara;

    //Esta variable nos dira si estamos en un dispositivo movil o no.
    private bool isMobile;

    //Con esta variable podremos tener acceso a las distintas opciones que nos dan los controladores
    private WebXRController controlador;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();


        Camera[] cameras = FindObjectsOfType<Camera>();
        /*
         * El array recoge todas las camaras que tiene la escena
         * para que luego en el bucle foreach las recorra una a
         * una buscando cual es la que está funcionando, y así poder
         * cambiar la camara según el modo de juego que se esté usando
         * (navegador,VR o AR). En el caso del VR y AR se quedará
         * siempre con el ojo izquierdo por que en cuanto encuentra
         * una la asigna a la variable y sale del bucle.
         */
        foreach (var camera in cameras)
        {
            if (camera.enabled)
            {
               camara = camera;
                break;
            }
        }
        controlador = lHand.GetComponent<WebXRController>();
    }

    private void Reset()
    {
        velocidad = 1f;
    }

    private void Start()
    {
        velocidad = velocidad == 0? 10f:velocidad;

        //Aqui es donde asignamos a la variable el valor segun se este en movil o no(true o false respectivamente)
        isMobile = Application.isMobilePlatform;

        //Hace que sea visible o no el joystick para los controles de movil
        mobileController.enabled = isMobile;
        
    }

    private void Update()
    {
        //Aqui es donde si cambias al modo VR o vuelve al navegador cambie la camara segun uses.
        if (!camara.enabled)
        {
            Camera[] cameras = FindObjectsOfType<Camera>();
            foreach (var camera in cameras)
            {
                if (camera.enabled)
                {
                    camara = camera;
                    break;
                }

            }
        }
    }

        private void FixedUpdate()
    {
        //El movimiento hacia delante y el lado teniendo en cuenta el no elevarse.
        Vector3 forward = new Vector3(camara.transform.forward.x, 0f, camara.transform.forward.z).normalized;
        Vector3 right = new Vector3(camara.transform.right.x, 0f, camara.transform.right.z).normalized;
        if (isMobile)
        {//Detecta el movimiento con el joystick que saldría en pantalla en móvil
            rb.velocity += (forward * velocidad * Time.fixedDeltaTime * joystick.Vertical) + (right * velocidad * Time.fixedDeltaTime * joystick.Horizontal);
        
        } else if (Input.GetAxis("Vertical")!=0 || Input.GetAxis("Horizontal") != 0)
        {//Detecta el movimiento con el teclado y creo que con joysticks de mandos que detecte el ordenador directamente
            rb.velocity += (forward * velocidad * Time.fixedDeltaTime * Input.GetAxis("Vertical")) + (right * velocidad * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));
        
        } else if (controlador.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).y != 0 || controlador.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).x != 0) 
        {//Detecta el movimiento de los joysticks de los mandos de VR y creo que de los mandos que conectes como en el caso de las oculus que puedes conectar un mando de xbox
            rb.velocity += (forward * velocidad * Time.fixedDeltaTime * controlador.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).y) + (right * velocidad * Time.fixedDeltaTime * controlador.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick).x);
        
        } else if (controlador.GetAxis2D(WebXRController.Axis2DTypes.Touchpad).y != 0 || controlador.GetAxis2D(WebXRController.Axis2DTypes.Touchpad).x != 0)
        {//Detecta el movimiento de las pantallas tactiles de los mandos de VR y creo que de los mandos que conectes ,como en el caso de las oculus que puedes conectar un mando de xbox, si tienen pantallas tactiles
            rb.velocity += (forward * velocidad * Time.fixedDeltaTime * controlador.GetAxis2D(WebXRController.Axis2DTypes.Touchpad).y) + (right * velocidad * Time.fixedDeltaTime * controlador.GetAxis2D(WebXRController.Axis2DTypes.Touchpad).x);
        }
    }
}
