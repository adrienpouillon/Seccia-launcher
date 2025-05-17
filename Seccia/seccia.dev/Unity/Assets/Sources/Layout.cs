using UnityEngine;
using System.Collections.Generic;
public class Layout
{
public Sprite m_spriteAutoSave;
public Sprite m_spriteBag;
public Sprite m_spriteDetach;
public Sprite m_spriteMagnify;
public Sprite m_spriteMenu;
public Sprite m_spriteObject;
public Sprite m_spriteObjectPrev;
public Sprite m_spriteObjectNext;
public Sprite m_spritePlayer;
public Sprite m_spritePlayerPrev;
public Sprite m_spritePlayerNext;
public Sprite m_spriteShutup;
public Sprite m_spriteUser1;
public Sprite m_spriteUser2;
public Sprite m_spriteUser3;
public Sprite m_spriteUser4;
public Sprite m_spriteUser5;
public Sprite m_spriteUser6;
public Sprite m_spriteUser7;
public Sprite m_spriteUser8;
public LayoutCtrl[] m_ctrls = new LayoutCtrl[(int)LAYOUT_CTRL.COUNT];
public float m_takerX;
public float m_takerY;
public bool m_bagLocked;
public bool m_bagOpened;
public bool m_bagForceHidden;
public Player[] m_players = new Player[0];
public Player[] m_visiblePlayers = new Player[0];
public int m_playerScroll;
public static implicit operator bool(Layout inst) { return inst!=null; }
public Layout()
{
}
public void __64(Asset asset)
{
for ( int i=0 ; i<m_ctrls.Length ; i++ )
{
m_ctrls[i] = new LayoutCtrl(this, (LAYOUT_CTRL)i);
m_ctrls[i].__64(asset);
}
}
public void __416(Asset asset)
{
G.m_game.__237(asset, m_spriteAutoSave);
G.m_game.__237(asset, m_spriteBag);
G.m_game.__237(asset, m_spriteDetach);
G.m_game.__237(asset, m_spriteMagnify);
G.m_game.__237(asset, m_spriteMenu);
G.m_game.__237(asset, m_spriteObject);
G.m_game.__237(asset, m_spriteObjectPrev);
G.m_game.__237(asset, m_spriteObjectNext);
G.m_game.__237(asset, m_spritePlayer);
G.m_game.__237(asset, m_spritePlayerPrev);
G.m_game.__237(asset, m_spritePlayerNext);
G.m_game.__237(asset, m_spriteShutup);
G.m_game.__237(asset, m_spriteUser1);
G.m_game.__237(asset, m_spriteUser2);
G.m_game.__237(asset, m_spriteUser3);
G.m_game.__237(asset, m_spriteUser4);
G.m_game.__237(asset, m_spriteUser5);
G.m_game.__237(asset, m_spriteUser6);
G.m_game.__237(asset, m_spriteUser7);
G.m_game.__237(asset, m_spriteUser8);
}
public void Reset()
{
m_bagLocked = false;
m_bagOpened = false;
m_bagForceHidden = false;
m_playerScroll = 0;
}
public void __219()
{
for ( int i=0 ; i<m_ctrls.Length ; i++ )
m_ctrls[i].__219();
m_takerX = Get(LAYOUT_CTRL.BAG).m_rcView.__439();
m_takerY = Get(LAYOUT_CTRL.BAG).m_rcView.__440();
}
public void __417(string[] players)
{
for ( int i=0 ; i<m_players.Length ; i++ )
{
Sprite icon = m_players[i].__453();
if ( icon )
icon.End();
}
List<Player> newPlayers = new List<Player>();
for ( int i=0 ; i<players.Length ; i++ )
{
Player newPlayer = G.m_game.__279(players[i]);
if ( newPlayer )
newPlayers.Add(newPlayer);
}
m_players = new Player[newPlayers.Count];
for ( int i=0 ; i<m_players.Length ; i++ )
m_players[i] = newPlayers[i];
m_visiblePlayers = null;
m_playerScroll = 0;
Asset asset = G.__95(G.m_pathGraphics);
if ( asset )
{
for ( int i=0 ; i<m_players.Length ; i++ )
{
Sprite icon = m_players[i].__453();
if ( icon )
icon.__468(asset);
}
asset.Close();
}
__418();
}
public void __418()
{
Player player = G.m_game.__293();
int visibleCount = m_players.Length;
for ( int i=0 ; i<m_players.Length ; i++ )
{
if ( m_players[i]==player )
visibleCount--;
}
m_visiblePlayers = new Player[visibleCount];
for ( int i=0, j=0 ; i<m_players.Length ; i++ )
{
if ( m_players[i]!=player )
m_visiblePlayers[j++] = m_players[i];
}
__219();
}
public void __419()
{
if ( m_playerScroll<0 )
{
m_playerScroll = 0;
return;
}
int itemCount = Get(LAYOUT_CTRL.PLAYERS).m_itemCount;
int emptyCount = itemCount - m_visiblePlayers.Length;
if ( emptyCount>=0 )
{
m_playerScroll = 0;
return;
}
if ( m_playerScroll>-emptyCount )
{
m_playerScroll = m_visiblePlayers.Length - itemCount;
return;
}
}
public bool __420()
{
int itemCount = Get(LAYOUT_CTRL.PLAYERS).m_itemCount;
int emptyCount = itemCount - m_visiblePlayers.Length;
if ( emptyCount>=0 || m_playerScroll>=-emptyCount )
return true;
return false;
}
public LayoutCtrl Get(LAYOUT_CTRL type)
{
return m_ctrls[(int)type];
}
public Material __421()
{
if ( m_spriteObjectPrev )
return m_spriteObjectPrev.m_material;
if ( m_spritePlayerPrev )
return m_spritePlayerPrev.m_material;
return null;
}
public Material __422()
{
if ( m_spriteObjectNext )
return m_spriteObjectNext.m_material;
if ( m_spritePlayerNext )
return m_spritePlayerNext.m_material;
return null;
}
public Material __423()
{
if ( m_spritePlayerPrev )
return m_spritePlayerPrev.m_material;
if ( m_spriteObjectPrev )
return m_spriteObjectPrev.m_material;
return null;
}
public Material __424()
{
if ( m_spritePlayerNext )
return m_spritePlayerNext.m_material;
if ( m_spriteObjectNext )
return m_spriteObjectNext.m_material;
return null;
}
public bool __425(LAYOUT_CTRL type, float xView, float yView)
{
return m_ctrls[(int)type].__425(xView, yView);
}
public LAYOUT_CTRL __425(float xView, float yView)
{
for ( int i=m_ctrls.Length-1 ; i>=0 ; i-- )
{
if ( m_ctrls[i].__425(xView, yView) )
return (LAYOUT_CTRL)i;
}
return LAYOUT_CTRL.COUNT;
}
public Player __426(float xView, float yView)
{
LayoutCtrl ctrl = Get(LAYOUT_CTRL.PLAYERS);
int index = ctrl.__431(xView, yView);
if ( index==-1 )
return null;
index += m_playerScroll;
if ( index<0 || index>=m_visiblePlayers.Length )
return null;
return m_visiblePlayers[index];
}
public void __43(bool isPlayable)
{
for ( int i=m_ctrls.Length-1 ; i>=0 ; i-- )
m_ctrls[i].__43(isPlayable);
}
}
public class LayoutCtrl
{
public Layout m_layout;
public LAYOUT_CTRL m_id;
public bool m_active;
public Rect m_rc;
public Rect m_rcView;
public LAYOUT_ALIGN m_horz;
public LAYOUT_ALIGN m_vert;
public int m_align;
public LAYOUT_MODE m_mode;
public bool m_rightClick;
public LAYOUT_LINK m_bag;
public LAYOUT_LINK m_talk;
public bool m_input;
public LAYOUT_ALIGN m_dynamic;
public float m_grid;
public float m_invZoom;
public LAYOUT_CELL m_cell;
public bool m_hasArrows;
public int m_itemCount;
public int m_iFirstItem;
public int m_cellCount;
public Rect[] m_rcCells;
public LayoutCtrl(Layout layout, LAYOUT_CTRL id)
{
m_layout = layout;
m_id = id;
}
public void __64(Asset asset)
{
m_active = asset.__10();
if ( m_active )
{
m_rc = new Rect(asset.__13(), asset.__13(), asset.__13(), asset.__13());
m_rcView = Rect.Zero;
m_horz = (LAYOUT_ALIGN)asset.__12();
m_vert = (LAYOUT_ALIGN)asset.__12();
m_align = asset.__12();
m_mode = (LAYOUT_MODE)asset.__12();
m_rightClick = asset.__10();
m_bag = (LAYOUT_LINK)asset.__12();
m_talk = (LAYOUT_LINK)asset.__12();
m_input = asset.__10();
m_dynamic = (LAYOUT_ALIGN)asset.__12();
m_grid = asset.__14();
m_invZoom = 1.0f / (asset.__14()/100.0f);
m_cell = LAYOUT_CELL.NONE;
switch ( m_id )
{
case LAYOUT_CTRL.BAG:
case LAYOUT_CTRL.DETACH:
case LAYOUT_CTRL.ITEMS:
case LAYOUT_CTRL.MENU:
case LAYOUT_CTRL.PLAYERS:
m_talk = LAYOUT_LINK.INVERTED;
break;
case LAYOUT_CTRL.SHUTUP:
m_talk = LAYOUT_LINK.ENABLED;
break;
}
switch ( m_id )
{
case LAYOUT_CTRL.ITEMS:
if ( m_grid==0.0f )
m_cell = m_rc.height>m_rc.width ? LAYOUT_CELL.VERT : LAYOUT_CELL.HORZ;
else
m_cell = LAYOUT_CELL.GRID;
break;
case LAYOUT_CTRL.PLAYERS:
m_cell = m_rc.height>m_rc.width ? LAYOUT_CELL.VERT : LAYOUT_CELL.HORZ;
break;
}
switch ( m_id )
{
case LAYOUT_CTRL.BAG:
case LAYOUT_CTRL.DETACH:
case LAYOUT_CTRL.ITEMS:
case LAYOUT_CTRL.MENU:
case LAYOUT_CTRL.PLAYERS:
case LAYOUT_CTRL.SHUTUP:
m_input = true;
break;
}
}
else
{
m_rc = Rect.Zero;
m_rcView = Rect.Zero;
m_horz = LAYOUT_ALIGN.NEAR;
m_vert = LAYOUT_ALIGN.NEAR;
m_align = 0;
m_mode = LAYOUT_MODE.NONE;
m_rightClick = false;
m_bag = LAYOUT_LINK.DISABLED;
m_talk = LAYOUT_LINK.DISABLED;
m_input = false;
m_dynamic = LAYOUT_ALIGN.SIZE;
m_grid = 0.0f;
m_invZoom = 1.0f;
m_cell = LAYOUT_CELL.NONE;
}
m_hasArrows = false;
m_itemCount = 0;
m_iFirstItem = 0;
m_cellCount = 0;
m_rcCells = null;
}
public void __219()
{
if ( m_rc.width==0.0f || m_rc.height==0.0f )
return;
m_rcView.x = G.m_rcViewUI.x + m_rc.x;
m_rcView.width = m_rc.width;
switch ( m_horz )
{
case LAYOUT_ALIGN.CENTER:
m_rcView.x += (G.m_rcViewUI.width-G.m_rcGame.width)*0.5f;
break;
case LAYOUT_ALIGN.FAR:
m_rcView.x += G.m_rcViewUI.width - G.m_rcGame.width;
break;
case LAYOUT_ALIGN.SIZE:
m_rcView.width += G.m_rcViewUI.width - G.m_rcGame.width;
break;
}
m_rcView.y = G.m_rcViewUI.y + m_rc.y;
m_rcView.height = m_rc.height;
switch ( m_vert )
{
case LAYOUT_ALIGN.CENTER:
m_rcView.y += (G.m_rcViewUI.height-G.m_rcGame.height)*0.5f;
break;
case LAYOUT_ALIGN.FAR:
m_rcView.y += G.m_rcViewUI.height - G.m_rcGame.height;
break;
case LAYOUT_ALIGN.SIZE:
m_rcView.height += G.m_rcViewUI.height - G.m_rcGame.height;
break;
}
int gridCols = 0;
int gridRows = 0;
switch ( m_id )
{
case LAYOUT_CTRL.ITEMS:
{
Player player = G.m_game.__293();
int playerItemCount = player==null ? 0 : player.m_items.Count;
if ( m_cell==LAYOUT_CELL.GRID )
{
m_hasArrows = false;
m_iFirstItem = 0;
switch ( m_dynamic )
{
case LAYOUT_ALIGN.NEAR:
case LAYOUT_ALIGN.FAR:
{
gridCols = (int)(m_rcView.width/m_grid);
gridRows = (int)(m_rcView.height/m_grid);
m_cellCount = playerItemCount;
m_itemCount = playerItemCount;
break;
}
case LAYOUT_ALIGN.CENTER:
{
m_cellCount = playerItemCount;
m_itemCount = playerItemCount;
if ( m_cellCount==1 )
{
gridCols = 1;
gridRows = 1;
}
else if ( m_cellCount>1 )
{
gridCols = (int)(m_rcView.width/m_grid);
gridRows = (int)(m_rcView.height/m_grid);
float ratio = m_rcView.height==0.0f ? 1.0f : m_rcView.width/m_rcView.height;
int sq = (int)Mathf.Sqrt(m_cellCount);
int col, row;
if ( ratio>=1.0f )
{
col = (int)(sq*ratio);
row = sq;
}
else
{
col = sq;
row = (int)(sq/ratio);
}
bool found = false;
for ( int y=row ; y<=gridRows && found==false ; y++ )
{
for ( int x=col ; x<=gridCols ; x++ )
{
if ( x*y>=m_itemCount )
{
gridCols = x;
gridRows = y;
found = true;
break;
}
}
}
}
break;
}
case LAYOUT_ALIGN.SIZE:
{
gridCols = (int)(m_rcView.width/m_grid);
gridRows = (int)(m_rcView.height/m_grid);
m_cellCount = gridCols * gridRows;
m_itemCount = m_cellCount;
break;
}
}
}
else
{
if ( m_cell==LAYOUT_CELL.VERT )
{
m_cellCount = (int)(m_rcView.height/m_rcView.width);
if ( ((int)m_rcView.width)-((int)m_rcView.height)%((int)m_rcView.width)==1 )
m_cellCount++;
}
else
{
m_cellCount = (int)(m_rcView.width/m_rcView.height);
if ( ((int)m_rcView.height)-((int)m_rcView.width)%((int)m_rcView.height)==1 )
m_cellCount++;
}
m_hasArrows = m_cellCount>2 && playerItemCount>m_cellCount;
m_iFirstItem = m_hasArrows ? 1 : 0;
m_itemCount = m_hasArrows ? m_cellCount-2 : m_cellCount;
if ( m_itemCount<0 )
m_itemCount = 0;
if ( m_dynamic!=LAYOUT_ALIGN.SIZE )
{
if ( playerItemCount<m_itemCount )
{
m_cellCount = playerItemCount;
m_itemCount = playerItemCount;
}
}
}
break;
}
case LAYOUT_CTRL.PLAYERS:
{
if ( m_layout.m_visiblePlayers.Length==0 )
{
m_cellCount = 0;
m_itemCount = 0;
m_hasArrows = false;
m_iFirstItem = 0;
}
else
{
if ( m_cell==LAYOUT_CELL.VERT )
{
m_cellCount = (int)(m_rcView.height/m_rcView.width);
if ( ((int)m_rcView.width)-((int)m_rcView.height)%((int)m_rcView.width)==1 )
m_cellCount++;
}
else
{
m_cellCount = (int)(m_rcView.width/m_rcView.height);
if ( ((int)m_rcView.height)-((int)m_rcView.width)%((int)m_rcView.height)==1 )
m_cellCount++;
}
m_hasArrows = m_cellCount>2 && m_layout.m_visiblePlayers.Length>m_cellCount;
m_iFirstItem = m_hasArrows ? 1 : 0;
m_itemCount = m_hasArrows ? m_cellCount-2 : m_cellCount;
if ( m_itemCount<0 )
m_itemCount = 0;
}
break;
}
}
if ( m_cellCount==0 )
m_rcCells = null;
else
{
m_rcCells = new Rect[m_cellCount];
switch ( m_cell )
{
case LAYOUT_CELL.HORZ:
{
float offset = m_rcView.x + (m_rcView.width-m_cellCount*m_rcView.height)*0.5f;
if ( m_dynamic==LAYOUT_ALIGN.NEAR )
offset = m_rcView.x;
else if ( m_dynamic==LAYOUT_ALIGN.FAR )
offset = m_rcView.__436() - m_cellCount*m_rcView.height;
for ( int i=0 ; i<m_cellCount ; i++ )
{
m_rcCells[i] = new Rect(offset, m_rcView.y, m_rcView.height, m_rcView.height);
offset += m_rcView.height;
}
break;
}
case LAYOUT_CELL.VERT:
{
float offset = m_rcView.y + (m_rcView.height-m_cellCount*m_rcView.width)*0.5f;
if ( m_dynamic==LAYOUT_ALIGN.NEAR )
offset = m_rcView.y;
else if ( m_dynamic==LAYOUT_ALIGN.FAR )
offset = m_rcView.__437() - m_cellCount*m_rcView.width;
for ( int i=0 ; i<m_cellCount ; i++ )
{
m_rcCells[i] = new Rect(m_rcView.x, offset, m_rcView.width, m_rcView.width);
offset += m_rcView.width;
}
break;
}
case LAYOUT_CELL.GRID:
{
switch ( m_dynamic )
{
case LAYOUT_ALIGN.NEAR:
case LAYOUT_ALIGN.SIZE:
{
float offsetX = m_rcView.x;
float offsetY = m_rcView.y;
int pos = 0;
for ( int y=0 ; y<gridRows ; y++ )
{
for ( int x=0 ; x<gridCols && pos<m_cellCount ; x++ )
{
m_rcCells[pos++] = new Rect(offsetX, offsetY, m_grid, m_grid);
offsetX += m_grid;
}
offsetY += m_grid;
offsetX = m_rcView.x;
}
break;
}
case LAYOUT_ALIGN.CENTER:
{
float offsetY = m_rcView.__440() - gridRows*m_grid*0.5f;
int lastRowSize = m_cellCount % gridCols;
if ( lastRowSize==0 )
lastRowSize = gridCols;
int pos = 0;
for ( int y=0 ; y<gridRows ; y++ )
{
int cols = y==gridRows-1 ? lastRowSize : gridCols;
float offsetX = m_rcView.__439() - cols*m_grid*0.5f;
for ( int x=0 ; x<gridCols && pos<m_cellCount ; x++ )
{
m_rcCells[pos++] = new Rect(offsetX, offsetY, m_grid, m_grid);
offsetX += m_grid;
}
offsetY += m_grid;
}
break;
}
case LAYOUT_ALIGN.FAR:
{
float offsetX = m_rcView.x;
float offsetY = m_rcView.__437() - m_grid;
int pos = 0;
for ( int y=0 ; y<gridRows ; y++ )
{
for ( int x=0 ; x<gridCols && pos<m_cellCount ; x++ )
{
m_rcCells[pos++] = new Rect(offsetX, offsetY, m_grid, m_grid);
offsetX += m_grid;
}
offsetY -= m_grid;
offsetX = m_rcView.x;
}
break;
}
}
break;
}
}
}
}
public bool __427()
{
if ( m_active==false )
return false;
switch ( m_talk )
{
case LAYOUT_LINK.ENABLED:
{
if ( G.m_game.m_menuDialog.__38()==false || G.m_game.m_menuDialog.m_isWaitingUser==false || G.m_game.m_menuDialog.m_locked )
return false;
break;
}
case LAYOUT_LINK.INVERTED:
{
if ( G.m_game.m_menuDialog.__38() )
return false;
break;
}
}
switch ( m_bag )
{
case LAYOUT_LINK.ENABLED:
{
if ( m_layout.m_bagLocked )
return false;
if ( m_layout.m_bagOpened==false )
return false;
break;
}
case LAYOUT_LINK.INVERTED:
{
if ( m_layout.m_bagOpened )
return false;
break;
}
}
return true;
}
public bool __425(float xView, float yView)
{
if ( __427()==false )
return false;
if ( m_input==false )
return false;
if ( m_id==LAYOUT_CTRL.ITEMS || m_id==LAYOUT_CTRL.PLAYERS )
{
for ( int i=0 ; i<m_cellCount ; i++ )
{
if ( m_rcCells[i].Contains(xView, yView) )
return true;
}
}
else
{
if ( m_rcView.Contains(xView, yView) )
return true;
}
return false;
}
public bool __428(float xView, float yView)
{
if ( m_hasArrows==false )
return false;
if ( __425(xView, yView)==false )
return false;
if ( m_rcCells[0].Contains(xView, yView) )
return true;
if ( m_rcCells[m_rcCells.Length-1].Contains(xView, yView) )
return true;
return false;
}
public bool __429(float xView, float yView)
{
if ( m_hasArrows==false )
return false;
if ( __425(xView, yView)==false )
return false;
return m_rcCells[0].Contains(xView, yView);
}
public bool __430(float xView, float yView)
{
if ( m_hasArrows==false )
return false;
if ( __425(xView, yView)==false )
return false;
return m_rcCells[m_rcCells.Length-1].Contains(xView, yView);
}
public int __431(float xView, float yView)
{
if ( __427()==false )
return -1;
for ( int i=0 ; i<m_itemCount ; i++ )
{
int iCell = m_iFirstItem + i;
if ( m_rcCells[iCell].Contains(xView, yView) )
return i;
}
return -1;
}
public void __43(bool isPlayable)
{
if ( __427()==false )
return;
switch ( m_id )
{
case LAYOUT_CTRL.BAG:
{
if ( m_layout.m_bagForceHidden==false )
{
if ( m_mode==LAYOUT_MODE.ALWAYS )
G.m_graphics.__354(m_layout.m_spriteBag.m_material, ref m_rcView);
else
{
if ( m_layout.m_bagOpened )
G.m_graphics.__354(m_layout.m_spriteBag.m_material, ref m_rcView);
else if ( isPlayable && m_layout.m_bagLocked==false && G.m_game.m_dragObj==null && G.m_game.m_input.m_isDown && (G.m_game.m_time-G.m_game.m_input.m_isDownTime)>0.5f )
G.m_graphics.__354(m_layout.m_spriteBag.m_material, ref m_rcView);
}
}
break;
}
case LAYOUT_CTRL.DETACH:
{
G.m_graphics.__354(m_layout.m_spriteDetach.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.ITEMS:
{
if ( m_hasArrows )
{
Player player = G.m_game.__293();
for ( int iCell=0 ; iCell<m_cellCount ; iCell++ )
{
if ( iCell==0 )
{
float opacity = player && player.m_scroll==0 ? 0.25f : 1.0f;
G.m_graphics.__354(m_layout.__421(), ref m_rcCells[iCell], opacity);
}
else if ( iCell==m_cellCount-1 )
{
float opacity = player.__486() ? 0.25f : 1.0f;
G.m_graphics.__354(m_layout.__422(), ref m_rcCells[iCell], opacity);
}
else
{
if ( m_layout.m_spriteObject )
G.m_graphics.__354(m_layout.m_spriteObject.m_material, ref m_rcCells[iCell]);
}
}
}
else
{
if ( m_layout.m_spriteObject )
{
for ( int iCell=0 ; iCell<m_cellCount ; iCell++ )
G.m_graphics.__354(m_layout.m_spriteObject.m_material, ref m_rcCells[iCell]);
}
}
break;
}
case LAYOUT_CTRL.MENU:
{
G.m_graphics.__354(m_layout.m_spriteMenu.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.PLAYERS:
{
if ( m_hasArrows )
{
for ( int iCell=0 ; iCell<m_cellCount ; iCell++ )
{
if ( iCell==0 )
{
float opacity = m_layout.m_playerScroll==0 ? 0.25f : 1.0f;
G.m_graphics.__354(m_layout.__423(), ref m_rcCells[iCell], opacity);
}
else if ( iCell==m_cellCount-1 )
{
float opacity = m_layout.__420() ? 0.25f : 1.0f;
G.m_graphics.__354(m_layout.__424(), ref m_rcCells[iCell], opacity);
}
else
G.m_graphics.__354(m_layout.m_spritePlayer.m_material, ref m_rcCells[iCell]);
}
}
else
{
for ( int iCell=0 ; iCell<m_cellCount ; iCell++ )
G.m_graphics.__354(m_layout.m_spritePlayer.m_material, ref m_rcCells[iCell]);
}
break;
}
case LAYOUT_CTRL.SHUTUP:
{
G.m_graphics.__354(m_layout.m_spriteShutup.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.USER1:
{
G.m_graphics.__354(m_layout.m_spriteUser1.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.USER2:
{
G.m_graphics.__354(m_layout.m_spriteUser2.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.USER3:
{
G.m_graphics.__354(m_layout.m_spriteUser3.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.USER4:
{
G.m_graphics.__354(m_layout.m_spriteUser4.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.USER5:
{
G.m_graphics.__354(m_layout.m_spriteUser5.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.USER6:
{
G.m_graphics.__354(m_layout.m_spriteUser6.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.USER7:
{
G.m_graphics.__354(m_layout.m_spriteUser7.m_material, ref m_rcView);
break;
}
case LAYOUT_CTRL.USER8:
{
G.m_graphics.__354(m_layout.m_spriteUser8.m_material, ref m_rcView);
break;
}
}
}
}
