using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class botones : MonoBehaviour
{
    public Button comenzar;
    public Button configuracion;

    [SerializeField] private Slider music;
    [SerializeField] private Slider SFX;

    public void ComenzarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void IrAConfiguracion()
    {
        SceneManager.LoadScene("Configuration");
    }

    public void IrAInicio()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void ValorMusica()
    {
        Debug.Log("Valor actual de la música: " + music.value);
    }

    public void ValorSFX()
    {
        Debug.Log("Valor actual del SFX: " + SFX.value);
    }

}
