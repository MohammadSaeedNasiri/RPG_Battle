using UnityEngine;

//This code is for adjusting the game background size based on the screen size.
[RequireComponent(typeof(SpriteRenderer))]//Background
public class FitBackgroundToScreen : MonoBehaviour
{
    void Start()
    {
        Camera cam = Camera.main;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * cam.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;

        transform.localScale = new Vector3(
            worldWidth / spriteSize.x,
            worldHeight / spriteSize.y,
            1f
        );

        //Keep it center
        transform.position = new Vector3(
            cam.transform.position.x,
            cam.transform.position.y,
            0f
        );
    }
}
