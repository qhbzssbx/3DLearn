using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Character")]
public class CharacterConfig : ScriptableObject
{
    [Header("Movement")]
    public float WalkSpeed = 3f;
    public float CombatMoveSpeed = 2f;

    [Header("Combat")] 
    public int BaseAttack = 10;
    public float AttackRange = 2f;
}