using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUI : MonoBehaviour {
    [SerializeField] private UIDocument pauseMenu;
    private void Awake()
    {
        SharedUI.setInvisible(pauseMenu);
        pauseMenu.rootVisualElement.Q<Button>("quit").clicked += SharedUI.doQuit;
    }
}