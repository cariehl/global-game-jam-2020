using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Robot : MonoBehaviour {
    //PlayerControls controls;

    public float speed = 50f;
    Vector3 move;

    private void Awake() {
        //controls = new PlayerControls();

        //controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Move.canceled += ctx => move = Vector3.zero;
    }

    //private void OnEnable() {
    //    controls.Gameplay.Enable();
    //}

    // Update is called once per frame
    void Update() {
        Vector3 m = move * Time.deltaTime * speed;

        transform.Translate(m, Space.World);
    }

    public void Move(Vector2 move) {
        this.move = move;
    }

    private void Test(InputAction.CallbackContext ctx) {
        Debug.Log("A");
    }
}
