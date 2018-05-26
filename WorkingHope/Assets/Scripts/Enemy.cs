using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private int killDamage = 10000;
    public int fallBoundary = -10;

    [System.Serializable]
    public class EnemyStats
    {
        public int Health = 100;
    }

    public EnemyStats stats = new EnemyStats();

    private void Update()
    {
        if (transform.position.y <= fallBoundary)
        {
            DamageEnemy(killDamage);
        }
    }

    public void DamageEnemy(int damage)
    {
        stats.Health -= damage;
        if (stats.Health <= 0)
        {
            GameMaster.KillEnemy(this);
        }
    }
}
