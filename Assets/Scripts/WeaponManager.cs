using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public int damage;

    public float fireRate;

    private float nextFire;
    
    [Header("VFX")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    
    public Camera camera;

    private void Update()
    {
        if(nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
        
        
        if (Input.GetButton("Fire1") && nextFire <= 0)
        {
            nextFire = 1 / fireRate;

            Shoot();
        }
    }

    private void Shoot()
    {
       Ray ray = new Ray(camera.transform.position, camera.transform.forward);
       
       RaycastHit hit;
       
       if(Physics.Raycast(ray.origin, ray.direction, out hit, 100))
       {
           if(hit.transform.gameObject.GetComponent<HealthManager>())
           {
               hit.collider.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
           }
       }
    }
}
