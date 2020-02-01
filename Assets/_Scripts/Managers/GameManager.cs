using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public RobotFactory robotFactory;

    [SerializeField]
    private PlayerManager playerManagerPrefab;

    private PlayerManager playerManager;

    [SerializeField]
    private LevelManager curentLevel;

    [SerializeField]
    private List<LevelManager> levels = new List<LevelManager>();

    private void Awake() {
        playerManager = Instantiate<PlayerManager>(playerManagerPrefab);

        StartGame();
    }

    private void StartGame() {
        LevelManager level = GetNextLevel();

        if (level == null) {
            Debug.Log("No more levels");
            return;
        }

        StartLevel(level);
    }

    private LevelManager GetNextLevel() {
        if (curentLevel == null) {
            return levels[0];
        }

        int nextIndex = levels.IndexOf(curentLevel) + 1;
        if (nextIndex >= levels.Count) {
            return null;
        }

        return levels[nextIndex];
    }

    private void StartLevel(LevelManager level) {
        if (level == null) return;

        curentLevel = level;
        curentLevel.OnLevelCompleted += LevelCompleted;
        curentLevel.StartLevel(playerManager, robotFactory);
    }

    private void LevelCompleted(LevelManager level) {
        curentLevel.OnLevelCompleted -= LevelCompleted;
        
    }
}
