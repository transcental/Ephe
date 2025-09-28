using UnityEngine;

public class BeatMover : MonoBehaviour
{
    public float speed = 5f;
    public string direction;
    private float songEndTime;

    public void Init(string dir, float songLength)
    {
        direction = dir;
        songEndTime = Time.time + songLength;
    }

    void Update()
    {
        if (Time.time > songEndTime)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 moveDir = direction == "left" ? Vector3.left : Vector3.right;
        transform.position += moveDir * (speed * Time.deltaTime);

        // Destroy if off screen
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0f || viewportPos.x > 1f)
        {
            Destroy(gameObject);
        }
    }
}