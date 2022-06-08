using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeCollision : MonoBehaviour
{
    private Cube cube;

    private void Awake()
    {
        cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        if (otherCube != null && cube.CubeID>otherCube.CubeID)
        {
            if (cube.cubeNumber == otherCube.cubeNumber)
            {
                Vector3 contactPoint = collision.contacts[0].point;
                if (otherCube.cubeNumber < CubeSpawner.instance.maxCubeNumber)
                {
                    Cube newCube = CubeSpawner.instance.Spawn(cube.cubeNumber * 2, contactPoint + Vector3.up * 1.6f);
                    float pushForce = 2.5f;
                    newCube.cubeRigidbody.AddForce(new Vector3(0,0.3f,1f)*pushForce,ForceMode.Impulse);
                    // add some torque
                    float randomValue = Random.Range(-20f, 20f);
                    Vector3 randomDirection = Vector3.one*randomValue;
                    newCube.cubeRigidbody.AddTorque(randomDirection);
                }

                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 400f;
                float explosionRadius = 1.5f;
                foreach (Collider coll in surroundedCubes)
                {
                    if (coll.attachedRigidbody != null)
                    {
                        coll.attachedRigidbody.AddExplosionForce(explosionForce,contactPoint,explosionRadius);
                    }
                }
                FX.instance.PlayCubeExplosionFX(contactPoint,cube.cubeColor);
                CubeSpawner.instance.DestroyCube(cube);
                CubeSpawner.instance.DestroyCube(otherCube);
            }
        }
    }
}
