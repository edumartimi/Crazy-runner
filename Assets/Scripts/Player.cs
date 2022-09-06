using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 ir_para_frente;

    public float tempo;

    public float velocidade;

    public Animator animador;

    public TMP_Text pontuacao;

    float qtdpontuacao;
    float tempopontuacao;
    string qtdzeros = "0";

    Vector3 andarfrente;
    Vector3 andaresquerda;
    Vector3 andardireita;
    Vector3 andarmeio;

    Vector3 pulo;
    public float posicao;
    public float forcapulo;
    public float gravityforce;


    float tempoanimacao;
    bool direita;
    bool esquerda;
    bool noar;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        noar = false;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        Time.timeScale = 1;
        posicao = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tempopontuacao = Time.deltaTime + tempopontuacao;



        ir_para_frente = new Vector3(rb.velocity.x,rb.velocity.y,1*velocidade);

        rb.velocity = ir_para_frente;

        tempo = tempo + Time.deltaTime;
        if (tempo > 0.1f)
        {
            tempo = 0;
            
            if (Time.timeScale < 1.5f)
            {
                Time.timeScale = Time.timeScale + 0.0002f;
            }
            if (velocidade < 40) 
            {
                velocidade = velocidade + 0.005f;
                gravityforce = gravityforce + 0.00009f;
            }

        }


        


        

        andaresquerda = new Vector3(-7, transform.position.y, transform.position.z);
        andardireita = new Vector3(7, transform.position.y, transform.position.z);
        andarmeio = new Vector3(0, transform.position.y, transform.position.z);


        pulo = new Vector3(rb.velocity.x, forcapulo, rb.velocity.z);

        int estado = animador.GetInteger("estado");

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            esquerda = true;
            estado = 3;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direita = true;
            estado = 2;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            noar = true;
            estado = 1;
        }


        if (estado != 0) 
        {
            tempoanimacao = tempoanimacao + Time.deltaTime;
           

            if (tempoanimacao > 0.25f) 
            {
                if (estado == 1) 
                {
                rb.AddForce(pulo,ForceMode.Impulse);
                }
               
                if (direita)
                {
                    direita = false;
                    if (posicao == 0)
                    {
                        transform.position = andardireita;
                        posicao = 1;
                    }
                    else if (posicao == 2)
                    {
                        transform.position = andarmeio;
                        posicao = 0;
                    }

                }
                if (esquerda)
                {
                    esquerda = false;
                    if (posicao == 0)
                    {
                        transform.position = andaresquerda;
                        posicao = 2;
                    }
                    else if (posicao == 1)
                    {
                        transform.position = andarmeio;
                        posicao = 0;
                    }
                }
                tempoanimacao = 0;
                estado = 0;
            }
        }

        
       





        


        rb.AddForce(Vector3.down * gravityforce);






        animador.SetInteger("estado", estado);
    }

}




