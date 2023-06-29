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
    public float velocidad_rodar = 20;

    [Header("colisiones")]
    public Vector2 abajo;
    public float radio_de_colision;
    public LayerMask layer_piso;

    [Header("booleanos")]
    public bool se_puede_mover = true;
    public bool en_suelo = true;
    public bool puede_rodar;
    public bool haciendo_dash;
    public bool tocado_suelo;

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

    private void rodar(float x, float y){
        animator.SetBool("rodar",true);
        // Camera.main.GetComponent<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        puede_rodar = true;
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x,y).normalized * velocidad_rodar;
        StartCoroutine(preparar_rodar());
    }

    private IEnumerator preparar_rodar(){
        StartCoroutine(rodar_suelo());
        rb.gravityScale = 0;
        haciendo_dash = true;
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = 3;
        haciendo_dash = false;
    }

    private IEnumerator rodar_suelo(){
        yield return new WaitForSeconds(0.15f);
        if(en_suelo){
            puede_rodar = false;
        }
    }

    private void tocar_suelo(){
        puede_rodar = false;
        haciendo_dash = false;
    }

    public void finalizar_rodar(){
        animator.SetBool("rodar",false);
    }

    private void movimiento() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float x_raw = Input.GetAxisRaw("Horizontal");
        float y_raw = Input.GetAxisRaw("Vertical");
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
        if(Input.GetKeyDown(KeyCode.X) & !haciendo_dash){
            if(x_raw != 0 || y_raw != 0){
                rodar(x_raw,y_raw);
            }
        }

        if(en_suelo && !tocado_suelo){
            tocar_suelo();
            tocado_suelo = true;
        }

        if(!en_suelo && tocado_suelo){
            tocado_suelo = false;
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
        if (se_puede_mover && !haciendo_dash) {
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
