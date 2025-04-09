using UnityEngine;
using System;
using System.Collections.Generic;
public class Spline
{
int m_order;
float m_length;
SplinePoint[] m_pts;
float[] m_nodes;
SplinePoint[] m_recPts;
float[] m_recNodes;
public SplineStep[] m_steps;
public float m_pathLength;
public SplinePoint m_pathStart;
public SplinePoint m_pathEnd;
public static implicit operator bool(Spline inst) { return inst!=null; }
public Spline(int order)
{
m_order = order;
}
public void Clear()
{
m_pts = null;
m_nodes = null;
m_recPts = null;
m_recNodes = null;
m_length = 0.0f;
m_steps = null;
m_pathLength = 0.0f;
}
public void Load(Spot[] spots)
{
Clear();
m_recPts = new SplinePoint[m_order];
m_recNodes = new float[m_order*2-2];
m_pts = new SplinePoint[spots.Length];
for ( int i=0 ; i<spots.Length ; i++ )
{
m_pts[i].x = spots[i].m_x;
m_pts[i].y = spots[i].m_y;
m_pts[i].speed = spots[i].m_speed;
m_pts[i].zoom = spots[i].m_zoom;
}
m_length = 0.0f;
for ( int i=1 ; i<m_pts.Length ; i++ )
m_length += G.__140(m_pts[i-1].x, m_pts[i-1].y, m_pts[i].x, m_pts[i].y);
m_length = Mathf.Sqrt(m_length);
m_nodes = new float[m_order+m_pts.Length];
int acc = 1;
for ( int i=0 ; i<m_nodes.Length ; i++ )
{
if ( i<m_order )
m_nodes[i] = 0.0f;
else if ( i>=m_pts.Length+1 )
m_nodes[i] = 1.0f;
else
{
m_nodes[i] = acc / (float)(m_pts.Length+1-m_order);
acc++;
}
}
if ( m_order==2 )
{
m_pathLength = 0.0f;
m_steps = new SplineStep[m_pts.Length];
for ( int i=0 ; i<m_pts.Length ; i++ )
{
m_steps[i] = new SplineStep();
m_pts[i].step = m_steps[i];
}
m_pathStart = m_pts[0];
m_pathEnd = m_pts[m_pts.Length-1];
m_steps[0].m_spot = spots[0];
m_steps[0].m_point = m_pathStart;
for ( int i=1 ; i<m_pts.Length ; i++ )
{
m_steps[i].m_spot = spots[i];
m_steps[i].m_point = m_pts[i];
m_pathLength += G.__139(m_steps[i-1].m_point.x, m_steps[i-1].m_point.y, m_steps[i].m_point.x, m_steps[i].m_point.y);
m_steps[i].m_offset = m_pathLength;
}
}
else
{
int count = (int)(m_length*0.5f);
float max = (float)(count-1);
m_steps = new SplineStep[count];
for ( int i=0 ; i<count ; i++ )
{
float s = i/max;
m_steps[i] = new SplineStep();
m_steps[i].m_point = Eval(s);
m_steps[i].m_point.step = m_steps[i];
}
m_pathStart = m_steps[0].m_point;
m_pathEnd = m_steps[m_steps.Length-1].m_point;
int nextSpot = 1;
float prevDist = float.MaxValue;
int last = 0;
m_steps[0].m_spot = spots[0];
m_pts[0].step = m_steps[0];
for ( int i=1 ; i<count ; i++ )
{
last = i;
float dist = G.__140(m_steps[i].m_point.x, m_steps[i].m_point.y, m_pts[nextSpot].x, m_pts[nextSpot].y);
if ( dist>prevDist )
{
m_steps[i-1].m_spot = spots[nextSpot];
m_steps[i].m_spot = spots[nextSpot];
m_pts[nextSpot].step = m_steps[i-1];
nextSpot++;
if ( nextSpot>=m_pts.Length-1 )
break;
prevDist = G.__140(m_steps[i].m_point.x, m_steps[i].m_point.y, m_pts[nextSpot].x, m_pts[nextSpot].y);
}
else
{
m_steps[i].m_spot = spots[nextSpot-1];
m_pts[nextSpot-1].step = m_steps[i];
prevDist = dist;
}
}
for ( int i=last+1 ; i<count ; i++ )
m_steps[i].m_spot = spots[m_pts.Length-2];
if ( last+1<count )
m_pts[m_pts.Length-2].step = m_steps[last+1];
m_pts[m_pts.Length-1].step = m_steps[m_steps.Length-1];
for ( int i=1 ; i<count ; i++ )
{
m_pathLength += G.__139(m_steps[i-1].m_point.x, m_steps[i-1].m_point.y, m_steps[i].m_point.x, m_steps[i].m_point.y);
m_steps[i].m_offset = m_pathLength;
}
}
#if UNITY_EDITOR
for ( int i=0 ; i<m_pts.Length ; i++ )
{
if ( m_pts[i].step==null )
Debug.Log("BUG SPLINE PT "+i+"/"+m_pts.Length);
}
#endif
}
SplinePoint Eval(float s)
{
s = G.Clamp(s);
int dec = 0;
while( s>m_nodes[dec+m_order] )
dec++;
int decCount = dec + m_order;
if ( decCount>m_pts.Length )
decCount = m_pts.Length;
for ( int i=dec, j=0 ; i<decCount ; i++, j++ )
m_recPts[j] = m_pts[i];
for ( int i=dec+1, j=0 ; i<dec+m_order+m_order-1 ; i++, j++ )
m_recNodes[j] = m_nodes[i];
return EvalRec(s, m_recPts, m_order, m_recNodes);
}
SplinePoint EvalRec(float s, SplinePoint[] srcPts, int srcOrder, float[] srcNodes)
{
if ( srcPts.Length==1 )
return srcPts[0];
SplinePoint[] outPts = new SplinePoint[srcOrder-1];
for ( int i=0 ; i<srcOrder-1 ; i++ )
{
float n0 = srcNodes[i+srcOrder-1];
float n1 = srcNodes[i];
float f0 = (n0 - s) / (n0 - n1);
float f1 = (s - n1) / (n0 - n1);
outPts[i].x = srcPts[i].x*f0 + srcPts[i+1].x*f1;
outPts[i].y = srcPts[i].y*f0 + srcPts[i+1].y*f1;
outPts[i].speed = srcPts[i].speed*f0 + srcPts[i+1].speed*f1;
outPts[i].zoom = srcPts[i].zoom*f0 + srcPts[i+1].zoom*f1;
outPts[i].step = srcPts[i].step;
}
float[] outNodes = new float[srcNodes.Length-2];
for ( int i=1, j=0 ; i<srcNodes.Length-1 ; i++, j++ )
outNodes[j] = srcNodes[i];
return EvalRec(s, outPts, srcOrder-1, outNodes);
}
bool GetCoordsAt(float offset, ref SplinePoint outPoint)
{
if ( offset<0.0f )
offset = 0.0f;
else if ( offset>=m_pathLength )
{
outPoint = m_pathEnd;
return true;
}
for ( int i=1 ; i<m_steps.Length ; i++ )
{
if ( offset<m_steps[i].m_offset )
{
float s = G.Clamp((offset-m_steps[i-1].m_offset)/(m_steps[i].m_offset-m_steps[i-1].m_offset));
outPoint.x = G.__135(s, m_steps[i-1].m_point.x, m_steps[i].m_point.x);
outPoint.y = G.__135(s, m_steps[i-1].m_point.y, m_steps[i].m_point.y);
outPoint.speed = G.__135(s, m_steps[i-1].m_point.speed, m_steps[i].m_point.speed);
outPoint.zoom = G.__135(s, m_steps[i-1].m_point.zoom, m_steps[i].m_point.zoom);
outPoint.step = m_steps[i-1];
return false;
}
}
outPoint = m_pathEnd;
return true;
}
public bool Next(ref float offset, ref SplinePoint outPoint)
{
if ( GetCoordsAt(offset, ref outPoint) )
return true;
offset += outPoint.speed * G.m_game.m_elapsed;
return GetCoordsAt(offset, ref outPoint);
}
}
public class SplineStep
{
public Spot m_spot;
public float m_offset;
public SplinePoint m_point;
}
public struct SplinePoint
{
public float x;
public float y;
public float speed;
public float zoom;
public SplineStep step;
}
