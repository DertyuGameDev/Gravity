using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    private DialogueGenerator _dialogueGenerator;

    private void Awake()
    {
        _dialogueGenerator = GetComponent<DialogueGenerator>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.D)) return;

        _dialogueGenerator.PlayDialogue();
    }
}