using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDeUsuario : MonoBehaviour
{
    // Radio del área de detección
    public float detectionRadius = 5f;

    // Referencia al jugador
    public Transform player;

    // Método Gizmos para visualizar el área de detección en la vista de escena
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void Update()
    {
        // Verificar si el jugador está dentro del área de detección
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            // Activar el panel de letras
            GameManager.Instance.isLetraActive = true;
        }
        else
        {
            // Desactivar ambos paneles al salir del área
            GameManager.Instance.isLetraActive = false;
            GameManager.Instance.LetraActive.SetActive(false);  // Asegurarse de que se desactive
            GameManager.Instance.SubMenu.SetActive(false);      // Asegurarse de que se desactive
        }
    }
}
