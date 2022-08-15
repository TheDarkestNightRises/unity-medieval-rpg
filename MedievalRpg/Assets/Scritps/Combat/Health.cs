using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        float healthPoints = -1f;
        bool isDead = false;

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            if (healthPoints <= 0f)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            Debug.Log(instigator.name + " dealt damage: " + damage + " to " + gameObject.name);
            healthPoints = Mathf.Max(healthPoints - damage,0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void RegenerateHealth()
        {
            float regenhealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage/100);
            healthPoints = Mathf.Max(healthPoints , regenhealthPoints);
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Scheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                Die();
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }
    }
}

