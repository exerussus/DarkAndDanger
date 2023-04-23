using System;
using UnityEngine;


[Serializable]
public class Item
{
    [SerializeField] private string itemName;
    [SerializeField] private float weight;
    [SerializeField] private int price;
    [SerializeField] private int level;
    [SerializeField] private Sprite image;
    [SerializeField] private Parameter parameter;
    
    public string ItemName => itemName;
    public float Weight => weight;
    public int Price => price;
    public int Level => level;
    public Sprite Image => image;
    public Parameter Parameter => parameter;
}
