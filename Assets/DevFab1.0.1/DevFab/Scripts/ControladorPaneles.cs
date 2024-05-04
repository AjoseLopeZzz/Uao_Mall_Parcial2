using UnityEngine;

public class ControladorPaneles : MonoBehaviour
{
    public GameObject panelHamburguesa;
    public GameObject panelInfoMall;
    public GameObject panelLocales;
    public GameObject panelPromociones;
    public GameObject panelLocalesNuevos;

    public void activarPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    
}
