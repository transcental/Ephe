using UnityEngine;

public class BeatMover : MonoBehaviour
{
    public float speed = 5f;
    public string direction;
    private float _songEndTime;

    public void Init(string dir, float songLength)
    {
        direction = dir;
        _songEndTime = Time.time + songLength;
    }

    private void Update()
    {
        if (Time.time > _songEndTime)
        {
            Destroy(gameObject);
            return;
        }

        var moveDir = direction == "left" ? Vector3.left : Vector3.right;
        transform.position += moveDir * (speed * Time.deltaTime);
        Debug.Log(direction);

        
        var viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x is < 0f or > 1f)
        {
        //    Destroy(gameObject);
        }
    }
}