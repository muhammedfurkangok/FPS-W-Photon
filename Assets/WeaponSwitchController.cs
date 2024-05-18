using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchController : MonoBehaviour
{
    public Animation anim;
    public AnimationClip draw;
    
   private int selectedWeapon = 0;
    
    
    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
       
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }
        
         if (Input.GetKeyDown(KeyCode.Alpha3))
         {
             selectedWeapon = 2;
         }
         
         if(Input.GetAxis("Mouse ScrollWheel") > 0f)
         {
             if (selectedWeapon >= transform.childCount - 1)
             {
                 selectedWeapon = 0;
             }
             else
             {
                 selectedWeapon++;
             }
         }
         
         if(Input.GetAxis("Mouse ScrollWheel") < 0f)
         {
             if (selectedWeapon <= 0)
             {
                 selectedWeapon = transform.childCount - 1;
             }
             else
             {
                 selectedWeapon--;
             }
         }
         
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        if(selectedWeapon >= transform.childCount)
            selectedWeapon = transform.childCount - 1;
        anim.Stop();
        anim.Play(draw.name);
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
