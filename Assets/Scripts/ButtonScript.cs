using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para los elementos de la UI como los botones
using UnityEngine.SceneManagement; // Para manejar las escenas


public class ButtonScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /*void LoadNewScene()
    {
        // Asegúrate de tener la escena agregada en la lista de "Build Settings"
        //SceneManager.LoadScene("NombreDeLaEscena"); Cambia por el nombre de la escena
    }
    */

    public void Salir()
    {
        Application.Quit(); // Cambia la variable isDatos a true
    }

    public void Cotninue()
    {
        GameManager.Instance.IsPause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameManager.Instance.PanelPausa.SetActive(false);
    }
}
