using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroCinematica : MonoBehaviour
{
    public Image displayImage;
    public Sprite[] images;

    // AudioSources
    public AudioSource loopSource1; // portal
    public AudioSource loopSource2; // typing
    public AudioSource sfxSource;   // efectos

    // Clips
    public AudioClip alert;
    public AudioClip portal;
    public AudioClip print;
    public AudioClip stomp;
    public AudioClip typing;

    private int currentIndex = 0;

    void Start()
    {
        ShowImage();
        ControlAudio(currentIndex);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // click izquierdo
        {
            NextImage();
        }
    }

    void NextImage()
    {
        currentIndex++;

        if (currentIndex >= images.Length)
        {
            SceneManager.LoadScene("Inicio");
            return;
        }

        ShowImage();
        ControlAudio(currentIndex);
    }

    void ShowImage()
    {
        displayImage.sprite = images[currentIndex];
    }

    void ControlAudio(int index)
    {
        // ---------- TYPING (Intro1–4) ----------
        if (index >= 0 && index <= 3)
        {
            if (loopSource2.clip != typing)
            {
                loopSource2.clip = typing;
                loopSource2.loop = true;
                loopSource2.Play();
            }
        }
        else
        {
            if (loopSource2.clip == typing)
                loopSource2.Stop();
        }

        // ---------- PORTAL (Intro9–11) ----------
        if (index >= 8 && index <= 10)
        {
            if (loopSource1.clip != portal)
            {
                loopSource1.clip = portal;
                loopSource1.loop = true;
                loopSource1.Play();
            }
        }
        else
        {
            if (loopSource1.clip == portal)
                loopSource1.Stop();
        }

        // ---------- SONIDOS PUNTUALES ----------
        if (index == 5) // Intro6
        {
            sfxSource.PlayOneShot(print);
        }

        if (index == 6) // Intro7
        {
            sfxSource.PlayOneShot(stomp);
        }

        if (index == 7) // Intro8
        {
            sfxSource.PlayOneShot(alert);
        }
    }
}