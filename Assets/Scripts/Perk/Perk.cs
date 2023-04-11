using System;
using UnityEngine;

[Serializable]
public class Perk
{
    public string PerkName;
    [TextArea] public string PerkDescription;
    public Sprite Image;
    public Parameter Parameter;
}
