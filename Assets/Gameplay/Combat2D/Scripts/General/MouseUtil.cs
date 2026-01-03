using UnityEngine;

public static class MouseUtil
{
    private static Camera camera;

    public static void BindCamera(Camera cam)
    {
        camera = cam;
    }

    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("MouseUtil: Camera.main introuvable");
            return Vector3.zero;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float t = (zValue - ray.origin.z) / ray.direction.z;
        if (t < 0)
            return Vector3.zero;

        return ray.origin + ray.direction * t;
        /*

        Plane dragPlane = new(camera.transform.forward, new Vector3(0, 0, zValue));
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(dragPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
        */
    }
}
