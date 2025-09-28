using UnityEngine;
using UnityEngine.UIElements;

public class SharedUI
{
    // Shared QUIT function
    public static void doQuit() {
        Debug.Log("Application Quitting");
        Application.Quit();
    }

    public static void setInvisible(UIDocument document)
    {
        document.rootVisualElement.style.display = DisplayStyle.None;
    }
        public static void setVisible(UIDocument document)
    {
        document.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}
