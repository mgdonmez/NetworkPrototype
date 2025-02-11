using UnityEngine;

public static class Utils
{
    public static void SetRenderLayerInChildren(Transform transform, int layerNumber)
    {
        foreach(Transform trans in transform.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    public static void SetFirstPersonCamera(Transform playerTransform)
    {
        Camera.main.GetComponent<FirstPersonCamera>().Target = playerTransform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static Transform GetGrabPoint()
    {
        return Camera.main.GetComponent<FirstPersonCamera>().grabPoint;
    }
}
