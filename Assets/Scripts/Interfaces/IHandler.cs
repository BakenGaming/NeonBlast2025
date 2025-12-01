using UnityEngine;

public interface IHandler
{
    public abstract void Initialize();
    public abstract HealthSystem GetHealthSystem();
    public abstract void UpdateHealth();
    public abstract void HandleDeath();
}
