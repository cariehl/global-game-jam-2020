using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFactory : MonoBehaviour {

    public Robot robotPrefab;
    
    public Robot SpawnRobot(Vector3 spawnPoint) {

        Robot robot = Instantiate<Robot>(robotPrefab);
        robot.transform.position = spawnPoint;

        return robot;
    }
}
