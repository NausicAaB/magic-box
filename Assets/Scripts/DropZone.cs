using UnityEngine;

public class DropZone : MonoBehaviour
{
    public Transform[] slots;
    private bool[] slotOccupied = new bool[4];

    void Start()
    {
        for (int i = 0; i < slotOccupied.Length; i++)
        {
            slotOccupied[i] = false;
        }
    }

    public Vector3 GetAvailableSlotPosition()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slotOccupied[i])
            {
                slotOccupied[i] = true;
                return slots[i].position;
            }
        }
        
        return transform.position;
    }

    public bool HasTag(string tag)
    {
        return gameObject.tag == tag;
    }
}