using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    float MovementSpeed;

    Camera MainCamera;
    Player Player;

    private void Start()
    {
        MainCamera = Camera.main;
        Player = GameManager.instance.GetPlayer();
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
            Player.transform.LookAt(hit.point, Player.transform.up);
        }

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

        Player.transform.Translate(moveVector * MovementSpeed * Time.deltaTime, Space.World);
    }
}
