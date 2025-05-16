using UnityEngine;

public class BaseRotator : MonoBehaviour
{
    public GunSet gunSet;
    private BaseProfile profile;

    [SerializeField] private float tankRotateCooldown = 1f;
    [SerializeField] private float tankRotateTimer = 0f;

    [SerializeField] private float gunRotateCooldown = 1f;
    [SerializeField] private float gunRotateTimer = 0f;

    public Vector3 gunDir;
    public Vector3 tankDir;

    public float gunRotateSpeed;
    public float tankRotateSpeed;

    void Awake()
    {
        gunSet = GetComponentInChildren<GunSet>();
        profile = GetComponent<BaseCharacter>().profile;
    }

    void Update()
    {
        Rotate();
        RotateGun();

        gunRotateTimer -= Time.deltaTime;
        tankRotateTimer -= Time.deltaTime;
    }

    public void Rotate(in Vector2 dir)
    {
        if (tankRotateTimer <= 0f)
        {
            tankDir = dir;
            tankRotateSpeed = profile.rotationSpeed;

            tankRotateTimer = profile.tankRotateCooldown;
        }
    }

    public void RotateGun(in Vector2 dir)
    {
        if (gunRotateTimer <= 0f)
        {
            gunDir = dir;
            gunRotateSpeed = profile.gunRotationSpeed;
                
            gunRotateTimer = profile.gunRotateCooldown;
        }
    }

    public void RotateInstantly(in Vector2 dir)
    {
        tankDir = dir;
        tankRotateSpeed = 100000;

        tankRotateTimer = profile.tankRotateCooldown;
    }

    public void RotateGunInstantly(in Vector2 dir)
    {
        gunDir = dir;
        gunRotateSpeed = 100000;

        gunRotateTimer = profile.gunRotateCooldown;
    }

    private void RotateGun()
    {
        float speed = gunRotateSpeed;
        Vector3 dir = gunDir;

        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;

        Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);
        gunSet.transform.rotation = Quaternion.RotateTowards(gunSet.transform.rotation, to, speed * Time.deltaTime);
    }

    private void Rotate()
    {
        float speed = tankRotateSpeed;
        Vector3 dir = tankDir;

        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;

        Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);

        Quaternion tempControllerRotation = gunSet.transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, to, speed * Time.deltaTime);

        gunSet.transform.rotation = tempControllerRotation;
    }

    /*
        public void Rotate(in Vector2 dir)
        {
            if (rotateTimer <= 0f)
            {
                float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;

                Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, to, profile.rotationSpeed * Time.deltaTime);

                rotateTimer = rotateCooldown;
            }
        }

        public void RotateInstantly(in Vector2 dir)
        {
            if (rotateTimer <= 0f)
            {
                float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;

                Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward); ;
                transform.rotation = to;
            }
        }

        public void RotateGun(in Vector3 target, in float rotationSpeed)
        {
            if (rotateTimer <= 0f)
            {
                Vector3 dir = (target - controller.transform.position).normalized;
                float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;

                Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);

                //controller.transform.rotation = to;
                controller.transform.rotation = Quaternion.RotateTowards(transform.rotation, to, profile.gunRotationSpeed * Time.deltaTime);
            }
        }*/
}
