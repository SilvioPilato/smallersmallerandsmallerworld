using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

    public LayerMask planeMask;
    public float walkSpeed = 5f;
    public float pushStrenght = 5f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float frontMove = Input.GetAxis("Vertical");
        float strafeMove = Input.GetAxis("Horizontal");
        
        transform.LookAt(getFacingDirection());
        if (Input.GetButtonDown("Fire2")) {
            rb.AddRelativeForce(getMoveDirection(strafeMove, frontMove) * pushStrenght, ForceMode.Impulse);
        }
        else
        {
            rb.AddRelativeForce(getMoveDirection(strafeMove, frontMove) * walkSpeed, ForceMode.Force);
        }

        

    }


    private Vector3 getMoveDirection(float xAxis, float yAxis) {

        return Vector3.forward * yAxis + Vector3.right * xAxis;

    }

    private Vector3 getFacingDirection()
    {
        RaycastHit hitInfo;
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(cameraRay, out hitInfo, float.PositiveInfinity, planeMask );
        
        return new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
    }


}
