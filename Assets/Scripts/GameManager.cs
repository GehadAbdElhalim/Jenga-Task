using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JengaTask
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance 
        {
            get
            {
                if (instance == null) instance = FindObjectOfType<GameManager>();
                return instance;
            }
        }

        [SerializeField] BlocksDeserializer blocksDeserializer;
        [SerializeField] JengaTowersBuilder jengaTowersBuilder;
        [SerializeField] CameraController cameraController;
        [SerializeField] UIManager uiManager;

        private List<JengaTower> jengaTowers = new List<JengaTower>();
        private int currentTowerIndex = -1;
        private bool showingBlockDetails = false;

        private void Start()
        {
            blocksDeserializer.GetBlocksFromAPI(blocks =>
            {
                Dictionary<string,Stack> labeledStacks = StacksBuilder.CreateLabeledStacks(blocks);
                jengaTowers = jengaTowersBuilder.BuildJengaTowers(labeledStacks);
                LookAtNextTower();
            });
        }

        public void LookAtNextTower()
        {
            if (jengaTowers == null || jengaTowers.Count == 0) return;
            currentTowerIndex = (currentTowerIndex + 1) % jengaTowers.Count;
            cameraController.SetTarget(jengaTowers[currentTowerIndex].GetTowerCenterPoint());
        }

        public void LookAtPreviousTower()
        {
            if (jengaTowers == null || jengaTowers.Count == 0) return;
            if (currentTowerIndex <= 0) currentTowerIndex = jengaTowers.Count;
            currentTowerIndex = (currentTowerIndex - 1) % jengaTowers.Count;
            cameraController.SetTarget(jengaTowers[currentTowerIndex].GetTowerCenterPoint());
        }

        public void TestStackForCurrentTower()
        {
            jengaTowers[currentTowerIndex].TestStack();
        }

        public void RestartScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        public void ShowBlockDetails(Block block)
        {
            uiManager.ShowDetailsPanel(block);
        }

        public bool IsShowingBlockDetails()
        {
            return uiManager.IsShowingBlockDetails();
        }
    }
}