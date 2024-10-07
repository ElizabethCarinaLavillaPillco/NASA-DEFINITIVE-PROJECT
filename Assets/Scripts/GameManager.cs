using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PanelPausa;
    public GameObject PanelDatos;
    public GameObject LetraActive;
    public GameObject SubMenu;

    public string selectedPlanetName;

    public SelectorPlanetas selectorPlanetas;  // Referencia al script SelectorPlanetas
    public GameObject[] planetPrefabs;  // Prefabs de los planetas que quieres instanciar
    public Transform spawnPoint;
    private GameObject currentPlanet;

    private bool firstInstantiation = true;

    public bool isDatos;
    public bool isLetraActive;
    public bool IsPause = false;

    public static event Action OnPlanetChanged; // Evento para notificar el cambio de planeta

    private static GameManager instance;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        // Implementación Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SubMenu.SetActive(false);
        LetraActive.SetActive(false);
        PanelDatos.SetActive(false);
        PanelPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Ispausa();
        }

        PanelDatos.SetActive(isDatos);

        if (isLetraActive)
        {
            LetraActive.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Desactivar LetraActive y activar SubMenu
                LetraActive.SetActive(false);
                SubMenu.SetActive(true);
                isLetraActive = false;  // Cambiar el estado
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            LetraActive.SetActive(false);
        }

        // Si se desea implementar el comportamiento de "Visitar", añadir el método aquí
    }

    public void Ispausa()
    {
        IsPause = !IsPause;
        Cursor.visible = IsPause;
        Cursor.lockState = IsPause ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = IsPause ? 0f : 1f;
        PanelPausa.SetActive(IsPause);
    }

    public void InstanciarPlaneta()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        int index = selectorPlanetas.selectedPlanetIndex;
        if (index >= 0 && index < planetPrefabs.Length)
        {
            selectedPlanetName = selectorPlanetas.selectedPlanetName;

            // Instanciar el planeta
            if (firstInstantiation)
            {
                currentPlanet = Instantiate(planetPrefabs[index], spawnPoint.position, Quaternion.identity);
                firstInstantiation = false;
            }
            else if (currentPlanet != null && currentPlanet.name == planetPrefabs[index].name)
            {
                Debug.Log("El mismo planeta ya está instanciado.");
                return;
            }
            else
            {
                Vector3 currentPlanetPosition = currentPlanet.transform.position;
                Destroy(currentPlanet);
                currentPlanet = Instantiate(planetPrefabs[index], currentPlanetPosition, Quaternion.identity);
            }

            // Llamar al evento después de que se actualice el nombre del planeta
            OnPlanetChanged?.Invoke();
        }
    }

    // Método para restaurar las funciones de juego al hacer clic en "Visitar"
    public void Visitar()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SubMenu.SetActive(false);
        // Otras acciones que necesites al visitar...
    }
}
