using RPG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{
    Fighter fighter;
    private void Awake()
    {
        fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
    }

    private void Update()
    {
        if (fighter.GetTarget() == null)
        {
            GetComponent<TextMeshProUGUI>().text = "N/A";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}/{1:0}", fighter.GetTarget().GetHealthPoints(), fighter.GetTarget().GetMaxHealthPoints());
        }
    }
}
