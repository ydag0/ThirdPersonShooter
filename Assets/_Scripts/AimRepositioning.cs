using UnityEngine;
public class AimRepositioning : MonoBehaviour
{
    Vector3 pos;
    float yBoundry = 15.0f;
    float distance = 5.0f;
    [SerializeField] float trackSpeed = 10;

    void Update()
    {
        pos= ShootManager.Instance.CalculateTargetPoint(distance);
        pos.y = Mathf.Clamp(pos.y, -yBoundry, yBoundry);
        transform.position = Vector3.Lerp(transform.position,pos,Time.deltaTime * trackSpeed);
    }
}

