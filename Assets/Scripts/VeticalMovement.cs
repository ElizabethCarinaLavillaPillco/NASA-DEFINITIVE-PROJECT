using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeticalMovement : MonoBehaviour
{
    public float rayDistance = 100f; // Distancia del raycast
    public Color rayColor = Color.red;

    private bool isLookingAtPlanet = false;  // Variable para saber si estamos apuntando a un planeta

    void Update()
    {
        // Control de rotación vertical del objeto con el ratón
        float mousey = Input.GetAxis("Mouse Y");
        transform.Rotate(mousey * -1f, 0, 0);

        // Crear el raycast desde la posición y dirección del objeto
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Dibujar un rayo en la escena para depuración (opcional)
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, rayColor);

        // Lógica para detectar si estamos mirando un objeto con la etiqueta "Planet"
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Planet"))
            {
                if (!isLookingAtPlanet)  // Si no estamos ya mirando el planeta
                {
                    isLookingAtPlanet = true;
                    GameManager.Instance.isDatos = true;  // Activar el panel de información en el GameManager
                }
            }
            else
            {
                if (isLookingAtPlanet)  // Si dejamos de mirar el planeta
                {
                    isLookingAtPlanet = false;
                    GameManager.Instance.isDatos = false;  // Desactivar el panel de información
                }
            }
        }
        else
        {
            if (isLookingAtPlanet)  // Si el raycast ya no detecta nada
            {
                isLookingAtPlanet = false;
                GameManager.Instance.isDatos = false;  // Desactivar el panel de información
            }
        }
    }
}
