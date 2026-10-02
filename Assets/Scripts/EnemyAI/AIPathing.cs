using UnityEngine;

public class AIPathing : MonoBehaviour
{
    [SerializeField] Transform targetTransform;

    private enum CurrentState
    {
        idle,
        directChase,
        aStarChase,
    }
}
