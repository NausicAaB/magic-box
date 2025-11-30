using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip errorSound;   
    
    private AudioSource audioSource;
    private int placedObjects = 0;
    private int totalObjects = 16;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        UpdateCounter();
    }

    public void ObjectPlaced()
    {
        placedObjects++;
        UpdateCounter();
        
        if (successSound != null)
            audioSource.PlayOneShot(successSound);

        if (placedObjects >= totalObjects)
        {
            Victory();
        }
    }
    
    public void ObjectPlacedWrong()
    {
        if (errorSound != null)
            audioSource.PlayOneShot(errorSound);
    }

    void UpdateCounter()
    {
        counterText.text = "Shelved objects : " + placedObjects + "/" + totalObjects;
    }

    void Victory()
    {
        SceneManager.LoadScene(2);
    }
}