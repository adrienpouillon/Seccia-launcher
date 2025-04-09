using UnityEngine;
public class SceneShot
{
public Scene m_scene;
public int m_sid;
public string m_name;
public float m_x;
public float m_y;
public float m_width;
public float m_scale;
public static implicit operator bool(SceneShot inst) { return inst!=null; }
public void Reset()
{
}
}
