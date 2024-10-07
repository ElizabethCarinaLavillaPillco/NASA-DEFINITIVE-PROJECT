using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerMovement = 1f, jumpForce = 1f, fallVelocity, gravedad = 1f;

    private Vector3 Axis, movePlayer;
    private CharacterController Player;

    void Awake()
    {
        
    }

    void Start()
    {
        Player = GetComponent<CharacterController>();
    }

    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");

        Axis = new Vector3(horizontal, 0f, vertical);

        transform.Rotate(0, mouseX, 0);
        if (Axis.magnitude > 1) Axis = transform.TransformDirection(Axis).normalized;
        else Axis = transform.TransformDirection(Axis);

        movePlayer.x = Axis.x;
        movePlayer.z = Axis.z;
        setGravity();

        Player.Move(movePlayer * playerMovement * Time.deltaTime);
    }

    private void setGravity()
    {
        if (Player.isGrounded)
        {
            fallVelocity = -gravedad * Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                fallVelocity = jumpForce;
            }
        }
        else
        {
            fallVelocity -= gravedad * Time.deltaTime;
        }
        movePlayer.y = fallVelocity;
    }
}
