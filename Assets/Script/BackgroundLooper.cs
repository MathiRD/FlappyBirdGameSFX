using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public SpriteRenderer bgA;
    public SpriteRenderer bgB;
    public Sprite normalSprite;
    public Sprite caveSprite;
    public float speed = 1.5f;

    Camera cam;
    float tileWidth;
    bool nextIsCave = false;

    void Start()
    {
        cam = Camera.main;
        tileWidth = bgA.bounds.size.x;

        bgB.transform.position = new Vector3(
            bgA.transform.position.x + tileWidth,
            bgB.transform.position.y,
            bgB.transform.position.z
        );

        SetZone(bgA, false);
        SetZone(bgB, true);
        nextIsCave = false;
    }

    void Update()
    {
        float dx = speed * Time.deltaTime;
        bgA.transform.Translate(Vector3.left * dx);
        bgB.transform.Translate(Vector3.left * dx);

        float leftEdge = cam.transform.position.x - cam.orthographicSize * cam.aspect;
        RecycleIfOff(bgA.transform, bgB.transform, leftEdge);
        RecycleIfOff(bgB.transform, bgA.transform, leftEdge);
    }

    void RecycleIfOff(Transform tile, Transform otherTile, float leftEdge)
    {
        float halfWidth = tileWidth / 2f;
        float rightEdgeOfTile = tile.position.x + halfWidth;

        if (rightEdgeOfTile < leftEdge)
        {
            tile.position = new Vector3(otherTile.position.x + tileWidth, tile.position.y, tile.position.z);


            var sr = tile.GetComponent<SpriteRenderer>();
            bool makeCave = nextIsCave;
            sr.sprite = makeCave ? caveSprite : normalSprite;
            SetZone(sr, makeCave);

            nextIsCave = !nextIsCave;
        }
    }

    void SetZone(SpriteRenderer sr, bool isCave)
    {
        var zone = sr.GetComponent<BackgroundZone>();
        if (!zone) zone = sr.gameObject.AddComponent<BackgroundZone>();
        zone.isCave = isCave;

        var col = sr.GetComponent<BoxCollider2D>();
        if (!col) col = sr.gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;

        var size = sr.bounds.size;
        col.size = new Vector2(size.x, size.y);
        col.offset = Vector2.zero;
    }
}
