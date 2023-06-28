using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direccion;
    private Animator animator;

    [Header("estadisticas")]
    public float velocidad_de_movimiento = 6;
    public float fuerza_de_salto = 10;

    [Header("colisiones")]
    public Vector2 abajo;
    public float radio_de_colision;
    public LayerMask layer_piso;

    [Header("booleanos")]
    public bool se_puede_mover = true;
    public bool en_suelo = true;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        float velocidady = rb.velocity.y > 0 ? 1 : -1;
        direccion = new Vector2(x, y);

        caminar();
        mejorar_salto();

        if(!en_suelo){
            animator.SetFloat("velocidad_vertical",velocidady);
        }else if (en_suelo && velocidady == -1){
            animator.SetBool("saltar",false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (en_suelo)
            {
                animator.SetBool("saltar",true);
                saltar();
            }
        }
        if(Input.GetKeyDown(KeyCode.X)){
            Camera.main.GetComponent<RippleEffect>().Emit(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.GetComponent<RippleEffect>().Emit(transform.position);
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
                if(en_suelo){
                    animator.SetBool("caminar",true);
                }
                if (direccion.x < 0 && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                if (direccion.x > 0 && transform.localScale.x < 0) {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }else{
                animator.SetBool("caminar",false);
            }
        }
    }
    private void agarres() {
        en_suelo = Physics2D.OverlapCircle((Vector2)transform.position + abajo, radio_de_colision, layer_piso);
    }
}
