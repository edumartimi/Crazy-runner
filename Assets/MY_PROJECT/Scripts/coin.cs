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
    Player jogador;
      
    private void Start()
    {
        moedasUI = GameObject.FindGameObjectWithTag("ui_moedas");
        qtdmoedas = moedasUI.GetComponent<TMP_Text>();
        jogador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Player") 
        {
            jogador.tocarmoeda();
            moedas = System.Int32.Parse(qtdmoedas.text);
            moedas++;
            qtdmoedas.text = "" + moedas;
            Destroy(gameObject);
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

        if (jogador.IMADEMOEDAS) 
        {
            irparaplayer();
        }

    }

    public void irparaplayer() 
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)<30) {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up, 2f);
        }
    }
}
