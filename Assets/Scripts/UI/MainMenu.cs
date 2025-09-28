using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private UIDocument mainMenu;
    [SerializeField] private UIDocument selectMenu;

    private void Awake()
    {
        SharedUI.setVisible(mainMenu);
        mainMenu.rootVisualElement.Q<Button>("play").clicked += ChangeToSelectLevel;
        mainMenu.rootVisualElement.Q<Button>("quit").clicked += SharedUI.doQuit;
    }

    private void ChangeToSelectLevel() {
        SharedUI.setInvisible(mainMenu);
        SharedUI.setVisible(selectMenu);
    }
}