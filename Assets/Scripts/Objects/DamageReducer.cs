using UnityEngine;

public class DamageReducer : MonoBehaviour
{
    [SerializeField] private int _reduceCoeff;

    public int Reduce(int amount)
    {
        return amount / _reduceCoeff;
    }
}