

public class BossGunman : Boss
{
    protected BossShooting shooting;

    protected bool inShootingRange;

    protected override void Awake()
    {
        base.Awake();

        shooting = GetComponent<BossShooting>();
    }

    protected override void Update() {}

    protected override void FixedUpdate() {}
}

