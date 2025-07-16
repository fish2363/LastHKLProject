using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    private Dictionary<Type, Ability> _ability=new();
}
