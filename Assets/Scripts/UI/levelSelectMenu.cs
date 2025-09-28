using UnityEngine;
using UnityEngine.UIElements;

public class levelSelectMenuUI : MonoBehaviour {
    [SerializeField] private UIDocument selectMenu;
    [SerializeField] private UIDocument levelTemplate;
    [SerializeField] private GameObject gameManager;
    private void Awake()
    {
        SharedUI.setInvisible(selectMenu);
        for (int i = 0; i < gameManager.levels.Length; i++)
        {
            VisualElement clonedElement = templateTree.CloneTree();
            Button button = clonedElement.Q<Button>();
                        if (button != null)
            {
                button.text = gameManager.levels[i].levelName;
                button.clicked += () => switchLevel(gameManager.levels[i]);
                buttonContainer.Add(button);
            }
        }
        //selectMenu.rootVisualElement.Q<Button>("quit").clicked += SharedUI.doQuit;
    }

    private void switchLevel(LevelData level) {
        
    }
}