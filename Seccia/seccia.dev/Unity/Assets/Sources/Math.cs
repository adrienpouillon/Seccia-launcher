using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public struct Vec2 : IComparable<Vec2>
{
static public Vec2 Zero = new Vec2(0.0f, 0.0f);
public float x;
public float y;
public Vec2(float xx, float yy)
{
x = xx;
y = yy;
}
public float __433()
{
return Mathf.Sqrt(x*x + y*y);
}
public bool __434()
{
float len = Mathf.Sqrt(x*x + y*y);
if ( G.__129(len) )
return false;
x /= len;
y /= len;
return true;
}
public bool __434(float len)
{
if ( G.__129(len) )
return false;
x /= len;
y /= len;
return true;
}
public void __435(float s)
{
x *= s;
y *= s;
}
public void Rotate(float angle)
{
Matrix4x4 mRot = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 0.0f, angle));
float xx = x * mRot.m00 + y * mRot.m01;
float yy = x * mRot.m10 + y * mRot.m11;
x = xx;
y = yy;
}
public int CompareTo(Vec2 cmp)
{
if ( x<cmp.x )
return -1;
else if ( x>cmp.x )
return 1;
if ( y<cmp.y )
return -1;
else if ( y>cmp.y )
return 1;
return 0;
}
}
public struct Vec3
{
static public Vec3 Zero = new Vec3(0.0f, 0.0f, 0.0f);
public float x;
public float y;
public float z;
public Vec3(float xx, float yy, float zz)
{
x = xx;
y = yy;
z = zz;
}
public void __434()
{
float len = Mathf.Sqrt(x*x + y*y + z*z);
if ( G.__129(len) )
return;
x /= len;
y /= len;
z /= len;
}
}
public struct Rect
{
static public Rect Zero = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
public float x;
public float y;
public float width;
public float height;
public Rect(float left, float top, float w, float h)
{
x = left;
y = top;
width = w;
height = h;
}
public void Reset()
{
x = 0.0f;
y = 0.0f;
width = 0.0f;
height = 0.0f;
}
public void Set(float xx, float yy, float w, float h)
{
x = xx;
y = yy;
width = w;
height = h;
}
public void Set(ref Obb obb)
{
x = obb.__443();
y = obb.__444();
width = obb.__436() - x;
height = obb.__437() - y;
}
public float __436()
{
return x + width;
}
public float __437()
{
return y + height;
}
public Vec2 __438()
{
return new Vec2(x+width*0.5f, y+height*0.5f);
}
public float __439()
{
return x + width*0.5f;
}
public float __440()
{
return y + height*0.5f;
}
public bool Contains(float xx, float yy)
{
if ( xx>=x && xx<x+width && yy>=y && yy<y+height )
return true;
return false;
}
public void __441(ref float time, ref uint frame)
{
if ( frame+1!=CameraBehavior.s_iRender )
time = 0.0f;
else
time += G.m_game.m_elapsed;
frame = CameraBehavior.s_iRender;
x += width*0.5f;
y += height*0.5f;
float scale = 1.0f - Mathf.Sin(time*5.0f)*0.05f;
width *= scale;
height *= scale;
x -= width*0.5f;
y -= height*0.5f;
}
public void __442(float scale)
{
float w = width;
float h = height;
width *= scale;
height *= scale;
x -= (width-w)*0.5f;
y -= (height-h)*0.5f;
}
}
public struct Obb
{
public Vec2[] pts;
public void Reset()
{
if ( pts==null )
pts = new Vec2[4];
for ( int i=0 ; i<4 ; i++ )
{
pts[i].x = 0.0f;
pts[i].y = 0.0f;
}
}
public void Set(ref Rect rc)
{
pts[0].x = rc.x;
pts[0].y = rc.y;
pts[1].x = rc.__436();
pts[1].y = rc.y;
pts[2].x = rc.__436();
pts[2].y = rc.__437();
pts[3].x = rc.x;
pts[3].y = rc.__437();
}
public float __443()
{
float val = float.MaxValue;
for ( int i=0 ; i<4 ; i++ )
{
if ( pts[i].x<val )
val = pts[i].x;
}
return val;
}
public float __444()
{
float val = float.MaxValue;
for ( int i=0 ; i<4 ; i++ )
{
if ( pts[i].y<val )
val = pts[i].y;
}
return val;
}
public float __436()
{
float val = -float.MaxValue;
for ( int i=0 ; i<4 ; i++ )
{
if ( pts[i].x>val )
val = pts[i].x;
}
return val;
}
public float __437()
{
float val = -float.MaxValue;
for ( int i=0 ; i<4 ; i++ )
{
if ( pts[i].y>val )
val = pts[i].y;
}
return val;
}
public Vec2 __438()
{
Vec2 v;
v.x = (__443()+__436())*0.5f;
v.y = (__444()+__437())*0.5f;
return v;
}
public void Move(float x, float y)
{
for ( int i=0 ; i<4 ; i++ )
{
pts[i].x += x;
pts[i].y += y;
}
}
public void Rotate(float angle)
{
Matrix4x4 mRot = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 0.0f, angle));
for ( int i=0 ; i<4 ; i++ )
{
float x = pts[i].x * mRot.m00 + pts[i].y * mRot.m01;
float y = pts[i].x * mRot.m10 + pts[i].y * mRot.m11;
pts[i].x = x;
pts[i].y = y;
}
}
public bool Contains(float x, float y)
{
float sign = G.__158(x, y, pts[0].x, pts[0].y, pts[1].x, pts[1].y);
if ( sign<0.0f )
return false;
sign = G.__158(x, y, pts[1].x, pts[1].y, pts[2].x, pts[2].y);
if ( sign<0.0f )
return false;
sign = G.__158(x, y, pts[2].x, pts[2].y, pts[3].x, pts[3].y);
if ( sign<0.0f )
return false;
sign = G.__158(x, y, pts[3].x, pts[3].y, pts[0].x, pts[0].y);
return sign>=0.0f;
}
public void __441(ref float time, ref uint frame, float cx, float cy)
{
if ( frame+1!=CameraBehavior.s_iRender )
time = 0.0f;
else
time += G.m_game.m_elapsed;
frame = CameraBehavior.s_iRender;
float scale = 1.0f - Mathf.Sin(time*5.0f)*0.05f;
for ( int i=0 ; i<4 ; i++ )
{
pts[i].x -= cx;
pts[i].y -= cy;
pts[i].x *= scale;
pts[i].y *= scale;
pts[i].x += cx;
pts[i].y += cy;
}
}
}
public struct Margin
{
static public Margin Zero = new Margin(0.0f, 0.0f, 0.0f, 0.0f);
public float left;
public float top;
public float right;
public float bottom;
public Margin(float _left, float _top, float _right, float _bottom)
{
left = _left;
top = _top;
right = _right;
bottom = _bottom;
}
public void Reset()
{
left = 0.0f;
top = 0.0f;
right = 0.0f;
bottom = 0.0f;
}
public float __445()
{
return left + right;
}
public float __446()
{
return top + bottom;
}
public void __447()
{
float tmp = left;
left = right;
right = tmp;
}
public Margin __448(float width, float height)
{
if ( width<=0.0f || height<=0.0f )
return Zero;
Margin margin = this;
margin.left /= width-1.0f;
margin.right /= width-1.0f;
margin.top /= height-1.0f;
margin.bottom /= height-1.0f;
return margin;
}
}
