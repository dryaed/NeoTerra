using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [FormerlySerializedAs("playerInput")] [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private PlayerMovement playerMovement;

    public float interactionRayLength = 5;

    public LayerMask groundMask;


    public bool fly = false;

    public Animator animator;

    bool isWaiting = false;

    public World world;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        playerController = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
        world = FindObjectOfType<World>();
    }

    private void Start()
    {
        playerController.OnMouseClick += HandleMouseClick;
        playerController.OnFly += HandleFlyClick;
    }

    private void HandleFlyClick()
    {
        fly = !fly;
    }

    void Update()
    {
        if (fly)
        {
            animator.SetFloat("speed", 0);
            animator.SetBool("isGrounded", false);
            animator.ResetTrigger("jump");
            playerMovement.Fly(playerController.MovementInput, playerController.IsJumping, playerController.RunningPressed);

        }
        else
        {
            animator.SetBool("isGrounded", playerMovement.IsGrounded);
            if (playerMovement.IsGrounded && playerController.IsJumping && isWaiting == false)
            {
                animator.SetTrigger("jump");
                isWaiting = true;
                StopAllCoroutines();
                StartCoroutine(ResetWaiting());
            }
            animator.SetFloat("speed", playerController.MovementInput.magnitude);
            playerMovement.HandleGravity(playerController.IsJumping);
            playerMovement.Walk(playerController.MovementInput, playerController.RunningPressed);


        }

    }
    IEnumerator ResetWaiting()
    {
        yield return new WaitForSeconds(0.1f);
        animator.ResetTrigger("jump");
        isWaiting = false;
    }

    private void HandleMouseClick()
    {
        Ray playerRay = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(playerRay, out hit, interactionRayLength, groundMask))
        {
            ModifyTerrain(hit);
        }
    }

    private void ModifyTerrain(RaycastHit hit)
    {
        world.SetBlock(hit, BlockType.Air);
    }

}
