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
}
