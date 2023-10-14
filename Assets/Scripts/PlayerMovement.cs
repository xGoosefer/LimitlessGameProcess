using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveValue = 5f; // speed at which the character moves
    public float JumpPower = 10f; // Jump strength
    Rigidbody rb; // calls upon the player's rigidbody in unity
    bool movement;
    private int jump = 2; // TOOO: Find a faster jump method
    bool jumpR = false;
    bool onPlatform = false;
    //public GameBehavior Game;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // WASD key movement

        if (Input.GetKey(KeyCode.A))
            TurnLeft();
        else if (Input.GetKey(KeyCode.D))
            TurnRight();
        else if (Input.GetKey(KeyCode.W))
            MoveForward();
        else if (Input.GetKey(KeyCode.S))
            MoveBackward();
        if (Input.GetKeyDown(KeyCode.Space) && (jump > 0))
        {
            jump--;
            jumpR = true;
        }
        // if the player can jump standing still, Jump method will be called
        if (jumpR)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            


        }
        // allow the player the ability to jump while moving at the same time.
        if (movement && jumpR)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            


        }

    }

    private void FixedUpdate()
    {
        movement = false;



    }

    // transform.Translate allows the character to move backwards per second the key is pressed
    public void MoveBackward()
    {

        transform.Translate(-1 * Vector3.forward * Time.deltaTime * MoveValue);
        movement = true;
    }

    // transform.Translate allows the character to move forward per second the key is pressed

    public void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * MoveValue);
        movement = true;

    }

    // Left rotates the character in a counterclockwise direction
    public void TurnLeft()
    {
        transform.Rotate(0, -1, 0);
        //transform.Translate(Vector3.forward * Time.deltaTime * MoveValue);
        movement = true;

    }

    // Right rotates the character in a clockwise direction
    public void TurnRight()
    {
        transform.Rotate(0, 1, 0);
        //transform.Translate(Vector3.forward * Time.deltaTime * MoveValue);
        movement = true;

    }

    // applies the jump
    public void Jump()
    {// ForceMode.Impulse allows the object to jump over time rather than just appearing there
        rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        Debug.LogFormat("jump");
        //jump = false;
        jumpR = false;

    }


    // record the collisons the player has
    public void OnCollisionEnter(Collision collision)
    {
        GameObject collObj = collision.gameObject;
        // while player touches the floor, they have the abilitly to jump.
        if (collObj.CompareTag("floor"))
        {
            jump = 2;
            Debug.LogFormat("true to floor");
        }
    }
}
