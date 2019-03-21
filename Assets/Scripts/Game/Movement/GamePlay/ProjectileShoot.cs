using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    public InputButton ShootButton;

    public GameObject ProjectilePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(ShootButton.KeyName))
        {
            var gameObject = Instantiate(
                ProjectilePrefab, 
                this.transform.position,
                this.transform.rotation, 
                this.gameObject.transform);
        }
    }
}
