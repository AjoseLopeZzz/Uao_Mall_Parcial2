using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ocultar : MonoBehaviour
{
    public GameObject camara1;
    public GameObject camara2;

    private bool camara1Activa = true;
    public void ocultarcamaras()
    {
        if (camara1Activa)
        {
            camara1.SetActive(false);
            camara2.SetActive(true);
        }
        else
        {
            camara1.SetActive(true);
            camara2.SetActive(false);
        }

        // Invertir el estado de activación de las cámaras
        camara1Activa = !camara1Activa;
    }
}
