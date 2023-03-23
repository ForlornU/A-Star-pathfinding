using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        Vector3 input = GetInput().normalized;
        input.y *= speed;

        transform.Translate(input * speed * Time.deltaTime, Space.World);
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
