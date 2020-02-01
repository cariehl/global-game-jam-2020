using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelManager {

    public override void SpawnRobots(RobotFactory factory) {
        Robot robot = factory.SpawnRobot(SpawnPoint);
        playerManager.AddRobot(robot);
    }
}
