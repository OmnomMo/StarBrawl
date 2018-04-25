using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurnbasedCombat
{

    public class TurnbasedActor : MonoBehaviour
    {

        public bool myTurn;
        

        public int currentHealth;
        public int maxHealth;

        public Image healthBar;

        public AttackAction attack1;

        // Use this for initialization
        void Start()
        {

        }

        public void Attack(TurnbasedActor target)
        {

            ActionResult attackResult = attack1.Do();
            if (attackResult.actionEffect != ActionResult.Effect.miss)
            {
                target.TakeDamage(attackResult.damage);
            } else
            {
                //didnt hit sry
            }

        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            UpdateHealthBar();
        }

        public void UpdateHealthBar()
        {
            if (currentHealth > 0) {
                healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
            } else
            {
                healthBar.fillAmount = 0;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
