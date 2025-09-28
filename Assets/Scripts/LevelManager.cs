using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetupLevel()
    {
        Debug.Log("LevelManager: Setting up level...");
        var height = Camera.main.orthographicSize * 2;
        var width = height * Camera.main.aspect;
        Debug.Log($"Camera dimensions - Width: {width}, Height: {height}");
        // Additional setup code here
        
        // top bar will be a canvas 5% of the height of the screen
        // bottom bar will be a canvas 10% of the height of the screen
        var topBarTop = height / 2;
        var topBarBottom = topBarTop - (height * 0.05f);
        var bottomBarBottom = -height / 2;
        var bottomBarTop = bottomBarBottom + (height * 0.1f);
        Debug.Log($"Top Bar - Top: {topBarTop}, Bottom: {topBarBottom}");
        Debug.Log($"Bottom Bar - Top: {bottomBarTop}, Bottom: {bottomBarBottom}");
        
        var playAreaTop = topBarBottom;
        var playAreaBottom = bottomBarTop;
        
    }

}
