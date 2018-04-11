using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/* Controls the player. Here we chose our "focus" and where to move. */

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	public delegate void OnFocusChanged(Interactable newFocus);
	public OnFocusChanged onFocusChangedCallback;

    [Header("[Controller Settings]")]
    public bool pointClickMovement = false;  // Check if you want a point and click Controller
    public bool usingJoystick = false;       // Joystick are detect automatically
    public string joystickName = "";         // Joystick name

    [Header("[Interactable Settings]")]
    public Interactable focus;               // Our current focus: Item, Enemy etc.

    [Header("[Movement Settings]")]
    public LayerMask movementMask;           // The ground
	public LayerMask interactionMask;        // Everything we can interact with

	PlayerMotor motor;                       // Reference to our motor
	public Camera cam;                       // Reference to our camera

    // Get references
    void Start ()
	{
		motor = GetComponent<PlayerMotor>();
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update () {

		if (EventSystem.current.IsPointerOverGameObject())
			return;

       // Check point click controller
        if (pointClickMovement)
        {
            usingJoystick = false;
            joystickName = null;

            // If we press left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // Shoot out a ray
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // If we hit
                if (Physics.Raycast(ray, out hit, movementMask))
                {
                    motor.MoveToPoint(hit.point);

                    SetFocus(null);
                }
            }

            // If we press right mouse button
            if (Input.GetMouseButtonDown(1))
            {
                // Shoot out a ray
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // If we hit
                if (Physics.Raycast(ray, out hit, 100f, interactionMask))
                {
                    SetFocus(hit.collider.GetComponent<Interactable>());
                }
            }
        }
        else
        {
            // Check if player are using joystick or keyboard
            // This can help to active some controllers UI and change some things ingame, its all on you.
            string[] controllers = Input.GetJoystickNames();
            if (controllers[0] != "")
            {
                joystickName = controllers[0];
                usingJoystick = true;
                Debug.Log("Using Controller: " + joystickName);

                // WASD
                float h = Input.GetAxis("Horizontal Joystick");
                float v = Input.GetAxis("Vertical Joystick");

                if (h > 0.1f || h < -0.1f || v > 0.1f || v < -0.1f)
                {
                    motor.WASDMove(true);
                }
            }
            // Has no active joysticks, using keyboard controller.
            else
            {
                joystickName = "Keyboard";
                usingJoystick = false;
                Debug.Log("Using Keyboard");

                // WASD
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                if (h > 0.1f || h < -0.1f || v > 1f || v < -0.1f)
                {
                    motor.WASDMove(false);
                }
            }        
        }
	}

    private void OnTriggerStay(Collider item)
    {
        // not apply for point click
        if (pointClickMovement)
            return;

        // check item object
        if(item != null)
        {
            // If has item tag you can pick up this object
            if (item.gameObject.transform.tag == "Item")
            {
                Debug.Log("Press Keyboard >E< or Joystick >X< to interact with: " + item.name);
                if (Input.GetButtonDown("Interact"))
                {
                    // Call Interact (Pick up)
                    item.gameObject.GetComponent<ItemPickup>().Interact();
                }
            }
        }
    }

    private void OnTriggerExit(Collider item)
    {
        // not apply for point click
        if (pointClickMovement)
            return;

        // Object not in range anymore
        if (item.gameObject.transform.tag == "Item")
            Debug.Log("Away from object: " + item.name);     
    }

    // Set our focus to a new focus
    void SetFocus (Interactable newFocus)
	{
		if (onFocusChangedCallback != null)
			onFocusChangedCallback.Invoke(newFocus);

		// If our focus has changed
		if (focus != newFocus && focus != null)
		{
			// Let our previous focus know that it's no longer being focused
			focus.OnDefocused();
		}

		// Set our focus to what we hit
		// If it's not an interactable, simply set it to null
		focus = newFocus;

		if (focus != null)
		{
			// Let our focus know that it's being focused
			focus.OnFocused(transform);
		}

	}

}
