using UnityEngine;

public class DropZone : MonoBehaviour
{
    public Transform[] slots; // Les 4 positions d'emplacement
    private bool[] slotOccupied = new bool[4]; // Savoir quels slots sont occupés

    void Start()
    {
        // Initialiser tous les slots comme libres
        for (int i = 0; i < slotOccupied.Length; i++)
        {
            slotOccupied[i] = false;
        }
    }

    public Vector3 GetAvailableSlotPosition()
    {
        // Trouver le premier slot libre
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slotOccupied[i])
            {
                slotOccupied[i] = true; // Marquer comme occupé
                return slots[i].position;
            }
        }
        
        // Si tous les slots sont pleins, retourner le centre de la zone
        return transform.position;
    }

    public bool HasTag(string tag)
    {
        return gameObject.tag == tag;
    }
}