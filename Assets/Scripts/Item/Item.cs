using System;
using UnityEngine;


[Serializable]
public class Item
{
    public enum HoldingSpaceType
    {
        OneXOne = 0,
        TwoXOne = 1,
        OneXTwo = 2,
        OneXThree = 3,
        OneXFour = 4,
        TwoXTwo = 5,
        TwoXThree = 6,
        TwoXFour = 7,
        ThreeXTwo = 8,
        ThreeXThree = 9,
        
    };

    public int sellCost;
    public HoldingSpaceType HoldingSpace;
    public Sprite Image;
    public Parameter Parameter;
    
    
    
}
