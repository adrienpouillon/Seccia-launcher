using System.Collections.Generic;
using UnityEngine;
public class SceneCursor : SceneEntity
{
public static implicit operator bool(SceneCursor inst) { return inst!=null; }
public SceneCursor()
{
m_entity = ENTITY.CURSOR;
}
public override void __605()
{
}
}
