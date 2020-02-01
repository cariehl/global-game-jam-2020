using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    PlayerControls controls;


    public float speed = 50f;
    Vector3 move;

    private void Awake() {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector3.zero;
    }

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 m = move * Time.deltaTime * speed;

        transform.Translate(m, Space.World);
    }

    private void Test(InputAction.CallbackContext ctx) {
        Debug.Log("A");
    }
}
