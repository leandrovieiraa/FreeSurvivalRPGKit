using System.Collections;
using System.Collections.Generic;
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
    void Update()
    {
        // Check our health to continue
        if (Player.instance.playerStats.currentHealth <= 0)
            return;

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

            // Get the distance to the focus
            if(focus != null)
            {
                // get distance
                float distance = Vector3.Distance(focus.transform.position, transform.position);

                // check distance
                if (distance <= focus.GetComponent<EnemyController>().lookRadius)
                {
                    // If game object has enemy tag you entered in battle
                    if (focus.transform.tag == "Enemy" && focus.GetComponent<CharacterStats>().currentHealth > 0)
                    {
                        // In Battle
                        GetComponent<CharacterCombat>().Battle();
                        GetComponent<CharacterCombat>().healthSlider.gameObject.SetActive(true);

                        // log
                        Debug.Log("Enemy in range " + focus.gameObject.name);

                        // click
                        if (Input.GetMouseButtonDown(1))
                        {
                            // Call Interact
                            focus.gameObject.GetComponent<Enemy>().Interact();
                        }
                    }
                }
            }
            else
            {
                // no targets
                GetComponent<CharacterCombat>().Normal();
                GetComponent<CharacterCombat>().healthSlider.gameObject.SetActive(false);
            }
        }
        else
        {
            // Check if player are using joystick or keyboard
            // This can help to active some controllers UI and change some things ingame, its all on you.
            // Ww create a list of all connected joysticks

            List<string> controllers = new List<string>();
            controllers.AddRange(Input.GetJoystickNames());

            // Prevent bug on first play mode
            if (controllers.Count == 0) controllers.Add("");

            // Prevent bug on plug and unplug joystick
            if (controllers[0] != "" && controllers.Count > 0)
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

                if (h > 0.1f || h < -0.1f || v > 0.1f || v < -0.1f)
                {
                    motor.WASDMove(false);
                }
            }
        }
    }

    private void OnTriggerStay(Collider ingameObj)
    {
        // not apply for point click
        if (pointClickMovement)
            return;

        // check item object
        if(ingameObj != null)
        {
            // If gamne object has item tag you can pick up this object
            if (ingameObj.transform.tag == "Item")
            {
                Debug.Log("Press Keyboard >E< or Joystick >X< to interact with: " + ingameObj.name);
                if (Input.GetButtonDown("Interact"))
                {
                    // Call Interact (Pick up)
                    ingameObj.GetComponent<ItemPickup>().Interact();
                }
            }

            // If game object has enemy tag you entered in battle
            if (ingameObj.transform.tag == "Enemy" && ingameObj.GetComponent<CharacterStats>().currentHealth > 0)
            {
                GetComponent<CharacterCombat>().Battle();
                GetComponent<CharacterCombat>().healthSlider.gameObject.SetActive(true);
                FaceTarget(ingameObj.transform);

                Debug.Log("Enemy in range " + gameObject.name);

                if (Input.GetButtonDown("Interact"))
                {
                    // Call Interact
                    ingameObj.gameObject.GetComponent<Enemy>().Interact();
                }
            }
        }
    }

    private void OnTriggerExit(Collider ingameObj)
    {
        // not apply for point click
        if (pointClickMovement)
            return;
      
        // Object not in range anymore
        if (ingameObj.transform.tag == "Item")
            Debug.Log("Object " + ingameObj.name + " not in range anymore");

        // Enemy  not in range anymore
        if (ingameObj.transform.tag == "Enemy")
        {
            GetComponent<CharacterCombat>().Normal();
            GetComponent<CharacterCombat>().healthSlider.gameObject.SetActive(false);
            ingameObj.GetComponent<CharacterCombat>().healthSlider.gameObject.SetActive(false);
            Debug.Log("Enemy " + ingameObj.name + " not in range anymore");
        }
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

    void FaceTarget(Transform target)
    {
        // Only face target when player character is not in movement
        float currentSpeed = GetComponent<CharacterAnimator>().animator.GetFloat("Speed Percent");
        if (currentSpeed > 0.5)
            return;

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
