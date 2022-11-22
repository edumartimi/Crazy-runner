using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_inicial : MonoBehaviour
{
    public Animator animador;
    public Slider volumegeral;
    public AudioSource musica;

    // Start is called before the first frame update
    void Start()
    {
        if (Time.timeScale == 0) 
        {
            Time.timeScale = 1; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        float value = volumegeral.value;
        musica.volume = value;
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
