using UnityEngine;

public class DamageReducer : MonoBehaviour
{
    [SerializeField][Range(1, 10)] private int _reduceCoeff;

    public int Reduce(int amount)
    {
        return amount / _reduceCoeff;
    }
}