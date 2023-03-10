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
    public Animator camera_anim;
    public Slider volumegeral;
    public AudioSource musica;
    public float value;

    public Animator anim_aj;
    public Animator anim_michelle;

    public GameObject[] corredores;


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
        print(corredores.Length);
    }

    public void iniciar()
    {
        SceneManager.LoadScene(1);
    }

    public void configuracoes()
    {
        animador.SetBool("fecharmenu", false);
        animador.SetTrigger("configmenu");
        camera_anim.SetTrigger("mudar_camera");
    }

    public void fecharmenu() 
    {
        animador.SetBool("fecharmenu", true);
        camera_anim.SetTrigger("voltar_camera");
    }


    public void trocar_de_personagem_michelle() 
    {
        gerenciador_troca.numplayer = 0;
        anim_michelle.SetTrigger("escolhido");
    }

    public void trocar_de_personagem_aj() 
    {
        gerenciador_troca.numplayer = 1;
        anim_aj.SetTrigger("escolhido");
    }
}
