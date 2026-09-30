using UnityEngine;
using static CustomUtilities;

public class HealthTracker : MonoBehaviour, IDamagable, IHasTeam
{
    [SerializeField] PlayerTeams playerTeam = PlayerTeams.Player;
    public PlayerTeams PlayerTeam
    {
        get => playerTeam;
        set => playerTeam = value;
    }

    [SerializeField] float maxHealth = 100;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Debug.Log("Im ded lol");
        }
    }
}
