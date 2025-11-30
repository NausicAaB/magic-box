using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip errorSound;  
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private Image progressBarFill;
    
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
        UpdateProgressBar();
    }

    public void ObjectPlaced()
    {
        placedObjects++;
        UpdateCounter();
        UpdateProgressBar();
        
        if (successSound != null)
            audioSource.PlayOneShot(successSound);
        
        if (placedObjects == 8)
        {
            DialogueManager.Instance.ShowMidGameDialogue();
        }

        if (placedObjects >= totalObjects)
        {
            Victory();
        }
    }
    
    public void PlaySuccessParticles(Vector3 position)
    {
        if (successParticles != null)
        {
            successParticles.transform.position = position;
            successParticles.Play();
        }
    }
    
    public void ObjectPlacedWrong()
    {
        if (errorSound != null)
            audioSource.PlayOneShot(errorSound);
        DialogueManager.Instance.ShowErrorDialogue();
    }

    void UpdateCounter()
    {
        counterText.text = "Shelved objects : " + placedObjects + "/" + totalObjects;
    }
    
    void UpdateProgressBar()
    {
        if (progressBarFill != null)
        {
            float progress = (float)placedObjects / (float)totalObjects;
            StartCoroutine(AnimateProgressBar(progress));
        }
    }
    
    private System.Collections.IEnumerator AnimateProgressBar(float targetFill)
    {
        float currentFill = progressBarFill.fillAmount;
        float duration = 0.3f;
        float elapsed = 0f;
    
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            progressBarFill.fillAmount = Mathf.Lerp(currentFill, targetFill, elapsed / duration);
            yield return null;
        }
    
        progressBarFill.fillAmount = targetFill;
    }

    void Victory()
    {
        SceneManager.LoadScene(2);
    }
}