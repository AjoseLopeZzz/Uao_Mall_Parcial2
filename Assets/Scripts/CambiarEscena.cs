using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    // Nombre de la escena a la que queremos cambiar
    public string nombreDeLaEscena;

    // M�todo que se llamar� cuando se haga clic en el bot�n
    public void CambiarAEscena()
    {
        // Cargar la escena con el nombre especificado
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
