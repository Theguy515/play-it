using UnityEngine;

public class FreeFallPipeRow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float destroyY = 10f;    // when it goes past top of screen

    private bool scoreGiven = false;
    private Transform player;


    private float halfWidth;


    private Transform leftPipe;
    private Transform rightPipe;

    private void Awake()
    {
        // Find child pipe pieces by name
        leftPipe = transform.Find("LeftPipe");
        rightPipe = transform.Find("RightPipe");

        if (leftPipe == null || rightPipe == null)
        {
            Debug.LogError("FreeFallPipeRow: LeftPipe or RightPipe child not found. Make sure your prefab has these children.");
        }

        // Compute the visible half-width of the camera so pipes always line up with screen edges
        Camera cam = Camera.main;
        if (cam != null)
        {
            float halfHeight = cam.orthographicSize;
            halfWidth = halfHeight * cam.aspect;
        }
        else
        {
            Debug.LogWarning("FreeFallPipeRow: No main camera found. Using fallback halfWidth = 5.");
            halfWidth = 5f;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

 
    public void Setup(float gapCenterX, float gapWidth)
    {
        if (leftPipe == null || rightPipe == null)
            return;

        float gapHalf = gapWidth * 0.5f;

        float leftEdge  = -halfWidth;
        float rightEdge =  halfWidth;

        float gapLeft  = gapCenterX - gapHalf;
        float gapRight = gapCenterX + gapHalf;

        // LEFT PIPE
        float leftPipeWidth = gapLeft - leftEdge;
        if (leftPipeWidth < 0f) leftPipeWidth = 0f;

        Vector3 leftScale = leftPipe.localScale;
        leftScale.x = leftPipeWidth;
        leftPipe.localScale = leftScale;

        Vector3 leftPos = leftPipe.localPosition;
        leftPos.x = leftEdge + leftPipeWidth * 0.5f;
        leftPipe.localPosition = leftPos;

        // RIGHT PIPE
        float rightPipeWidth = rightEdge - gapRight;
        if (rightPipeWidth < 0f) rightPipeWidth = 0f;

        Vector3 rightScale = rightPipe.localScale;
        rightScale.x = rightPipeWidth;
        rightPipe.localScale = rightScale;

        Vector3 rightPos = rightPipe.localPosition;
        rightPos.x = gapRight + rightPipeWidth * 0.5f;
        rightPipe.localPosition = rightPos;
    }

    private void Update()
    {
        
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        // Simple "passed row" check hook for scoring later
        if (!scoreGiven && player != null && transform.position.y > player.position.y)
        {
            scoreGiven = true;
            // TODO: Call your score system here, e.g.: 
            // ScoreManager.Instance.AddScore(pointsPerRow);
        }

        // Destroy when off screen
        if (transform.position.y > destroyY)
        {
            Destroy(gameObject);
        }
    }
}