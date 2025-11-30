using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 originalPosition;
    private float originalZ;
    private bool isPlaced = false; 

    void Start()
    {
        originalPosition = transform.position;
        originalZ = transform.position.z;
    }

    void OnMouseDown()
    {
        if (isPlaced) return;

        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
        
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
        
        // Remettre le Z original
        transform.position = new Vector3(transform.position.x, transform.position.y, originalZ);
        
        // Vérifier si l'objet est sur une étagère
        CheckShelfPlacement();
    }

    void CheckShelfPlacement()
    {
        // Faire un raycast pour voir ce qu'il y a sous l'objet
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("DropZones"));
    
        if (hit.collider != null)
        {
            DropZone zone = hit.collider.GetComponent<DropZone>();
        
            if (zone != null)
            {
                string objectTag = this.gameObject.tag;
            
                Debug.Log("Objet tag: " + objectTag + " | Zone tag: " + zone.tag);
            
                // Vérifier si les tags correspondent
                if (zone.HasTag(objectTag))
                {
                    // BRAVO ! Bon placement
                    Debug.Log("BON PLACEMENT !");
                    Vector3 slotPosition = zone.GetAvailableSlotPosition();
                    PlaceOnShelf(slotPosition);
                    GameManager.Instance.ObjectPlaced();
                }
                else
                {
                    // ERREUR ! Mauvais placement
                    Debug.Log("MAUVAIS PLACEMENT !");
                    ReturnToStart();
                }
            }
            else
            {
                ReturnToStart();
            }
        }
        else
        {
            // Relâché dans le vide
            Debug.Log("Relâché dans le vide");
            ReturnToStart();
        }
    }

    void PlaceOnShelf(Vector3 shelfPosition)
    {
        // Placer l'objet sur l'étagère
        transform.position = shelfPosition;
        isPlaced = true;
        
        // TODO: Jouer un son de succès
        // TODO: Notifier le GameManager
    }

    void ReturnToStart()
    {
        // Retourner à la position de départ
        transform.position = originalPosition;
        
        // TODO: Jouer un son d'erreur
        // TODO: Animation de "shake"
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.nearClipPlane + 10f;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}