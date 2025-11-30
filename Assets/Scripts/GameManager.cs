using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton
    
    [SerializeField] private TextMeshProUGUI counterText;
    private int placedObjects = 0;
    private int totalObjects = 16;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateCounter();
    }

    public void ObjectPlaced()
    {
        placedObjects++;
        UpdateCounter();

        if (placedObjects >= totalObjects)
        {
            Victory();
        }
    }

    void UpdateCounter()
    {
        counterText.text = "Shelved objects : " + placedObjects + "/" + totalObjects;
    }

    void Victory()
    {
        Debug.Log("VICTOIRE !");
        // TODO: Charger scène de victoire
    }
}