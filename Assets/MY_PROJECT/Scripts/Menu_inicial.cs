using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_inicial : MonoBehaviour
{
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
        
    }

    public void iniciar()
    {
        SceneManager.LoadScene(1);
    }
}
