using UnityEngine;

public class BaseRotator : MonoBehaviour
{
    public void Rotate(in Vector2 dir, in float rotationSpeed)
    {
        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;

        Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, to, rotationSpeed * Time.deltaTime);
    }

    public void RotateInstantly(in Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;

        Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward); ;
        transform.rotation = to;
    }
}
