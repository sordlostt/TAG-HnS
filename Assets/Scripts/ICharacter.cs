using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public void SetDamage(float damage);

    public bool IsAttacking();

    public void OnAttackBegin();

    public void OnAttackEnd();
}
