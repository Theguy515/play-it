using UnityEngine;

public class MazeExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<MazePlayer>() != null)
        {
            MazeGameController.Instance.OnExitReached();
        }
    }
}