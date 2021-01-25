using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    public Slider cameraSpeedSlide;
    public ManagerScript manager;
    private float _zoom;
    private float _x;
    private float _y;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        _zoom = Input.GetAxis("Mouse ScrollWheel") * 10;
        _x = Input.GetAxis("Horizontal");
        _y = Input.GetAxis("Vertical");

        // Move camera
        transform.Translate(new Vector3(_x * -cameraSpeedSlide.value, _y * -cameraSpeedSlide.value, 0.0f));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -50, 180), Mathf.Clamp(transform.position.y, -20, 20), Mathf.Clamp(transform.position.z, -20, 20));

        // Zoom by increasing or decreasing camera orthographic size between -25 and -5
        int min = -25;
        int max = -5;

        if (_zoom < 0 && _camera.orthographicSize >= min)
        {
            _camera.orthographicSize -= _zoom * -cameraSpeedSlide.value;
        }

        if (_zoom > 0 && _camera.orthographicSize <= max)
        {
            _camera.orthographicSize += _zoom * cameraSpeedSlide.value;
        }
    }
}
