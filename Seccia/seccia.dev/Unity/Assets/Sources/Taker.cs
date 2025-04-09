using UnityEngine;
using System;
public class Taker
{
public Obj m_obj = null;
public float m_time = 0.0f;
public float m_x = 0.0f;
public float m_y = 0.0f;
public static implicit operator bool(Taker inst) { return inst!=null; }
}
