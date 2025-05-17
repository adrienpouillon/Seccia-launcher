using UnityEngine;
using System.Collections.Generic;
using System;
public class Router
{
public class Bridge
{
public List<uint> m_cells = new List<uint>();
public static implicit operator bool(Bridge inst) { return inst!=null; }
}
public const int NOT_STARTED = 0;
public const int FOUND = 1;
public const int FAILED = 2;
public const bool WALKABLE = true;
public const bool UNWALKABLE = false;
public bool[] m_mapWalkable;
public Bridge[] m_mapBridge;
int m_mapColMin = 0;
int m_mapColMax = 0;
int m_mapRowMin = 0;
int m_mapRowMax = 0;
int m_mapWidth = 0;
int m_mapHeight = 0;
int m_mapSize = 0;
int m_onClosedList = 10;
int[] m_openList;
int[] m_whichList;
int[] m_openX;
int[] m_openY;
int[] m_parentX;
int[] m_parentY;
int[] m_Fcost;
int[] m_Gcost;
int[] m_Hcost;
public List<int> m_pathBank = null;
public int m_pathStatus;
public int m_pathLength;
public int m_pathLocation;
public float m_xFinalTrg;
public float m_yFinalTrg;
public float m_xTrg;
public float m_yTrg;
public int m_startCol;
public int m_startRow;
public bool m_hasBridgeAtStart;
public static implicit operator bool(Router inst) { return inst!=null; }
public void Init(int colMin, int colMax, int rowMin, int rowMax)
{
m_mapColMin = colMin;
m_mapColMax = colMax;
m_mapRowMin = rowMin;
m_mapRowMax = rowMax;
m_mapWidth = colMax - colMin + 1;
m_mapHeight = rowMax - rowMin + 1;
m_mapSize = m_mapWidth * m_mapHeight;
m_mapWalkable = new bool[m_mapSize];
m_mapBridge = new Bridge[m_mapSize];
for ( int i=0 ; i<m_mapSize ; i++ )
m_mapBridge[i] = null;
}
public void __468()
{
m_openList = new int[m_mapWidth*m_mapHeight+2];
m_whichList = new int[(m_mapWidth+1)*(m_mapHeight+1)];
m_openX = new int[m_mapWidth*m_mapHeight+2];
m_openY = new int[m_mapWidth*m_mapHeight+2];
m_parentX = new int[(m_mapWidth+1)*(m_mapHeight+1)];
m_parentY = new int[(m_mapWidth+1)*(m_mapHeight+1)];
m_Fcost = new int[m_mapWidth*m_mapHeight+2];
m_Gcost = new int[(m_mapWidth+1)*(m_mapHeight+1)];
m_Hcost = new int[m_mapWidth*m_mapHeight+2];
m_pathBank = new List<int>();
m_pathLocation = 0;
m_pathLength = 0;
m_pathStatus = FAILED;
}
public void End()
{
m_openList = null;
m_whichList = null;
m_openX = null;
m_openY = null;
m_parentX = null;
m_parentY = null;
m_Fcost = null;
m_Gcost = null;
m_Hcost = null;
m_pathBank = null;
}
public int __391(int col, int row)
{
return (row-m_mapRowMin)*m_mapWidth + col - m_mapColMin;
}
public int __391(float x, float y)
{
return (G.__145(y)-m_mapRowMin)*m_mapWidth + G.__145(x) - m_mapColMin;
}
public int __511(int col, int row)
{
return (row-m_mapRowMin)*(m_mapWidth+1) + col - m_mapColMin;
}
public bool __512(int index)
{
return index>=0 && index<m_mapSize;
}
public bool __513(float x, float y)
{
int index = __391(x, y);
if ( index<0 || index>=m_mapWalkable.Length )
return false;
return m_mapWalkable[index]==WALKABLE;
}
public void __514()
{
m_pathLocation = 0;
m_pathLength = 0;
m_pathStatus = FAILED;
m_pathBank.Clear();
}
public int __515(float x, float y, float targetX, float targetY)
{
int lastPathStatus = m_pathStatus;
int lastPathLocation = m_pathLocation;
m_pathLocation = 0;
m_pathLength = 0;
m_pathStatus = FAILED;
m_hasBridgeAtStart = false;
int onOpenList = 0;
int m = 0;
int u = 0;
int v = 0;
int temp = 0;
int numberOfOpenListItems = 0;
int addedGCost = 0;
int tempGcost = 0;
int path = 0;
int newOpenListItemID = 0;
int tempCol, pathCol, pathRow, cellPosition;
int[] xChoices = new int[9+32];
int[] yChoices = new int[9+32];
int choiceCount = 0;
int startCol = G.__145(x);
int startRow = G.__145(y);
bool isBridge = false;
if ( __513(x, y)==false )
{
isBridge = true;
int iPath = lastPathLocation - 1;
if ( lastPathStatus==FOUND && iPath>=0 )
{
startCol = m_pathBank[iPath*2];
startRow = m_pathBank[iPath*2+1];
}
else
{
}
}
int startIndexEx = __511(startCol, startRow);
if ( startIndexEx<0 || startIndexEx>=m_Gcost.Length )
goto noPath;
int targetCol = G.__145(targetX);
int targetRow = G.__145(targetY);
int targetIndex = __391(targetCol, targetRow);
int targetIndexEx = __511(targetCol, targetRow);
if ( isBridge==false && startCol==targetCol && startRow==targetRow )
{
m_pathStatus = FOUND;
return m_pathStatus;
}
/*if ( startCol==targetCol && startRow==targetRow && isBridge )
{
startCol = m_xTrg1K/G.PATH_GRID_CELLSIZE1K;
startRow = m_yTrg1K/G.PATH_GRID_CELLSIZE1K;
if ( startCol==targetCol && startRow==targetRow && lastPathStatus==FOUND )
{
startCol = m_startCol;
startRow = m_startRow;
}
}*/
if ( m_mapWalkable[targetIndex]==UNWALKABLE )
goto noPath;
m_pathBank.Clear();
if ( m_onClosedList>1000000 )
{
for ( int i=0 ; i<m_whichList.Length ; i++ )
m_whichList[i] = 0;
m_onClosedList = 10;
}
m_onClosedList = m_onClosedList+2;
onOpenList = m_onClosedList-1;
m_pathLength = NOT_STARTED;
m_pathLocation = NOT_STARTED;
m_Gcost[startIndexEx] = 0;
numberOfOpenListItems = 1;
m_openList[1] = 1;
m_openX[1] = startCol;
m_openY[1] = startRow;
while ( true )
{
if ( numberOfOpenListItems!=0 )
{
int parentX = m_openX[m_openList[1]];
int parentY = m_openY[m_openList[1]];
int parentIndex = __391(parentX, parentY);
int parentIndexEx = __511(parentX, parentY);
m_whichList[parentIndexEx] = m_onClosedList;
numberOfOpenListItems = numberOfOpenListItems - 1;
m_openList[1] = m_openList[numberOfOpenListItems+1];
v = 1;
while ( true )
{
u = v;
if ( 2*u+1<=numberOfOpenListItems )
{
if ( m_Fcost[m_openList[u]]>=m_Fcost[m_openList[2*u]] )
v = 2*u;
if ( m_Fcost[m_openList[v]]>=m_Fcost[m_openList[2*u+1]] )
v = 2*u+1;
}
else
{
if ( 2*u<=numberOfOpenListItems )
{
if ( m_Fcost[m_openList[u]]>=m_Fcost[m_openList[2*u]] )
v = 2*u;
}
}
if ( u!=v )
{
temp = m_openList[u];
m_openList[u] = m_openList[v];
m_openList[v] = temp;
}
else
break;
}
choiceCount = 0;
for ( int b=parentY-1 ; b<=parentY+1 ; b++ )
{
for ( int a=parentX-1 ; a<=parentX+1 ; a++ )
{
xChoices[choiceCount] = a;
yChoices[choiceCount] = b;
choiceCount++;
}
}
Bridge bridge = null;
if ( parentIndex>=0 && parentIndex<m_mapBridge.Length )
bridge = m_mapBridge[parentIndex];
if ( bridge )
{
for ( int i=0 ; i<bridge.m_cells.Count ; i++ )
{
xChoices[choiceCount] = G.__99(bridge.m_cells[i]);
yChoices[choiceCount] = G.__100(bridge.m_cells[i]);
choiceCount++;
}
}
for ( int iChoice=0 ; iChoice<choiceCount ; iChoice++ )
{
int a = xChoices[iChoice];
int b = yChoices[iChoice];
int index = __391(a, b);
int indexEx = __511(a, b);
if ( a==m_mapColMin-1 || b==m_mapRowMin-1 || a==m_mapColMax+1 || b==m_mapRowMax+1 )
continue;
if ( indexEx<0 || indexEx>=m_whichList.Length )
goto noPath;
if ( m_whichList[indexEx]==m_onClosedList )
continue;
if ( index<0 || index>=m_mapWalkable.Length )
goto noPath;
if ( m_mapWalkable[index]==UNWALKABLE )
continue;
if ( m_whichList[indexEx]!=onOpenList )
{
newOpenListItemID = newOpenListItemID + 1;
m = numberOfOpenListItems+1;
m_openList[m] = newOpenListItemID;
m_openX[newOpenListItemID] = a;
m_openY[newOpenListItemID] = b;
if ( iChoice>8 )
addedGCost = (int)(G.__139(parentX, parentY, a, b)*10.0f);
else if ( Mathf.Abs(a-parentX)==1 && Mathf.Abs(b-parentY)==1 )
addedGCost = 28;
else
addedGCost = 10;
m_Gcost[indexEx] = m_Gcost[parentIndexEx] + addedGCost;
m_Hcost[m_openList[m]] = 20*(Mathf.Abs(a-targetCol) + Mathf.Abs(b-targetRow));
m_Fcost[m_openList[m]] = m_Gcost[indexEx] + m_Hcost[m_openList[m]];
m_parentX[indexEx] = parentX;
m_parentY[indexEx] = parentY;
while ( m!=1 )
{
if ( m_Fcost[m_openList[m]]<=m_Fcost[m_openList[m/2]] )
{
temp = m_openList[m/2];
m_openList[m/2] = m_openList[m];
m_openList[m] = temp;
m = m/2;
}
else
break;
}
numberOfOpenListItems = numberOfOpenListItems+1;
m_whichList[indexEx] = onOpenList;
}
else
{
if ( iChoice>8 )
addedGCost = (int)(G.__139(parentX, parentY, a, b)*10.0f);
else if ( Mathf.Abs(a-parentX)==1 && Mathf.Abs(b-parentY)==1 )
addedGCost = 28;
else
addedGCost = 10;
tempGcost = m_Gcost[parentIndexEx] + addedGCost;
if ( tempGcost<m_Gcost[indexEx] )
{
m_parentX[indexEx] = parentX;
m_parentY[indexEx] = parentY;
m_Gcost[indexEx] = tempGcost;
for ( int i=1 ; i<=numberOfOpenListItems ; i++ )
{
if ( m_openX[m_openList[i]]==a && m_openY[m_openList[i]]==b )
{
m_Fcost[m_openList[i]] = m_Gcost[indexEx] + m_Hcost[m_openList[i]];
m = i;
while ( m!=1 )
{
if ( m_Fcost[m_openList[m]]<m_Fcost[m_openList[m/2]] )
{
temp = m_openList[m/2];
m_openList[m/2] = m_openList[m];
m_openList[m] = temp;
m = m/2;
}
else
break;
}
break;
}
}
}
}
}
}
else
{
path = FAILED;
break;
}
if ( m_whichList[targetIndexEx]==onOpenList )
{
path = FOUND;
break;
}
}
if ( path==FOUND )
{
m_startCol = startCol;
m_startRow = startRow;
pathCol = targetCol;
pathRow = targetRow;
do
{
int indexEx = __511(pathCol, pathRow);
tempCol = m_parentX[indexEx];
pathRow = m_parentY[indexEx];
pathCol = tempCol;
m_pathLength++;
} while ( pathCol!=startCol || pathRow!=startRow );
int pathBankCount = m_pathBank.Count;
for ( int i=pathBankCount ; i<m_pathLength*8 ; i++ )
m_pathBank.Add(0);
pathCol = targetCol;
pathRow = targetRow;
cellPosition = m_pathLength*2;
do
{
cellPosition = cellPosition - 2;
m_pathBank[cellPosition] = pathCol;
m_pathBank[cellPosition+1] = pathRow;
int indexEx = __511(pathCol, pathRow);
tempCol = m_parentX[indexEx];
pathRow = m_parentY[indexEx];
pathCol = tempCol;
} while ( pathCol!=startCol || pathRow!=startRow );
m_xTrg = x;
m_yTrg = y;
}
if ( isBridge )
{
m_hasBridgeAtStart = true;
m_pathBank.Insert(0, startRow);
m_pathBank.Insert(0, startCol);
m_pathLength++;
startCol = G.__145(x);
startRow = G.__145(y);
m_pathBank.Insert(0, startRow);
m_pathBank.Insert(0, startCol);
m_pathLength++;
m_startCol = startCol;
m_startRow = startRow;
path = FOUND;
m_xTrg = x;
m_yTrg = y;
}
if ( path==FOUND && m_pathLength>1 )
{
m_xFinalTrg = G.__146(m_pathBank[(m_pathLength-1)*2-2]);
m_yFinalTrg = G.__146(m_pathBank[(m_pathLength-1)*2-1]);
}
m_pathStatus = path;
return m_pathStatus;
noPath:
m_xTrg = x;
m_yTrg = y;
m_pathStatus = FAILED;
return m_pathStatus;
}
public bool __516(float currentX, float currentY, float len)
{
bool changed = false;
if ( m_pathStatus==FOUND && m_pathLength>0 )
{
if ( m_pathLocation<m_pathLength )
{
if ( m_pathLocation==0 || G.__139(currentX, currentY, m_xTrg, m_yTrg)<=len )
{
m_pathLocation++;
changed = true;
}
}
if ( m_pathLocation<=m_pathLength )
{
m_xTrg = G.__146(m_pathBank[m_pathLocation*2-2]);
m_yTrg = G.__146(m_pathBank[m_pathLocation*2-1]);
}
if ( m_pathLocation==m_pathLength )
{
if ( G.__139(currentX, currentY, m_xTrg, m_yTrg)<=len )
m_pathStatus = NOT_STARTED;
}
}
else
{
m_xTrg = currentX;
m_yTrg = currentY;
}
return changed;
}
}
