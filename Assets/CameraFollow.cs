using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //we need two floats, our follow speed and our y offset
    public float FollowSpeed = 2f;
    public float yOffset = 1f;

    //then we need our variable to target(the player in this case
    public Transform target;

    //update will be called once per frame

    void Update()
    {
        //we need a vector 3 for our new position(3 variables of (x,y,z))
        Vector3 newPosition = new Vector3(target.position.x, target.position.y + yOffset, -10f);

        //transform line to actually move the player(slerp is like moving a sphere in place
        //... to make the camera smooth) so this line moves the camera alongside the players position based on follow speed
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}
