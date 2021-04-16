using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    Camera mainCamera;
    Player player;
    Rigidbody playerRigidbody;


    private void Start()
    {
        mainCamera = Camera.main;
        player = GameManager.instance.GetPlayer();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        LayerMask floorMask = LayerMask.GetMask("Camera Ray Layer");

        Debug.DrawRay(player.transform.position, player.transform.forward * 5.0f, Color.green);

        if (Physics.Raycast(cameraRay, out hit, float.PositiveInfinity, floorMask))
        {
            Debug.DrawLine(cameraRay.origin, hit.point, Color.red);
            Vector3 hitPointYAdjusted = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
            player.transform.LookAt(hitPointYAdjusted, player.transform.up);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            player.Attack();
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVector += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVector += Vector3.right;
        }

        playerRigidbody.velocity = (moveVector.normalized * movementSpeed);
    }

    private void OnCollisionExit(Collision collision)
    {
        // nullify all velocity or angular velocity gained from colliding with enemies
        if (collision.gameObject.tag == "Enemy")
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
