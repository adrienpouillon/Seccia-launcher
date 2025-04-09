using UnityEngine;
using System.Collections.Generic;
using System;
public class SceneWall
{
public Scene m_scene;
public int m_sid;
public float m_ax;
public float m_ay;
public float m_bx;
public float m_by;
public static implicit operator bool(SceneWall inst) { return inst!=null; }
public void Reset()
{
}
}
public struct SceneWallIntersect
{
public float x;
public float y;
public float length;
public float angle;
public bool first;
}
public class SceneWallCompare : IComparer<float>
{
public int Compare(float a, float b)
{
float da = a - b;
da = Mathf.Atan2(Mathf.Sin(da), Mathf.Cos(da));
if ( da==0.0f )
return 0;
return da<0.0f ? -1 : 1;
}
}
