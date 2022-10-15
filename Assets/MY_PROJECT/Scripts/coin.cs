using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class coin : MonoBehaviour
{
    public GameObject moedasUI;
    TMP_Text qtdmoedas;
    int moedas;
    float tempogirar;
    float giro;
    private void Start()
    {
        moedasUI = GameObject.FindGameObjectWithTag("ui_moedas");
        qtdmoedas = moedasUI.GetComponent<TMP_Text>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Player") 
        {
            moedas = System.Int32.Parse(qtdmoedas.text);
            moedas++;
            qtdmoedas.text = "" + moedas;
           
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        tempogirar = tempogirar + Time.deltaTime;
        if (tempogirar > 0.001) 
        {
            giro = giro + 2;
            tempogirar = 0;
        }
        transform.rotation = Quaternion.Euler(-90,giro,0);
    }
}
