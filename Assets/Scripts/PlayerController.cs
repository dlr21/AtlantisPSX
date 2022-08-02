using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Velocidades")]
    public float playerWalkSpeed;
    public float playerRunSpeed;
    [SerializeField] private float playerSpeed;
    private float auxplayerSpeed;
    public float gravity;
    public float jumpForce;
    [SerializeField] private float fallSpeed;
    private float auxfallSpeed;

    [Header("Direcciones")]
    [SerializeField] private float horizontalMove;
    [SerializeField] private float verticalMove;
    [SerializeField] private Vector3 playerInput;

    [Header("MainCamara")]
    public Camera cam;
    [SerializeField] private Vector3 camForward;
    [SerializeField] private Vector3 camRight;

    private CharacterController player;
    private Vector3 playerMove;

    [Header("Animaciones")]
    [SerializeField] private Animator playerAnimatorController;

    [Header("Agarre")]
    [SerializeField] private bool colgando;
    public Ledge ledge;


    [Header("Estado")]
    [SerializeField] private int state; //1 suelo, 2 agua no respira, 3 agua respira
    public bool running;
    public bool pause;


    private void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
        running = false;
        colgando = false;
        state = 1;
        auxplayerSpeed = playerSpeed;
        auxfallSpeed = fallSpeed;

    }

    private void Update()
    {

        MenuInputs();

        if (!pause)
        {
            Inputs();
            camDirection();
            Move();
        }
        else if (pause) {
            
        }
    }

    //movimiento con arreglo diagonal
    private void Inputs() {

        isRunning();

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        //animacion de que hacemos segun la velocidad
        playerAnimatorController.SetFloat("playerWalkVelocity", playerInput.magnitude * playerSpeed);
    }

    private void MenuInputs()
    {
        
        if (Input.GetButtonDown("Cancel") && pause)//salir de pausa
        {
            PauseGame();
            pause = false;
            HideInventory();
        }else if (Input.GetButtonDown("Cancel") && !pause)//entrar en pausa
        {
            PauseGame();
            pause = true;
            ShowInventory();
        }

    }

    void PauseGame()
    {
        if (pause)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    void isRunning() {
        if (running && state==1 && Input.GetButtonDown("Fire3"))
        {
            running = false;
            playerSpeed = playerWalkSpeed;
        }
        else if (!running && state == 1 && Input.GetButtonDown("Fire3"))
        {
            running = true;
            playerSpeed = playerRunSpeed;
        }
    }


    private void Move()
    {

        //movimiento en referencia a la camara
        playerMove = (playerInput.x * camRight + playerInput.z * camForward) * playerSpeed;
        //mover personaje a donde mire
        player.transform.LookAt(player.transform.position + playerMove);

        MovimientoEjeY();

        player.Move(playerMove * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Climb();
    }

    public void CallMeWithWait() { }

    private void  Climb(){

        if (colgando)
        {
            if (playerInput.z > 0)
            {
                playerAnimatorController.SetTrigger("playerClimb"); 
            }
            else if (playerInput.z < 0)
            {
                colgando = false;
                //playerAnimatorController.SetTrigger("playerClimb"); FALL
            }
        }

    }

    public void posClimb()
    {
        PlayerPosition(ledge.endPos);
    }

    //direccion de la que mira la camara
    private void camDirection()
    {
        camForward = cam.transform.forward;
        camRight = cam.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    //aqui para no obstaculizar el look up y no ser borrado por los inputs
    private void MovimientoEjeY()
    {
        SetGravity();
        Jump();
    }

    //fuerza hacia abajo estando en el suelo y aceleracion de gravedad estando en el aire
    private void SetGravity() {


        if (colgando)//ledge
        {
            playerMove.y = fallSpeed;
        }
        else if (player.isGrounded && state!=2)//grounded
        {
            fallSpeed = -gravity * Time.deltaTime;
            playerMove.y = fallSpeed;
            state = 1;
        }
        else if(state==2){//agua no respira

            fallSpeed += gravity * Time.deltaTime;
            if(fallSpeed>0.1)
            {
                fallSpeed = 0;
            }
            playerMove.y = fallSpeed;
        }
        else if (state == 3){//agua  respira
            fallSpeed = 0;
            playerMove.y = fallSpeed;
        }
        else {//air
            fallSpeed -= gravity * Time.deltaTime;
            playerMove.y = fallSpeed;
            //animacion de que hacemos segun la velocidad en el aire
            playerAnimatorController.SetFloat("playerVerticalVelocity", player.velocity.y);
        }
        playerAnimatorController.SetBool("colgando", colgando);
        playerAnimatorController.SetBool("isGrounded", player.isGrounded);
    }

    private void Jump() {
        //salto desde tierra
        if (player.isGrounded && Input.GetButtonDown("Jump") && state!=2)
        {
            fallSpeed = jumpForce;
            playerMove.y = fallSpeed;
            playerAnimatorController.SetTrigger("playerJump");
        }
        ////agua respira saktasalta
        if (state == 3 && Input.GetButtonDown("Jump")) {
            fallSpeed = jumpForce;
            playerMove.y = fallSpeed;
            state = 0;
            playerAnimatorController.SetBool("isSwimming", false);
            playerAnimatorController.SetTrigger("playerJump");
        }
        ///agua no respira intenta subir
        if (state == 2 && Input.GetKey("space"))
        {
            fallSpeed += gravity*3*Time.deltaTime;
            playerMove.y = fallSpeed;
        }
        if (state == 2 && Input.GetKey("left shift"))
        {
            fallSpeed -= gravity *1.5f* Time.deltaTime;
            playerMove.y = fallSpeed;
        }
    }

    public void SetColgando(bool a, Collider other) {
        colgando = a;
        if (colgando)
        {
            StopMoving();

            PlayerLooks(other.transform);
        }
        else if (!colgando) {
            StartMoving();
        }
    }

    public void StopMoving() {
        playerSpeed = 0;
        fallSpeed = 0;
    }

    public void StartMoving() {
        playerSpeed = auxplayerSpeed;
        fallSpeed = auxfallSpeed;
    }

    public void PlayerLooks(Transform target) {
        transform.forward = -target.forward;
    }

    private void OnAnimatorMove()
    {
        
    }

    public void PlayerPosition(Vector3 newPos)
    {
        transform.position =  newPos;
    }

    public void playerState(string estado)
    {

        if (estado.Equals("grounded"))
        {
            state = 1;
            playerAnimatorController.SetBool("isSwimming", false);
        }
        else if (estado.Equals("WaterIn"))
        {
            state = 2;
            playerAnimatorController.SetBool("isSwimming", true);
        }
        else if (estado.Equals("WaterOut"))
        {
            state = 3;
        }
    }

    public void ShowInventory() {
        InventoryManager.instance.Show();
    }

    public void HideInventory()
    {
        InventoryManager.instance.Hide();
    }












}
