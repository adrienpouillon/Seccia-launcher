using UnityEngine;
public class Curve
{
public int m_count;
public float[] m_x;
public float[] m_y;
public float[] m_x1;
public float[] m_y1;
public float[] m_x2;
public float[] m_y2;
public static implicit operator bool(Curve inst) { return inst!=null; }
public void __44(Asset asset)
{
m_count = asset.__12();
if ( m_count==0 )
{
m_x = new float[2];
m_y = new float[2];
m_x[0] = 0.0f;
m_y[0] = 0.0f;
m_x[1] = 1.0f;
m_y[1] = 1.0f;
}
else if ( m_count==1 )
{
m_x = new float[2];
m_y = new float[2];
m_x[0] = G.__110(asset.__16());
m_y[0] = G.__110(asset.__16());
m_x[1] = G.__110(asset.__16());
m_y[1] = G.__110(asset.__16());
}
else
{
m_x = new float[m_count+1];
m_y = new float[m_count+1];
m_x1 = new float[m_count];
m_y1 = new float[m_count];
m_x2 = new float[m_count];
m_y2 = new float[m_count];
for ( int i=0 ; i<m_count ; i++ )
{
m_x[i] = G.__110(asset.__16());
m_y[i] = G.__110(asset.__16());
m_x1[i] = G.__110(asset.__16());
m_y1[i] = G.__110(asset.__16());
m_x2[i] = G.__110(asset.__16());
m_y2[i] = G.__110(asset.__16());
}
m_x[m_count] = G.__110(asset.__16());
m_y[m_count] = G.__110(asset.__16());
}
}
public float GetValue(float ratio)
{
if ( m_count==0 )
return ratio;
if ( m_count==1 )
{
ratio = (ratio-m_x[0])/(m_x[1]-m_x[0]);
return G.__135(G.Clamp(ratio), m_y[0], m_y[1]);
}
int seg = m_count - 1;
float s = 1.0f;
for ( int i=1 ; i<=m_count ; i++ )
{
if ( ratio<m_x[i] )
{
seg = i - 1;
float len = m_x[i] - m_x[seg];
if ( G.__129(len)==false )
s = G.Clamp((ratio-m_x[seg])/len);
break;
}
}
float segLen = m_x[seg+1] - m_x[seg];
float step = 0.001f / segLen;
float s2 = 0.0f;
float omt = 0.0f;
float omt2 = 0.0f;
while ( s<1.0f )
{
s2 = s*s;
omt = 1.0f - s;
omt2 = omt*omt;
float x = omt2*omt*m_x[seg] + 3.0f*omt2*s*m_x1[seg] + 3.0f*omt*s2*m_x2[seg] + s2*s*m_x[seg+1];
if ( x>=ratio )
break;
s += step;
s = G.Clamp(s);
}
s2 = s*s;
omt = 1.0f - s;
omt2 = omt*omt;
float y = omt2*omt*m_y[seg] + 3.0f*omt2*s*m_y1[seg] + 3.0f*omt*s2*m_y2[seg] + s2*s*m_y[seg+1];
return G.Clamp(y);
}
public bool __45(int[] values)
{
if ( values.Length<2 )
return false;
int max = values.Length - 1;
float step = 1.0f / max;
float offset = 0.0f;
for ( int i=0 ; i<values.Length ; i++ )
{
if ( i==max )
offset = 1.0f;
values[i] = Mathf.RoundToInt(GetValue(offset)*max);
offset += step;
}
return true;
}
public bool __45(float[] values)
{
if ( values.Length<2 )
return false;
int max = values.Length - 1;
float step = 1.0f / max;
float offset = 0.0f;
for ( int i=0 ; i<values.Length ; i++ )
{
if ( i==max )
offset = 1.0f;
values[i] = GetValue(offset);
offset += step;
}
return true;
}
}
