using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node2D
{
    public int GridX, GridY;
    public Node2D parent;
    public int gCost, hCost;
    public bool collidable;
    public Vector3 truePosFromWorld;

    public Node2D(bool _collidable, Vector3 _truePos, int _gridX, int _gridY){
        collidable = _collidable;
        truePosFromWorld = _truePos;
        GridX = _gridX;
        GridY = _gridY;
    }

    public int FCost{
        get{
            return gCost + hCost;
        }
    }

    public void tilemapCollidable(bool coll){
        collidable = coll;
    }
}
