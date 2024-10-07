using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorPlanetas : MonoBehaviour
{
    public GameObject olding;  // Objeto actual que est� activo
    public GameObject[] newing;  // Arreglo de objetos a seleccionar
    public Dropdown myDD;  // Dropdown para la selecci�n

    public string selectedPlanetName;  // Nombre del planeta seleccionado

    public int selectedPlanetIndex;  // �ndice del planeta seleccionado

    void Start()
    {
        // Configurar el evento cuando cambie la selecci�n del Dropdown
        myDD.onValueChanged.AddListener(delegate { ActualizarObjeto(myDD); });
    }

    public void ActualizarObjeto(Dropdown dropdown)
    {
        // Actualizar el �ndice y el nombre del planeta seleccionado
        selectedPlanetIndex = dropdown.value;
        selectedPlanetName = selectorPlanetas.selectedPlanetName;
        //selectedPlanetName = dropdown.options[dropdown.value].text;  // Obtener el nombre del planeta del dropdown

        // Desactivar el objeto anterior si existe
        if (olding != null)
        {
            olding.SetActive(false);
        }

        // Asignar y activar el nuevo objeto seleccionado
        olding = newing[selectedPlanetIndex];
        olding.SetActive(true);

        Debug.Log("Nombre del planeta seleccionado: " + GameManager.Instance.selectedPlanetName);
    }
}

