using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Pickupable {

    public delegate void RobotDestroyedDelegate(Robot robot);
    public event RobotDestroyedDelegate OnRobotDestroyed;

    [SerializeField]
    private float speed = 50f;
    [SerializeField]
    private float pickupRange = 2f;

    [SerializeField]
    private Pickupable heldObject;
    
    // items you can receive that "float" around you
    [SerializeField] private List<Item> items = new List<Item>();

    private Vector3 move;

    // Update is called once per frame
    void Update() {
        Vector3 m = move * Time.deltaTime * speed;

        transform.Translate(m, Space.World);
    }

    public void Move(Vector2 move) {
        this.move = move;
    }

    /// <summary>
    /// Recall this robot to the location of the mainRobot
    /// </summary>
    /// <param name="mainRobot"></param>
    public void Recall(Robot mainRobot) {
        if (mainRobot != null) {
            GiveItems(mainRobot);
        }

        RecallSelf();
    }

    /// <summary>
    /// Invokes events, plays animations, cleans up and destroys self
    /// </summary>
    private void RecallSelf() {
        OnRobotDestroyed?.Invoke(this);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Gives items to other Robot
    /// </summary>
    /// <param name="other"></param>
    private void GiveItems(Robot other) {
        foreach(Item item in items) {
            item.GiveTo(other);
        }
    }

    /// <summary>
    /// Receives an item
    /// </summary>
    /// <param name="item"></param>
    public void ReceiveItem(Item item) {
        items.Add(item);
    }

    /// <summary>
    /// Uses an "object"
    ///   Idea: Pick up another robot or throw an already held robot
    /// </summary>
    public void Use() {
        if (heldObject != null) {
            ThrowObject();
            return;
        }

        // pickup new object        
        Pickupable foundObject = FindPickupableObject();
        Pickup(foundObject);
    }

    /// <summary>
    /// Find the closest pickupableObject that is within its pickup range
    /// </summary>
    /// <returns>Pickupable object or null if none found</returns>
    private Pickupable FindPickupableObject() {
        Collider[] collisions = Physics.OverlapSphere(this.transform.position, pickupRange);
        foreach (Collider col in collisions) {
            Pickupable pickup = col.gameObject.GetComponent<Pickupable>();
            if (pickup != null && pickup != this) {
                return pickup;
            }
        }

        return null;
    }

    /// <summary>
    /// Attempt to pickup an item in an area around the Robot if one is found
    /// </summary>
    private void Pickup(Pickupable other) {
        if (other == null) return;

        other.GetPickedUp(this);
        heldObject = other;
    }

    protected override void OnPickedUp() {
        base.OnPickedUp();
    }

    /// <summary>
    ///   Throw the currently held object
    /// </summary>
    private void ThrowObject() {
        Pickupable other = heldObject;
        heldObject = null;
        other.GetThrown();
    }
}
