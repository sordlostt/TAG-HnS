using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField]
    Vector3 CameraOffset;
    [SerializeField]
    public Vector3 CameraRotation;

    private Player Player;

    private void Start()
    {
        Player = GameManager.instance.GetPlayer();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x + CameraOffset.x, CameraOffset.y, Player.transform.position.z + CameraOffset.z);
        transform.rotation = Quaternion.Euler(CameraRotation);
    }
}
