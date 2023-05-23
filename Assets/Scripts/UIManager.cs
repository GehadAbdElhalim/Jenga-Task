using TMPro;
using UnityEngine;

namespace JengaTask
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject gameplayPanel;
        [SerializeField] GameObject detailsPanel;
        [SerializeField] TMP_Text detailsText;

        public void OnRightArrowClicked()
        {
            GameManager.Instance.LookAtPreviousTower();
        }

        public void OnLeftArrowClicked()
        {
            GameManager.Instance.LookAtNextTower();
        }

        public void TestMyStack()
        {
            GameManager.Instance.TestStackForCurrentTower();
        }

        public void RestartScene()
        {
            GameManager.Instance.RestartScene();
        }

        public void ShowDetailsPanel(Block block) 
        {
            detailsPanel.SetActive(true);
            gameplayPanel.SetActive(false);
            detailsText.text = "• " + block.Grade + " : " + block.Domain + "\n"
                + "• " + block.Cluster + "\n"
                + "• " + block.StandardId + " : " + block.StandardDescription;
        }

        public void CloseDetailsPanels()
        {
            detailsPanel.SetActive(false);
            gameplayPanel.SetActive(true);
        }

        internal bool IsShowingBlockDetails()
        {
            return detailsPanel.activeSelf;
        }
    }
}