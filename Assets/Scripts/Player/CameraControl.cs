using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 8f;
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 input = GetInput().normalized;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + input.y * 2, 30, 110);
        transform.Translate(input.Flat() * speed * Time.deltaTime, Space.World);
    }

    private Vector3 GetInput()
    {
        Vector3 values = new Vector3();
        values.x = Input.GetAxis("Horizontal");
        values.z = Input.GetAxis("Vertical");
        values.y = Input.GetAxis("Mouse ScrollWheel");
        return values;
    }

}
