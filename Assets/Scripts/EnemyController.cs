using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Stats
    [SerializeField]
    [Tooltip("Enemy Health")]
    private int health;

    [SerializeField]
    [Tooltip("How much damage the player will take")]
    private int damage;
    #endregion

    #region Health Methods
    public void DecreaseHealth(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
