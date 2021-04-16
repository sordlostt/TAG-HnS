using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // set of characters hit during the current attack
    HashSet<ICharacter> hitCharacters = new HashSet<ICharacter>();
    ICharacter owner;

    [SerializeField]
    float damage;

    private void Awake()
    {
        owner = gameObject.GetComponentInParent<ICharacter>();
    }

    private void Update()
    {
        if (!owner.IsAttacking() && hitCharacters.Count > 0)
        {
            hitCharacters.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (owner.IsAttacking())
        {
            ICharacter character = other.gameObject.GetComponentInParent<ICharacter>();
            if (character != null && hitCharacters.Add(character) && character != owner)
            {
                character.SetDamage(damage);
            }
        }
    }
}
