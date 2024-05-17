using UnityEngine;

public class FaceObjectToTheCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
