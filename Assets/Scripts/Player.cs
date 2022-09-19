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
    public float velocidade_mov; 

    public TMP_Text pontuacao;

    float tempopontuacao;

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

    int estado;
    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "chao") 
        {
        estado = 0;
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
        posicao = 0;
    }




    void Update()
    {
        //variaveis animação
        animador.SetInteger("estado", estado);
        animador.SetFloat("velocityY", rb.velocity.y);
       




        //ir pra frente e acelerar
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
        //-------------------------------------------------------------------------------------------------------------------------

        


        //ir para os lados

        andaresquerda = new Vector3(-3.5f, transform.position.y, transform.position.z);
        andardireita = new Vector3(3.5f, transform.position.y, transform.position.z);
        andarmeio = new Vector3(0, rb.velocity.y, transform.position.z);
        andarfrente = new Vector3(0, rb.velocity.y, velocidade);
        pulo = new Vector3(rb.velocity.x, forcapulo, rb.velocity.z);

       

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            esquerda = true;
            estado = 2;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direita = true;
            estado = 1;
        }

       



        if (direita)
        {
            rb.AddForce(Vector3.right * velocidade_mov);
            if (posicao == 0)
            {               
                posicao = 2;
            }
            else if (posicao == 3) 
            {   
                posicao = 0;
            }
            direita = false;
        }


        if (esquerda)
        {
            rb.AddForce(Vector3.left  * velocidade_mov);
            //rb.velocity = Vector3.left * velocidade_mov;
            if (posicao == 0)
            {
                posicao = 3;
            }
            else if (posicao == 2)
            {
                posicao = 0;
            }
            esquerda = false;
        }



        if (posicao == 2 && transform.position.x > andardireita.x) 
        {           
            estado = 0;
            rb.velocity = andarfrente;
        }

        if (posicao == 0 && transform.position.x > -0.1 && transform.position.x < 0.1)
        {           
            estado = 0;
            rb.velocity = andarfrente;
        }

        if (posicao == 3 && transform.position.x < andaresquerda.x)
        {   
            estado = 0;
            rb.velocity = andarfrente;
        }
        //===============================================================================================================================

        //pulo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(pulo , ForceMode.Impulse );
            
        }





        //===============================================================================================================================



    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravityforce);
    }

}




