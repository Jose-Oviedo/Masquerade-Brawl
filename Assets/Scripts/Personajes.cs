using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Personaje")]
public class Personajes : ScriptableObject
{
    public RuntimeAnimatorController controladorAnimacion;
    public Sprite imagen;
    public string nombre;
    public int maxHealth;
    public GameObject defaultWeapon;
}
