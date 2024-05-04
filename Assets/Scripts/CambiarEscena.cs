using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    // Nombre de la escena a la que queremos cambiar
    public string nombreDeLaEscena;

    // Método que se llamará cuando se haga clic en el botón
    public void CambiarAEscena()
    {
        // Cargar la escena con el nombre especificado
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
