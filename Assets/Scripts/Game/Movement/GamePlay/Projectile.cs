using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileLocator))]
public class Projectile : MonoBehaviour
{
    [Tooltip("Force for projectile to move at")]
    public float Force;

    public ProjectileLocator Locator;

    Vector3 _startVelocity;

    private void Awake()
    {
        _startVelocity = Vector3.forward * Force; 
    }

    private void OnEnable()
    {
        Locator.RigidBody.position = this.gameObject.transform.position;
        Locator.RigidBody.rotation = this.gameObject.transform.rotation;
        Locator.RigidBody.velocity = this.gameObject.transform.rotation * _startVelocity;
    }
}
