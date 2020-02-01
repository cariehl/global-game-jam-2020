using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {

    private Robot owner;
    
    public void GiveTo(Robot robot) {
        owner = robot;
        owner.ReceiveItem(this);

        // play animation to move item to new owner
    }
}
