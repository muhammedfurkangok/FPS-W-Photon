using System;
using UnityEngine;
public class WaponBehaviourController : MonoBehaviour
{
    
    public float swayClamp = 0.89f;

    public float smoothing = 3f;

    private Vector3 origin;

    private void Start()
    {
        origin = transform.localPosition;
    }

    private void Update()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        mouseInput.x = Math.Clamp( mouseInput.x, -swayClamp, swayClamp);
        mouseInput.y = Math.Clamp( mouseInput.y, -swayClamp, swayClamp);
        
        Vector3 targetPosition = new Vector3(-mouseInput.x, -mouseInput.y, 0);
        
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition + origin, Time.deltaTime * smoothing);
    }
}
