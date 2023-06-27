using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// En este script se recoge se hace posible el mover
/// la camara con el raton en el modo navegador fuera
/// de las Vr o Ar.
/// </summary>
public class MovimientoCamara : MonoBehaviour
{
    //Establece la velocidad de movimiento de la camara
    [SerializeField]
    private float VelocidaddeCamara;

    private float movVertical;
    private float movHorizontal;

    private void Reset()
    {
        VelocidaddeCamara = 1f;
    }

    private void Start()
    {
        movHorizontal = 0f;
        movVertical = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Lo mismo que en el script de MovimientoCuerpo
        Camera[] cameras = FindObjectsOfType<Camera>();
        foreach (var camera in cameras)
        {
            if (camera.name == "CameraMain")
            {
                this.enabled = camera.enabled;
            }
        }

        //Mientras este pulsado el click derecho del raton entra en el if
        if (Input.GetAxis("Fire2")>0)
        {//Quita la visibilidad del raton para que no moleste en pantalla mientras rotas y observas tu entorno
            Cursor.visible = false;

            //Obtiene el moviemiento que se haga del raton en el eje x 
            movHorizontal += Input.GetAxis("Mouse X") * VelocidaddeCamara;

            //Obtiene el movimiento que se haga del raton en el eje y
            movVertical -= Input.GetAxis("Mouse Y") * VelocidaddeCamara;

            //Limite el movimiento del eje y por que la camara cuando supera x grados cambia valores +- y deja de funcionar correctamente
            movVertical = Mathf.Clamp(movVertical, -80f, 80f);

            //Rota la camara lo que se haya movido el raton
            transform.eulerAngles = new Vector3(movVertical, movHorizontal, 0f);
        } else
        {
            transform.eulerAngles = new Vector3(movVertical, movHorizontal, 0f);
            Cursor.visible = true;
        }
    }
}
