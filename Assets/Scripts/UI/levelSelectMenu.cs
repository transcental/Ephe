using UnityEngine;
using UnityEngine.UIElements;

public class levelSelectMenuUI : MonoBehaviour {
    [SerializeField] private UIDocument selectMenu;
    [SerializeField] private VisualTreeAsset levelTemplate;
    [SerializeField] private GameObject gameManager;
    private void Awake()
    {
        SharedUI.setInvisible(selectMenu);
        for (int i = 0; i < gameManager.GetComponent<GameManager>().levels.Count; i++)
        {
            VisualElement clonedElement = levelTemplate.CloneTree();
            ListView container = selectMenu.rootVisualElement.Q<ListView>("LevelSelectList");
            Button button = clonedElement.Q<Button>();
                if (button != null)
            {
                button.text = gameManager.GetComponent<GameManager>().levels[i].levelName;
                button.clicked += () => switchLevel(i);
                container.Add(button);
            }

        }
        //selectMenu.rootVisualElement.Q<Button>("quit").clicked += SharedUI.doQuit;
    }

    private void switchLevel(int level) {
        Debug.Log("Summoning the Game Manager to switch Scene");
        gameManager.GetComponent<GameManager>().StartLevel(level);
    }
}