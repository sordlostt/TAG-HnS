using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField]
    Vector3 cameraOffset;
    [SerializeField]
    public Vector3 cameraRotation;

    private Player Player;

    private void Start()
    {
        Player = GameManager.instance.GetPlayer();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x + cameraOffset.x, cameraOffset.y, Player.transform.position.z + cameraOffset.z);
        transform.rotation = Quaternion.Euler(cameraRotation);
    }
}
