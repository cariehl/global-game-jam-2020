using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour {

    public delegate void LevelDelegate(LevelManager level);
    public event LevelDelegate OnLevelCompleted;

    public string levelName;

    [SerializeField] private GameObject spawner;
    public Vector3 SpawnPoint => spawner.transform.position;

    [SerializeField]
    private Finish finish;

    private bool levelActive = false;

    protected PlayerManager playerManager;

    private void OnEnable() {
        finish.OnLevelCompleted += LevelCompleted;
    }

    private void OnDisable() {
        finish.OnLevelCompleted -= LevelCompleted;
    }

    private void Awake() {
        spawner = GameObject.FindGameObjectWithTag("Spawn");
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
    }

    public void StartLevel(PlayerManager playerManager, RobotFactory factory) {
        this.playerManager = playerManager;
        levelActive = true;

        SpawnRobots(factory);
        playerManager.StartLevel();

        Debug.Log("Started level " + ToString());
    }

    public abstract void SpawnRobots(RobotFactory factory);

    private void LevelCompleted(Robot robot) {
        Debug.Log("Robot " + robot.ToString() + " completed level " + ToString());

        playerManager.FinishLevel();

        levelActive = false;
        OnLevelCompleted?.Invoke(this);
    }

    public override string ToString() {
        return levelName;
    }
}
