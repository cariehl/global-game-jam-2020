using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private PlayerControls controls;

    [SerializeField]
    private List<Robot> robots = new List<Robot>();

    public Robot mainRobot;

    private void OnEnable() {
        controls.Gameplay.Enable();
    }

    private void OnDisable() {
        controls.Gameplay.Disable();
    }

    private void Awake() {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        controls.Gameplay.Move.canceled += ctx => Movement(Vector2.zero);

        controls.Gameplay.Swap.performed += ctx => Swap();
        controls.Gameplay.Recall.performed += ctx => Recall();
        controls.Gameplay.Use.performed += ctx => Use();
    }

    private void Start() {
        AddRobots();

        if (robots.Count > 0) {
            mainRobot = robots[0];
        }

        Movement(Vector2.zero);
    }

    private void AddRobots() {
        robots.Clear();
        robots.AddRange(GetComponentsInChildren<Robot>());
    }

    public void AddRobot(Robot robot) {
        robots.Add(robot);
    }

    private void Movement(Vector2 move) {
        if (mainRobot == null) return;

        mainRobot.Move(move);
    }

    private void Swap() {
        if (robots.Count <= 1) return;

        int nextIndex = robots.IndexOf(mainRobot) + 1;

        if (nextIndex >= robots.Count) {
            nextIndex = 0;
        }

        mainRobot = robots[nextIndex];
    }

    /// <summary>
    /// Recalls all other Robot(s) to the main Robot
    /// </summary>
    private void Recall() {
        if (mainRobot == null) return;

        foreach (Robot robot in robots) {
            if (robot == mainRobot) continue;

            robot.Recall(mainRobot);
        }
    }

    /// <summary>
    /// Uses an "object"
    ///   Idea: Pick up another robot or throw an already held robot
    /// </summary>
    private void Use() {
        if (mainRobot == null) return;

        mainRobot.Use();
    }
}
