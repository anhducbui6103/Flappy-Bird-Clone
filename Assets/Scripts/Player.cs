using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private Vector3 direction;
    public float gravity = -20f;
    public float strength = 10f;

    public float tiltSmooth = 2f;
    public float maxTiltAngle = 45f;

    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        float angle = Mathf.Lerp(0, -maxTiltAngle, -direction.y / strength);
        if (direction.y > 0)
        {
            angle = Mathf.Lerp(0, maxTiltAngle, direction.y / strength);
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
            FindObjectOfType<GameManager>().GameOver();
        else if (other.gameObject.tag == "Scoring")
            FindObjectOfType<GameManager>().IncreaseScore();

    }
}
