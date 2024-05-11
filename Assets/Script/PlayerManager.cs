using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public Camera Camera;

    private Vector3 _velocity;
    private bool _jumpPressed;

    private CharacterController _controller;

    public float PlayerSpeed = 0.5f;
    public float JumpForce = 5f;
    private float GravityValue = -3f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        if (_controller.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
            Debug.LogError("’n–Ê");
        }

        Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        Vector3 move = cameraRotationY * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * PlayerSpeed;

        _velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && _controller.isGrounded)
        {
            _velocity.y += JumpForce;
        }
        _controller.Move(move + _velocity * Runner.DeltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        _jumpPressed = false;
    }

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = Camera.main;
            Camera.GetComponent<FirstPersonCamera>().Target = transform;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpPressed = true;
            Debug.LogError("Space");
        }
    }
}
