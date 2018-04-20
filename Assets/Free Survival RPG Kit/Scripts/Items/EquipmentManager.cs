using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EquipmentManager : MonoBehaviour {

	#region Singleton


	public static EquipmentManager instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<EquipmentManager> ();
			}
			return _instance;
		}
	}
	static EquipmentManager _instance;

	void Awake ()
	{
		_instance = this;
	}

	#endregion

	Equipment[] currentEquipment;
    GameObject[] currentObjects;

    [Header("Slots Transform")]
    public Transform headTransform;
    public Transform chestTransform;
    public Transform legsTransform;
    public Transform feetTransform;
    public Transform rightHandTransform;
    public Transform leftHandTransform;

    // Callback for when an item is equipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public event OnEquipmentChanged onEquipmentChanged;

	Inventory inventory;

	void Start ()
	{
		inventory = Inventory.instance;

		int numSlots = System.Enum.GetNames (typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];

        currentObjects = new GameObject[numSlots];   
	}

	void Update() {
        if (Input.GetButtonDown("Unequip"))
        {
            UnequipAll();
        }		
	}

	public Equipment GetEquipment(EquipmentSlot slot) {
		return currentEquipment [(int)slot];
	}

	// Equip a new item
	public void Equip (Equipment newItem)
	{
		Equipment oldItem = null;

		// Find out what slot the item fits in and put it there.
		int slotIndex = (int)newItem.equipSlot;

		// If there was already an item in the slot make sure to put it back in the inventory
		if (currentEquipment[slotIndex] != null)
		{
			oldItem = currentEquipment [slotIndex];
			inventory.Add (oldItem);
		}

		// An item has been equipped so we trigger the callback
		if (onEquipmentChanged != null)
			onEquipmentChanged.Invoke(newItem, oldItem);

		currentEquipment [slotIndex] = newItem;
		Debug.Log(newItem.name + " equipped!");

        // An item has been equipped so equip game object checking your type.
        if (newItem.equipSlot == EquipmentSlot.Weapon)
            AttachToObject(newItem.objectPrefab, slotIndex, rightHandTransform);

        if (newItem.equipSlot == EquipmentSlot.Shield)
            AttachToObject(newItem.objectPrefab, slotIndex, leftHandTransform);
    }

	void Unequip(int slotIndex) {
		if (currentEquipment[slotIndex] != null)
		{
			Equipment oldItem = currentEquipment [slotIndex];
			inventory.Add(oldItem);
				
			currentEquipment [slotIndex] = null;

            if (currentObjects[slotIndex] != null)
                Destroy(currentObjects[slotIndex].gameObject);

            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
				onEquipmentChanged.Invoke(null, oldItem);		
		}
	}

	void UnequipAll() {
		for (int i = 0; i < currentEquipment.Length; i++) {
			Unequip (i);
		}
	}

    void AttachToObject(GameObject obj, int slotIndex, Transform parent)
    {
        // Check objects
        if (currentObjects[slotIndex] != null)
            Destroy(currentObjects[slotIndex].gameObject);

        // Instantiate new equip
        GameObject newObj = Instantiate(obj, parent) as GameObject;
        currentObjects[slotIndex] = newObj;

        // Log
        Debug.LogFormat("Equipped at position: {0}, Parent: {1}", newObj.transform.position, parent.gameObject.name);

    }
}
