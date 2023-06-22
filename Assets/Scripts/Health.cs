using UnityEngine;

public class Health : MonoBehaviour
{
    public void ApplyDamage()
    {
        Destroy(gameObject);
    }
}