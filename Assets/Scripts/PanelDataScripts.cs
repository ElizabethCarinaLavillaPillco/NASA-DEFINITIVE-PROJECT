using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using TMPro; // Asegúrate de que tienes la referencia a TextMeshPro
using UnityEngine;
using System.IO;

public class PanelDataScript : MonoBehaviour
{
    private string dbPath;
    public TextMeshProUGUI textMeshPro; // Asigna tu componente TextMeshPro en el Inspector

    // Este método se llama cuando el script se habilita
    void OnEnable()
    {
        // Suscribirse al evento para cuando se seleccione un planeta
        GameManager.OnPlanetChanged += ReadData;
    }

    // Este método se llama cuando el script se deshabilita
    void OnDisable()
    {
        // Desuscribirse del evento para evitar errores cuando el objeto se deshabilite
        GameManager.OnPlanetChanged -= ReadData;
    }

    // Leer datos de la tabla
    void ReadData()
    {
        // Crear la ruta de la base de datos
        dbPath = Path.Combine(UnityEngine.Application.persistentDataPath, "PlanetsDatabase.db");

        using (var connection = new SqliteConnection("URI=file:" + dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string selectedPlanet = GameManager.Instance.selectedPlanetName;

                // Verificar si el nombre es correcto
                Debug.Log("Nombre del planeta que se buscará en la base de datos: " + selectedPlanet);

                if (string.IsNullOrEmpty(selectedPlanet))
                {
                    Debug.LogError("El nombre del planeta seleccionado es nulo o vacío.");
                    textMeshPro.text = "Error: El nombre del planeta seleccionado es inválido.";
                    return;
                }

                // Realizar la consulta con el nombre del planeta
                command.CommandText = "SELECT * FROM Planets WHERE Name = @planetName;";
                command.Parameters.Add(new SqliteParameter("@planetName", selectedPlanet));

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string planetName = reader["Name"].ToString();
                        string planetInfo = reader["Info"].ToString();

                        textMeshPro.text = $"Planeta: {planetName}\nInformación: {planetInfo}";
                    }
                    else
                    {
                        Debug.LogError("Planeta no encontrado en la base de datos: " + selectedPlanet);
                        textMeshPro.text = "Planeta no encontrado en la base de datos.";
                    }
                }
            }

            connection.Close();
        }
    }
}