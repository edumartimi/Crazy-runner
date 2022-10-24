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
    Vector3 posicaoesquerda;
    Vector3 posicaodireita;
    Vector3 posicaomeio;
    Vector3 ir_para_baixo;

    Vector3 pulo;
    Vector3 forsuperpulo;
    public float posicao;
    public float forcapulo;
    public float graviteforce;
    bool tanochao;
    bool superpulo;
    float TimeSpPulo;
    float TimeImaMoedas;


    float tempoanimacao;
    bool direita;
    bool esquerda;
    bool noar;
    bool temlateral_dir;
    bool temlateral_esq;
    int contador_morte;


    int estado;
    bool morte;

    int dispontuacao;
    Vector3 lugarinicial;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    bool pulando;

    public GameObject TempoPoder;

    public bool IMADEMOEDAS;

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "chao") 
        {
        estado = 0;
        }
       
        if(collision.gameObject.tag == "obstaculo") 
        {
            morte = true;
            animador.SetTrigger("morte");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SUPER_PULO") 
        {
            superpulo = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "lateral_dir")
        {
            temlateral_dir = true;
        }
        if (other.gameObject.tag == "lateral_esq")
        {
            temlateral_esq = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "lateral_dir")
        {
            temlateral_dir = false;
        }
        if (other.gameObject.tag == "lateral_esq")
        {
            temlateral_esq = false;
        }

        if (other.gameObject.tag == "coin") 
        {
            coin moeda = other.gameObject.GetComponent<coin>();
            moeda.efeito.Play(1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            tanochao = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            tanochao = false;
            contador_morte = 0;
        }
    }


    void Start()
    {
        morte=false;
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 0;
        posicao = 0;
        tanochao = false;
        superpulo = false;
        TempoPoder.SetActive(false);
        lugarinicial = transform.position;
}

    


    void Update()
    {

        dispontuacao = System.Convert.ToInt32(transform.position.z - lugarinicial.z);

        Contarpontuacao(dispontuacao);


        if (contador_morte >= 2) 
        {
            morte = true;
        }
        
        if (superpulo) 
        {
            TempoPoder.SetActive(true);
            TimeSpPulo = TimeSpPulo + Time.deltaTime;
            TempoPoder.GetComponent<Scrollbar>().size = TimeSpPulo * 0.2f;
            if (TimeSpPulo > 5) 
            {
                TimeSpPulo = 0;
                superpulo =false;
                TempoPoder.SetActive(false);
            }
        }

        if (IMADEMOEDAS) 
        {
            TempoPoder.SetActive(true);
            TimeImaMoedas = TimeImaMoedas + Time.deltaTime;
            TempoPoder.GetComponent<Scrollbar>().size = TimeImaMoedas * 0.2f;
            if (TimeImaMoedas > 20) 
            {
                TimeImaMoedas = 0;
                IMADEMOEDAS = false;
                TempoPoder.SetActive(false);
            }


        }




        
        //variaveis animação
        animador.SetInteger("estado", estado);
        animador.SetFloat("velocityY", rb.velocity.y);
        animador.SetBool("tanochao", tanochao);

        estado = 0;


        Swipe();

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !temlateral_esq)
        {
            esquerda = true;
            estado = 2;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && temlateral_esq)
        {
            contador_morte++;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !temlateral_dir)
        {
            direita = true;
            estado = 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && temlateral_dir)
        {
            contador_morte++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animador.SetTrigger("cambalhota");
            rb.AddForce(new Vector3(0, -200, 0), ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Space) && tanochao)
        {
            pulando = true;
        }
        if (pulando && !superpulo)
        {
            pulando = false;
            rb.AddForce(pulo, ForceMode.Impulse);
        }
        else if(pulando && superpulo)
        {
            pulando = false;
            rb.AddForce(forsuperpulo, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (!morte)
        {
            rb.AddForce(new Vector3(0, graviteforce, 0), ForceMode.Force);

            //ir pra frente e acelerar
            ir_para_frente = new Vector3(rb.velocity.x, rb.velocity.y, 1 * velocidade);

            rb.velocity = ir_para_frente;
            tempo = tempo + Time.deltaTime;
            if (tempo > 0.1f)
            {
                tempo = 0;
                if (velocidade < 80)
                {
                    velocidade = velocidade + 0.05f;
                }
            }
            //-------------------------------------------------------------------------------------------------------------------------

            //ir para os lados

            posicaoesquerda = new Vector3(-3.5f, transform.position.y, transform.position.z);
            posicaodireita = new Vector3(3.5f, transform.position.y, transform.position.z);
            posicaomeio = new Vector3(0, transform.position.y, transform.position.z);
            andarfrente = new Vector3(0, 0, velocidade);
            pulo = new Vector3(0, forcapulo, 0);
            forsuperpulo = new Vector3(0, forcapulo * 1.3f, 0);
            ir_para_baixo = new Vector3(rb.velocity.x, -10, rb.velocity.z);


            if (esquerda) 
            {
                if (posicao == 0)
                {
                    rb.MovePosition(posicaoesquerda);
                    posicao = 1;
                }
                else if (posicao == 2) 
                {
                    rb.MovePosition(posicaomeio);
                    posicao = 0;
                }
                esquerda = false;
            }

            if (direita)
            {
                if (posicao == 0)
                {
                    rb.MovePosition(posicaodireita);
                    posicao = 2;
                }
                else if (posicao == 1)
                {
                    rb.MovePosition(posicaomeio);
                    posicao = 0;
                }
                direita = false;
            }


        }      
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
        {
        currentPosition = Input.GetTouch(0).position;
        Vector2 Distance = currentPosition - startTouchPosition;


            if (!stopTouch)
                {
                if (Distance.x < -swipeRange)
                {
                    esquerda = true;
                    estado = 2;
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    direita = true;
                    estado = 1;
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange)
                {
                    rb.AddForce(new Vector3(0, -200, 0), ForceMode.Impulse);
                    animador.SetTrigger("cambalhota");
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange && tanochao) 
                {
                    pulando = true;
                    stopTouch = true;
                }
            }
        }
     
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) 
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(Distance.x)< tapRange && Mathf.Abs(Distance.y) < tapRange) 
            {
                print("doideira");
            }

        }
        
    }


    void Contarpontuacao(int numpontuacao) 
    {
        if (numpontuacao < 10) 
        {
            pontuacao.text = "0000000" + numpontuacao;
        }
        else if (numpontuacao<99)
        {
            pontuacao.text = "000000" + numpontuacao;
        }
        else if (numpontuacao <999)
        {
            pontuacao.text = "00000" + numpontuacao;
        }
        else if (numpontuacao < 9999)
        {
            pontuacao.text = "0000" + numpontuacao;
        }
        else if (numpontuacao < 99999)
        {
            pontuacao.text = "000" + numpontuacao;
        }
        else if (numpontuacao < 999999)
        {
            pontuacao.text = "00" + numpontuacao;
        }
        else if (numpontuacao < 9999999)
        {
            pontuacao.text = "0" + numpontuacao;
        }
        else if (numpontuacao < 999999999)
        {
            pontuacao.text = "" + numpontuacao;
        }
    }

   
    
}




