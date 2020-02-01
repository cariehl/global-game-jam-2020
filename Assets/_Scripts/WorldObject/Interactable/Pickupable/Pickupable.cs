using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickupable : Interactable {

    private Transform parent;

    [SerializeField] private WorldObject owner;

    private void Awake() {
        parent = transform.parent;
    }

    public void GetPickedUp(WorldObject newOwner) {
        owner = newOwner;
        OnPickedUp();
        Debug.Log(this.ToString() + " picked up by " + newOwner.ToString(), this);
    }

    protected virtual void OnPickedUp() {
        transform.parent = owner.transform;
        transform.localPosition = new Vector3(-0.2f, -0.2f, 2);
    }

    public void GetThrown() {
        WorldObject oldOwner = owner;
        owner = null;
        OnThrown();
        Debug.Log(this.ToString() + " picked up by " + oldOwner.ToString(), this);
    }

    protected virtual void OnThrown() {
        transform.localPosition = new Vector3(-1.5f, 0, 0);
        transform.parent = parent;
    }
}
