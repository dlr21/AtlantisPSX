using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Velocidades")]
    public float playerSpeed;
    public float gravity;
    public float jumpForce;
    [SerializeField] private float fallSpeed;

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

    private void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
    }

    private void Update()
    {
        Inputs();
        camDirection();

        Move();

        Debug.Log(player.velocity.magnitude);
    }

    //movimiento con arreglo diagonal
    private void Inputs() {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput,1);

        //animacion de que hacemos segun la velocidad
        playerAnimatorController.SetFloat("playerWalkVelocity", playerInput.magnitude * playerSpeed);
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
        
        if (player.isGrounded)
        {
            fallSpeed = -gravity*Time.deltaTime;
            playerMove.y = fallSpeed;
        }
        else {
            fallSpeed -= gravity * Time.deltaTime;
            playerMove.y = fallSpeed;
            //animacion de que hacemos segun la velocidad en el aire
            playerAnimatorController.SetFloat("playerVerticalVelocity", player.velocity.y);
        }
        playerAnimatorController.SetBool("isGrounded", player.isGrounded);
    }

    private void Jump() {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallSpeed = jumpForce;
            playerMove.y = fallSpeed;
            playerAnimatorController.SetTrigger("playerJump");
        }
    }

    private void OnAnimatorMove()
    {
        
    }

}
