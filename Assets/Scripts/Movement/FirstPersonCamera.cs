using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;

    private float verticalRotation;
    private float horizontalRotation;

    public Transform grabPoint;

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = new Vector3(Target.position.x, Target.position.y + 1.675f, Target.position.z); //y value is same as the eye height

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * MouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

        horizontalRotation += mouseX * MouseSensitivity;

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

        Target.rotation = Quaternion.Euler(Target.rotation.x, horizontalRotation, 0);
    }
}
