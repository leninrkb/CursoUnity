using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direccion;
    private Animator animtr;

    [Header("estadisticas")]
    public float velocidad_de_movimiento = 6;
    public float fuerza_de_salto = 6;

    [Header("colisiones")]
    public Vector2 abajo;
    public float radio_de_colision;
    public LayerMask layer_piso;

    [Header("booleanos")]
    public bool se_puede_mover = true;
    public bool en_suelo = true;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animtr = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movimiento();
        agarres();
    }

    private void movimiento() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        direccion = new Vector2(x, y);
        caminar();
        mejorar_salto();
        if(en_suelo){
            animtr.SetBool("caer",false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (en_suelo)
            {
                animtr.SetBool("saltar",true);
                saltar();
            }
        }
    }
    private void saltar() {
        rb.velocity = new Vector2(direccion.x, 0);
        rb.velocity += Vector2.up * fuerza_de_salto; 
    }
    private void mejorar_salto() {
        if (rb.velocity.y < 0 )
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (4.0f - 1) * Time.deltaTime;
        }else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2.0f -1) * Time.deltaTime;
        }
    }
    private void caminar() {
        if (se_puede_mover) {
            rb.velocity = new Vector2(direccion.x * velocidad_de_movimiento, rb.velocity.y);
            if (direccion != Vector2.zero)
            {
                if(!en_suelo){
                    animtr.SetBool("caer",true);
                }else{
                    animtr.SetBool("caminar",true);
                }
                if (direccion.x < 0 && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                if (direccion.x > 0 && transform.localScale.x < 0) {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }else{
                animtr.SetBool("caminar",false);
            }
        }
    }
    private void agarres() {
        en_suelo = Physics2D.OverlapCircle((Vector2)transform.position + abajo, radio_de_colision, layer_piso);
    }


    public void terminar_salto(){
        animtr.SetBool("saltar",false);
        animtr.SetBool("caer",true);
    }
}
