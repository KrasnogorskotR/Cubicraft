using UnityEngine;
using System.Collections;

public enum BrickLayerCondition
{
    AboveHeight,
    BelowHeight,
    AboveMountainValue,
    BelowMountainValue,
    AboveBlobValue,
    BelowBlobValue,

    AboveSquaredMountainValue,
    BelowSquaredMountainValue,
    AboveSquaredBlobValue,
    BelowSquaredBlobValue,

    AboveSqrtMountainValue,
    BelowSqrtMountainValue,
    AboveSqrtBlobValue,
    BelowSqrtBlobValue
}

[System.Serializable]
public class BrickLayerAttribute
{
    public BrickLayerCondition condition;
    public float threshold = 0;
}

[System.Serializable]
public class BrickLayer
{
    public string name = "Unnamed Bricklayer";
    public BrickType brick;
    public float weight = 1;
    public BrickLayerAttribute[] attributes;

    public virtual float Bid(int y, float mountainValue, float blobValue)
    {

        float bid = 0;
        int attribsMatched = 0;

        for (int a = 0; a < attributes.Length; a++)
        {
            float mybid = 0;

            switch (attributes[a].condition)
            {
                case BrickLayerCondition.AboveHeight:
                    mybid = Mathf.Min(2, 10 / ((y - attributes[a].threshold) + 2));
                    break;
                case BrickLayerCondition.BelowHeight:
                    mybid = Mathf.Min(2, (attributes[a].threshold - y) / 5);
                    break;
                case BrickLayerCondition.AboveMountainValue:
                    mybid = Mathf.Min(2, (mountainValue - attributes[a].threshold) * 2);
                    break;
                case BrickLayerCondition.BelowMountainValue:
                    mybid = Mathf.Min(2, (attributes[a].threshold - mountainValue) * 2);
                    break;
                case BrickLayerCondition.AboveBlobValue:
                    mybid = Mathf.Min(2, (blobValue - attributes[a].threshold) * 2);
                    break;
                case BrickLayerCondition.BelowBlobValue:
                    mybid = Mathf.Min(2, (attributes[a].threshold - blobValue) * 2);
                    break;


                case BrickLayerCondition.AboveSquaredMountainValue:
                    mybid = Mathf.Min(2, (Mathf.Pow(mountainValue, 2) - attributes[a].threshold) * 2);
                    break;
                case BrickLayerCondition.BelowSquaredMountainValue:
                    mybid = Mathf.Min(2, (attributes[a].threshold - Mathf.Pow(mountainValue, 2)) * 2);
                    break;
                case BrickLayerCondition.AboveSquaredBlobValue:
                    mybid = Mathf.Min(2, (Mathf.Pow(blobValue, 2) - attributes[a].threshold) * 2);
                    break;
                case BrickLayerCondition.BelowSquaredBlobValue:
                    mybid = Mathf.Min(2, (attributes[a].threshold - Mathf.Pow(blobValue, 2)) * 2);
                    break;

                case BrickLayerCondition.AboveSqrtMountainValue:
                    mybid = Mathf.Min(2, (Mathf.Sqrt(mountainValue) - attributes[a].threshold) * 2);
                    break;
                case BrickLayerCondition.BelowSqrtMountainValue:
                    mybid = Mathf.Min(2, (attributes[a].threshold - Mathf.Sqrt(mountainValue)) * 2);
                    break;
                case BrickLayerCondition.AboveSqrtBlobValue:
                    mybid = Mathf.Min(2, (Mathf.Sqrt(blobValue) - attributes[a].threshold) * 2);
                    break;
                case BrickLayerCondition.BelowSqrtBlobValue:
                    mybid = Mathf.Min(2, (attributes[a].threshold - Mathf.Sqrt(blobValue)) * 2);
                    break;


            }

            if (mybid > 0)
            {
                bid += mybid;
                attribsMatched++;
            }

        }
        if (attribsMatched < attributes.Length) return 0;

        if (weight == 0) return bid;
        return bid * weight;

    }
}