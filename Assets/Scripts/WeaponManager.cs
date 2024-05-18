using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public int damage;

    public float fireRate;

    private float nextFire;
    
    [Header("VFX")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    [Header("Ammo")]
    public int mag = 5;
    
    public int ammo = 30;
    public int magAmmo = 30;
    
    public Camera camera;

    public GameObject muzzle;
    
    [Header("UI")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI magText;
    
    [Header("Anim")]
    public Animation animation;
    public AnimationClip reloadAnim;

    [Header("Recoil Settings")] 
    // [Range(0, 1)]
    // public float recoilPercent = 0.3f;
    
    [Range(0,2)]
    [SerializeField] public float recoverPercent = 0.7f;

    [Space]
    [SerializeField] public float recoilUp = 1f;
    [SerializeField]public float recoilBack = 1f;
    

    
    private Vector3 originalPosition;
    private Vector3 recoilVelocity = Vector3.zero;

    private float recoilLength;
    private float recoverLength;

    private bool recoiling;
    private bool recovering;
    private void Start()
    {
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
        
        originalPosition = transform.localPosition;

        recoilLength = 0;
        recoverLength = 1 / recoverPercent;
    }

    private void Update()
    {
        if(nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
        
        
        if (Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0 && animation.isPlaying == false)
        {
            nextFire = 1 / fireRate;

            ammo--;
            
            magText.text = mag.ToString();
            ammoText.text = ammo + "/" + magAmmo;
            
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.R) && mag >0)
        {
            Reload();
        }
        
        if(recoiling)
        {
            Recoil();
        }
        
        if(recovering)
        {
            Recover();
        }
    }

    private void Shoot()
    {
        recoiling = true;
        recovering = false;
        
       Ray ray = new Ray(camera.transform.position, camera.transform.forward);
       
       RaycastHit hit;
       
       muzzleFlash.Play();
       if(Physics.Raycast(ray.origin, ray.direction, out hit, 100))
       {
           if(hit.transform.gameObject.GetComponent<HealthManager>())
           {
               //PhotonNetwork.LocalPlayer.AddScore(10);
               if(damage > hit.transform.gameObject.GetComponent<HealthManager>().health)
               {
                   RoomManager.Instance.kills++;
                   RoomManager.Instance.SetHashes();
                   PhotonNetwork.LocalPlayer.AddScore(100);
               }
               
               hit.collider.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
           }
       }
    }

    private void Reload()
    {
        animation.Play(reloadAnim.name);
        if(mag > 0)
        {
            mag--;
            
            ammo = magAmmo;
            
           
        }
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
    }

    void Recoil()
    {
        Vector3 finalPosition = new Vector3(originalPosition.x , originalPosition.y + recoilUp, originalPosition.z - recoilBack);
        
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoilLength);
        
        if(transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = true;
        }
    }
    
    void Recover()
    {
        Vector3 finalPosition = originalPosition;
        
        
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, originalPosition, ref recoilVelocity, recoverLength);
        
        if(transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = true;
        }
    }
}
