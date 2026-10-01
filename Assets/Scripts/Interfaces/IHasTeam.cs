using UnityEngine;

public enum PlayerTeams
{
    Player,
    Enemy
}
public interface IHasTeam
{
    PlayerTeams PlayerTeam { get; set; }
}
