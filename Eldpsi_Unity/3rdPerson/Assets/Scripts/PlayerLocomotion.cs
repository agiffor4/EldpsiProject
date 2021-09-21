using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Transform camObject;
    Vector3 moveDir;
    Rigidbody rb;

    public float playerMoveSpeed = 7;
    public float playerRotationSpeed = 15;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        camObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDir = camObject.forward * inputManager.verticalInput;
        moveDir = moveDir + camObject.right * inputManager.horizontalInput;
        moveDir.Normalize();
        moveDir.y = 0;
        moveDir = moveDir * playerMoveSpeed;

        Vector3 movementVelocity = moveDir;
        rb.velocity = movementVelocity;

    }
    private void HandleRotation()
    {
        Vector3 targetDir = Vector3.zero;
        targetDir = camObject.forward * inputManager.verticalInput;
        targetDir = targetDir + camObject.right * inputManager.horizontalInput;
        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir ==Vector3.zero)
        {
            targetDir = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, playerRotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }



}
