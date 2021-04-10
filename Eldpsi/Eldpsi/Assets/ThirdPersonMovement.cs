using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public enum playerState {Idle, Dodging, Attacking, Jumping }
    playerState currState;
    public GameObject playerObject;
    public Renderer playerRenderer;
    public Rigidbody rb;
    public float forceAmt;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerRenderer = playerObject.GetComponent <Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Test keys
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerStateSwitch(playerState.Dodging);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerStateSwitch(playerState.Jumping);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            playerStateSwitch(playerState.Attacking);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            playerStateSwitch(playerState.Idle);
        }


        //Movement
        float horizontal = Input.GetAxisRaw("Horizontal"); //between -1,1 AD
        float vertical = Input.GetAxisRaw("Vertical"); //between -1,1 WS

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
    
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; //Angle to correct the player character to face the direction they are moving. 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    
    }

    void playerStateSwitch(playerState state)
    {
        switch(state)
        {
            case playerState.Idle:
                playerRenderer.material.color = Color.white;
                currState = playerState.Idle;
                print("Idle");
                break;
            case playerState.Attacking:
                playerRenderer.material.color = Color.red;
                currState = playerState.Attacking;
                //Perform Attack
                print("Attacking");
                break;
            case playerState.Jumping:
                playerRenderer.material.color = Color.green;
                currState = playerState.Jumping;
                //Perform Jump
                print("Jumping");
                break;
            case playerState.Dodging:
                playerRenderer.material.color = Color.blue;
                currState = playerState.Dodging;
                //Perform Dodge
                Dodge();
                print("Dodging");
                break;         
        }
    }

    void Dodge()
    {
       // rb.AddForce(transform.forward * forceAmt, ForceMode.Force);

    }

}
