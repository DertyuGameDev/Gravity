using UnityEngine;

public class DialogueTimerStart : MonoBehaviour
{
    private DialogueGenerator _dialogueGenerator;

    [SerializeField] private float startDialogueAfter;

    private float _timer;
    private void Awake()
    {
        _dialogueGenerator = GetComponent<DialogueGenerator>();
        _timer = startDialogueAfter;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0f)
        {
            _dialogueGenerator.PlayDialogue();
            gameObject.SetActive(false);
        }
    }
}