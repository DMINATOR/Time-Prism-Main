using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Tooltip("Button to shoot with")]
    public InputButton ShootButton;

    [Tooltip("Prefab to use to shoot with")]
    public GameObject ProjectilePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(ShootButton.KeyName))
        {
            OpenWorldController.Instance.CurrentBlock.Projectiles.Spawn(ProjectilePrefab, this.transform.position, this.transform.rotation);
        }
    }
}
