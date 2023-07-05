using UnityEngine;

public class DamageReducer : MonoBehaviour
{
    [SerializeField][Range(1f, 10f)] private float _reduceCoeff;

    public float Reduce(float amount)
    {
        return amount / _reduceCoeff;
    }
}