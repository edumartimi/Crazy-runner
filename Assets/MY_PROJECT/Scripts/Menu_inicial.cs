using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_inicial : MonoBehaviour
{
    public Animator animador;
    public Slider volumegeral;
    public AudioSource musica;
    public float value;


    // Start is called before the first frame update
    void Start()
    {
        volumegeral.value =  AudioListener.volume;
        if (Time.timeScale == 0) 
        {
            Time.timeScale = 1; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        value = volumegeral.value;
        AudioListener.volume = value;
        Application.targetFrameRate = 60;
    }

    public void iniciar()
    {
        SceneManager.LoadScene(1);
    }

    public void configuracoes()
    {
        animador.SetBool("fecharmenu", false);
        animador.SetTrigger("configmenu");
    }

    public void fecharmenu() 
    {
        animador.SetBool("fecharmenu", true);
    }

}
