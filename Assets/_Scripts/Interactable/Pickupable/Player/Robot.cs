using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Robot : Pickupable {

    [SerializeField]
    private float speed = 50f;

    private Vector3 move;

    

    // items you can receive that "float" around you
    private List<Item> items = new List<Item>();

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

    }

    /// <summary>
    /// Uses an "object"
    ///   Idea: Pick up another robot or throw an already held robot
    /// </summary>
    public void Use() {

    }
}
