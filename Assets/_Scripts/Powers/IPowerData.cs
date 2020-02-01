namespace _Scripts.Powers
{
    public interface IPowerData
    {
        float Cooldown(PowerType type);
        float EarthCooldown { get; }
        float WaterCooldown { get; }
        float WindCooldown { get; }
    }
}