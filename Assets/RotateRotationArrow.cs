using UnityEngine;

public class RotateRotationArrow : MonoBehaviour
{
    private void OnEnable()
    {
        transform.eulerAngles = Vector3.right*180;
    }

    private void Update()
    {
        transform.Rotate (0,0,-50*Time.deltaTime);
    }
}
