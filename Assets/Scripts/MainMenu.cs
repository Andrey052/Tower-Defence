using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        private void Start()
        {
            continueButton.interactable = FileHandler.HasFile(MapCompletion.fileName);
        }
        public void NewGame()
        {
            FileHandler.Reset(MapCompletion.fileName);
            FileHandler.Reset(Upgrades.fileName);
            SceneManager.LoadScene(1);
        }
        public void Continue()
        {
            SceneManager.LoadScene(1);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}