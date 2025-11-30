using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    [SerializeField] private string objectName = "Mystery Object";
    
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 originalPosition;
    private float originalZ;
    private bool isPlaced = false; 
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        originalPosition = transform.position;
        originalZ = transform.position.z;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }
    
    void OnMouseEnter()
    {
        if (!isPlaced && spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 1f, 0.7f, 1f);
            TooltipManager.Instance.ShowTooltip(objectName, transform.position);
        }
    }
    
    void OnMouseExit()
    {
        if (!isPlaced && spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
            TooltipManager.Instance.HideTooltip();
        }
    }

    void OnMouseDown()
    {
        if (isPlaced) return;

        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
        
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
        
        TooltipManager.Instance.HideTooltip();
        
        transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, -5f);
        }
    }

    void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;
        
        transform.position = new Vector3(transform.position.x, transform.position.y, originalZ);
        
        CheckShelfPlacement();
    }

    void CheckShelfPlacement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("DropZones"));
    
        if (hit.collider != null)
        {
            DropZone zone = hit.collider.GetComponent<DropZone>();
        
            if (zone != null)
            {
                string objectTag = this.gameObject.tag;
            
                if (zone.HasTag(objectTag))
                {
                    Vector3 slotPosition = zone.GetAvailableSlotPosition();
                    PlaceOnShelf(slotPosition);
                    GameManager.Instance.ObjectPlaced();
                    TooltipManager.Instance.HideTooltip();
                }
                else
                {
                    ReturnToStart();
                    GameManager.Instance.ObjectPlacedWrong();
                }
            }
            else
            {
                ReturnToStart();
                GameManager.Instance.ObjectPlacedWrong();
            }
        }
        else
        {
            ReturnToStart();
        }
    }

    void PlaceOnShelf(Vector3 shelfPosition)
    {
        transform.position = shelfPosition;
        isPlaced = true;
        
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
        GameManager.Instance.PlaySuccessParticles(shelfPosition);
    }
    
    void ReturnToStart()
    {
        StartCoroutine(ShakeAnimation());
    }
    
    private System.Collections.IEnumerator ShakeAnimation()
    {
        Vector3 startPos = transform.position;
        float shakeDuration = 0.3f;
        float shakeAmount = 0.3f;
        float elapsed = 0f;
        
        while (elapsed < shakeDuration)
        {
            float x = startPos.x + Random.Range(-shakeAmount, shakeAmount);
            float y = startPos.y + Random.Range(-shakeAmount, shakeAmount);
            
            transform.position = new Vector3(x, y, transform.position.z);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = originalPosition;
    }


    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.nearClipPlane + 10f;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}