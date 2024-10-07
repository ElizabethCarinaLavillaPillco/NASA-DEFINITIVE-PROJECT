using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;
using System.IO;

public class SD : MonoBehaviour
{
    private string dbPath;

    void Start()
    {
        // Crear la ruta de la base de datos
        dbPath = Path.Combine(UnityEngine.Application.persistentDataPath, "PlanetsDatabase.db");

        // Crear la base de datos si no existe
        CreateDatabase();

        // Crear una tabla
        CreateTable();
    }

    // Crear la base de datos si no existe
    void CreateDatabase()
    {
        if (!File.Exists(dbPath))
        {
            UnityEngine.Debug.Log("Creando base de datos en " + dbPath);
            File.Create(dbPath).Dispose();
        }
    }

    // Crear una tabla si no existe
    void CreateTable()
    {
        using (var connection = new SqliteConnection("URI=file:" + dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                // Crear la tabla Planets con las columnas Id, Name e Info
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Planets (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                            Name TEXT, 
                                            Info TEXT
                                        );";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        UnityEngine.Debug.Log("Tabla 'Planets' creada (si no existía ya).");
    }

}