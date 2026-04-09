using UnityEngine;

public class ink_guy_sounds : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip shootSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayShoot()
    {
        audioSource.PlayOneShot(shootSound);
    }
}