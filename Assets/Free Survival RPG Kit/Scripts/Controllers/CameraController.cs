using UnityEngine;

/* Makes the camera follow the player */

public class CameraController : MonoBehaviour {

	public Transform target;

	public Vector3 offset;
	public float smoothSpeed = 2f;

	public float currentZoom = 1f;
	public float maxZoom = 3f;
	public float minZoom = .3f;
	public float yawSpeed = 70;
	public float zoomSensitivity = .7f;
	float dst;

	float zoomSmoothV;
	float targetZoom;

	void Start() {
		dst = offset.magnitude;
		transform.LookAt (target);
		targetZoom = currentZoom;
	}

	void Update ()
	{
        // Check if Joystick are connected to change scroll whell event
        // Use this for make better controller, using mouse or joystick axis.
        PlayerController pController = target.gameObject.GetComponent<PlayerController>();
        float scroll;
        if (pController.usingJoystick)
            scroll = Input.GetAxisRaw("Mouse ScrollWheel Joystick") * zoomSensitivity;
        else
            scroll = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity;

        if (scroll != 0f)
		{
			targetZoom = Mathf.Clamp(targetZoom - scroll, minZoom, maxZoom);
		}
		currentZoom = Mathf.SmoothDamp (currentZoom, targetZoom, ref zoomSmoothV, .15f);
	}

	void LateUpdate () {
		transform.position = target.position - transform.forward * dst * currentZoom;
		transform.LookAt(target.position);

       // detect player variable for check controllers and apply on camera rotate system
        PlayerController pController = target.gameObject.GetComponent<PlayerController>();
        
        // point click
        if (pController.pointClickMovement)
        {
            float yawInput;
            yawInput = Input.GetAxisRaw("Horizontal");
            transform.RotateAround(target.position, Vector3.up, -yawInput * yawSpeed * Time.deltaTime);
        }
        else
        {
            // keyboard
            if (Input.GetMouseButton(1) && !pController.usingJoystick)
            {
                float yawInput;
                yawInput = Input.GetAxisRaw("Mouse X");
                transform.RotateAround(target.position, Vector3.up, -yawInput * yawSpeed * Time.deltaTime);
            }
            // joystick
            else
            {
                float yawInput;
                yawInput = Input.GetAxisRaw("Joystick Camera Rotate");
                transform.RotateAround(target.position, Vector3.up, -yawInput * yawSpeed * Time.deltaTime);
            }
        }            
	}
}
