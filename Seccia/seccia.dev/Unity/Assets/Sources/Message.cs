using UnityEngine;
public class Message
{
public const int NONE				= 0;
public const int SELECT				= 1;
public const int USE				= 2;
public const int USELABEL			= 3;
public const int DETACH				= 4;
public const int LABEL				= 5;
public const int DOOR				= 6;
public const int WALK				= 7;
public const int S_NONE = 0;
public const int S_MOVE = 1;
public const int S_ANIM = 2;
public int m_type;
public bool m_byUser;
public Player m_player;
public SceneObj m_sceneObj;
public SubObj m_subObj;
public SceneLabel m_label;
public SceneDoor m_door;
public Obj m_objA;
public Obj m_objB;
public bool m_bothFromInventory;
public float m_dist;
public Anim m_anim;
public int m_dir;
public bool m_enter;
public string m_enterCell;
public RoleBox m_roleBox;
public int m_roleBoxToken;
public int m_state;
public static implicit operator bool(Message inst) { return inst!=null; }
public Message()
{
Reset();
}
public void Reset()
{
m_type = NONE;
m_byUser = true;
m_player = null;
m_sceneObj = null;
m_subObj = null;
m_objA = null;
m_objB = null;
m_bothFromInventory = false;
m_label = null;
m_door = null;
m_dist = 0.0f;
m_anim = null;
m_dir = -1;
m_enter = false;
m_enterCell = "";
m_roleBox = null;
m_roleBoxToken = 0;
m_state = 0;
}
public bool __38()
{
return m_type!=NONE;
}
}
