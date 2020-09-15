using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout;
    [SerializeField] Ability _ability;

    private void Awake()
    {
        if (_ability != null)
        {
            _abilityLoadout?.EquipAbility(_ability);
        }
    }

    private void Update()
    {
        // check for left click
        if (Input.GetMouseButtonDown(0))
        {
            _abilityLoadout.UseEquippedAbility();
        }
    }
}
