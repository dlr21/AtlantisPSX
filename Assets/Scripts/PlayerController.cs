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

    [Header("Camaras")]
    public Camera cam;
    [SerializeField] private Vector3 camForward;
    [SerializeField] private Vector3 camRight;
    public FirstPerson fpScript;
    public Camera fpCam;

    [Header("Botones")]
    public string botonPrimeraPersona = "Fire2";//clic derecgho
    public string botonPausaInventario = "Cancel";//escape
    public string botonCorrer = "Fire3";//shift
    public string botonAgacharse = "Crouch";//c

    private CharacterController player;
    private Vector3 playerMove;

    [Header("CrouchValors")]
    [SerializeField] private float crouchHeigh = 1f;
    [SerializeField] private float auxHeigh = 1.8f;

    public Vector3 crouchCenter;
    public Vector3 auxCenter;


    [Header("Animaciones")]
    [SerializeField] private Animator playerAnimatorController;

    [Header("Agarre")]
    [SerializeField] private bool colgando;
    public Ledge ledge;


    [Header("Estado")]
    [SerializeField] private int state; //1 suelo, 2 agua no respira, 3 agua respira
    public bool running;
    public bool pause;
    public bool dialog;
    public bool firstPerson;
    public bool noInputs;


    private void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
        running = false;
        colgando = false;
        dialog = false;
        firstPerson = false;
        noInputs = false;
        state = 1;
        auxplayerSpeed = playerSpeed;
        auxfallSpeed = fallSpeed;

    }

    private void Update()
    {

        if (!noInputs)
        {

            MenuInputs();


            if (!pause)
            {
                InputFP();
                if (!firstPerson)
                {
                    InputMove();
                    camDirection();
                    Move();
                    Crouch();
                }
            }
            /*else if (pause) {
                if (dialog) {

                }
            }*/
        }
    }


    //INPUTS//
    //movimiento con arreglo diagonal
    private void InputMove() {

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
        
        if (Input.GetButtonDown(botonPausaInventario) && pause)//salir de pausa
        {
            ExitMenu();
        }else if (Input.GetButtonDown(botonPausaInventario) && !pause)//entrar en pausa
        {
            PauseGame();
            pause = true;
            ShowInventory();
        }

    }

    private void InputFP() {

        if (Input.GetButtonDown(botonPrimeraPersona))
        {
            FirstPerson(true);
        }else if(Input.GetButtonUp(botonPrimeraPersona)){
            FirstPerson(false);
        }
            

    }

    void isRunning()
    {
        if (running && state == 1 && Input.GetButtonDown("Fire3"))
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

    void Crouch() {

        if (Input.GetKeyDown(botonAgacharse))
        {
            playerAnimatorController.SetBool("Crouch",true);
            CrouchHitboxOn();
        }

        if (Input.GetKeyUp(botonAgacharse))
        {
            playerAnimatorController.SetBool("Crouch", false);
            CrouchHitboxOff();
        }

    }
    //INPUTS//

    void CrouchHitboxOn() {
        player.height = crouchHeigh;
        Debug.Log(player.center);
        player.center = crouchCenter;
        Debug.Log(crouchCenter);
    }

    void CrouchHitboxOff()
    {
        player.height = auxHeigh;
        player.center = auxCenter;
    }

    public void ExitMenu() {
        PauseGame();
        pause = false;
        HideInventory();
    }

    public void UsedKey() {
        PauseGame();
        pause = false;
        HideInventory();
    }

    public void Dialog(Vector3 npc) {
        if (!pause)
        {
            transform.LookAt(npc+new Vector3(0,transform.position.y,0));
            pause = true;
            playerAnimatorController.SetFloat("playerWalkVelocity", 0);
        }
        else if (pause) {
            pause = false;
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
                noInputs = true;
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
        PlayerPosition(new Vector3(player.transform.position.x,ledge.endPos.y, player.transform.position.z) );
        noInputs = false;
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

        if (Input.GetButtonDown("Jump")) {
            //salto desde tierra
            if (player.isGrounded && state != 2)
            {
                fallSpeed = jumpForce;
                playerMove.y = fallSpeed;
                playerAnimatorController.SetTrigger("playerJump");
            }

            ////agua respira saktasalta
            if (state == 3)
            {
                fallSpeed = jumpForce;
                playerMove.y = fallSpeed;
                state = 0;
                playerAnimatorController.SetBool("isSwimming", false);
                playerAnimatorController.SetTrigger("playerJump");
            }

            ///agua no respira intenta subir
            if (state == 2 )
            {
                fallSpeed += gravity * 3 * Time.deltaTime;
                playerMove.y = fallSpeed;
            }
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

    public void FirstPerson(bool f){
        if (f)
        {
            firstPerson = true;
            //DESACTIVAR HUD
            fpScript.Reset();
            cam.gameObject.SetActive(false);
            fpCam.gameObject.SetActive(true);

        }
        else {
            firstPerson = false;
            //ACTIVAR HUD
            fpScript.Reset();
            fpCam.gameObject.SetActive(false);
            cam.gameObject.SetActive(true);

        }

    }










}
