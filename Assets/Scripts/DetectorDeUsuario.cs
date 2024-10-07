using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDeUsuario : MonoBehaviour
{
    // Radio del �rea de detecci�n
    public float detectionRadius = 5f;

    // Referencia al jugador
    public Transform player;

    // M�todo Gizmos para visualizar el �rea de detecci�n en la vista de escena
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void Update()
    {
        // Verificar si el jugador est� dentro del �rea de detecci�n
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            // Activar el panel de letras
            GameManager.Instance.isLetraActive = true;
        }
        else
        {
            // Desactivar ambos paneles al salir del �rea
            GameManager.Instance.isLetraActive = false;
            GameManager.Instance.LetraActive.SetActive(false);  // Asegurarse de que se desactive
            GameManager.Instance.SubMenu.SetActive(false);      // Asegurarse de que se desactive
        }
    }
}
