using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    public delegate void RobotDelegate(Robot robot);
    public event RobotDelegate OnLevelCompleted;


    private void OnTriggerEnter2D(Collider2D collision) {
        Robot robot = collision.gameObject.GetComponent<Robot>();
        if (robot == null) return;
        
        OnLevelCompleted?.Invoke(robot);
    }
}
