
abstract public class SC_ShooterTower : SC_BaseShootingTower
{
    abstract protected void TransmissionTargetInfoToShooter();
    protected override void CalTargetPos()
    {
        base.CalTargetPos();
        TransmissionTargetInfoToShooter();
    }
}
