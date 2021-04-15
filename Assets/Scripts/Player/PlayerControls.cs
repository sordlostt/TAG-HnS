using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    float MovementSpeed;

    Camera MainCamera;
    Player Player;
    Rigidbody PlayerRigidbody;

    private void Start()
    {
        MainCamera = Camera.main;
        Player = GameManager.instance.GetPlayer();
        PlayerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray cameraRay = MainCamera.ScreenPointToRay(Input.mousePosition);
        LayerMask floorMask = LayerMask.GetMask("Camera Ray Layer");

        Debug.DrawRay(Player.transform.position, Player.transform.forward * 5.0f, Color.green);

        if (Physics.Raycast(cameraRay, out hit, float.PositiveInfinity, floorMask))
        {
            Debug.DrawLine(cameraRay.origin, hit.point, Color.red);
            Vector3 hitPointAdjusted = new Vector3(hit.point.x, Player.transform.position.y, hit.point.z);
            Player.transform.LookAt(hitPointAdjusted, Player.transform.up);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Player.Attack();
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

        PlayerRigidbody.velocity = (moveVector.normalized * MovementSpeed);
    }

    private void OnCollisionExit(Collision collision)
    {
        // nullify all velocity or angular velocity gained from colliding with enemies
        if (collision.gameObject.tag == "Enemy")
        {
            PlayerRigidbody.velocity = Vector3.zero;
            PlayerRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
