using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public int health;
    public int attack;

    public enum Direction { left = -1, right = 1};
    public Direction direction;

    public GameObject arrow;
}
