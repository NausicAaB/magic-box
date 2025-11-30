using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    private string[] introDialogues = {
        "I can't sell merchandise if I can't FIND the merchandise! This is affecting my profit margin!",
        "This is exactly why I preferred being a vengeance demon. Curses were SO much tidier."
    };
    
    private string[] errorDialogues = {
        "No. Just... no. Potions don't go with weapons. That's retail 101.",
        "That is NOT the correct shelf. Did you even READ the labels?",
        "I've been organizing inventories for eleven hundred years. Trust me on this one.",
        "Even Xander could figure this out. And Xander is... Xander."
    };
    
    private string[] midGameDialogues = {
        "Okay, we're making progress. There might be hope for you yet.",
        "Half done. Which means we're also half NOT done, so keep moving.",
        "See? Organization leads to efficiency. Efficiency leads to profit. Profit leads to... more profit!"
    };

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ShowRandomDialogue(introDialogues);
    }

    public void ShowErrorDialogue()
    {
        ShowRandomDialogue(errorDialogues);
    }
    
    public void ShowMidGameDialogue()
    {
        ShowRandomDialogue(midGameDialogues);
    }

    private void ShowRandomDialogue(string[] dialogues)
    {
        if (dialogues.Length > 0)
        {
            int randomIndex = Random.Range(0, dialogues.Length);
            dialogueText.text = dialogues[randomIndex];
        }
    }
}