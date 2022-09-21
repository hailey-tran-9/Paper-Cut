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

    #region Combat_functions
    public int GetDmg()
    {
        return damage;
    }
    #endregion

    #region Health Methods
    IEnumerator LoseHpHelper(int amount)
    {
        health -= amount;

        // Briefly tint the enemy red to show it getting hit
        Color initialColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 129, 129, 255);
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<SpriteRenderer>().color = initialColor;
        yield return new WaitForSeconds(0.1f);

        if(health <= 0)
        {
            // Destroy the enemy
            Destroy(gameObject);
        }
    }

    public void LoseHp(int amount)
    {
        StartCoroutine(LoseHpHelper(amount));
    }
    #endregion
}
