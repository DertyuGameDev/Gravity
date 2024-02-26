using UnityEngine;

public class TempleDoor : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip doorSound;

    private bool _isOpen;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = doorSound;
    }

    public void SwitchDoorState()
    {
        _isOpen = !_isOpen;
        UpdateAnimatorParameter();
        PlaySound();
    }

    public void SetDoorState(bool newIsOpen)
    {
        if (newIsOpen == _isOpen) return;

        _isOpen = newIsOpen;
        UpdateAnimatorParameter();
        PlaySound();
    }

    private void PlaySound()
    {
        _audioSource.Stop();
        _audioSource.Play();
    }

    private void UpdateAnimatorParameter()
    {
        _animator.SetBool(TempleDoorAnimatorParameters.IsOpen, _isOpen);
    }
}

public static class TempleDoorAnimatorParameters
{
    public static readonly int IsOpen = Animator.StringToHash("isOpen");
}