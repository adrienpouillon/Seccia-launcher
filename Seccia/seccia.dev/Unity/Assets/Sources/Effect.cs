using UnityEngine;
using System;
using System.Collections.Generic;
public class Effect
{
public int m_sid;
public string m_uid;
public byte[] m_output = new byte[(int)FXO.COUNT];
public EffectItem[] m_items = null;
public static implicit operator bool(Effect inst) { return inst!=null; }
public EffectItem __63(string name)
{
switch ( name )
{
case "BCS":					return new EffectItem_BCS(this);
case "BEACON":				return new EffectItem_Beacon(this);
case "BLUR":				return new EffectItem_Blur(this);
case "COLORIZATION":		return new EffectItem_Colorization(this);
case "CURVE":				return new EffectItem_Curve(this);
case "FIRE":				return new EffectItem_Fire(this);
case "GRAIN":				return new EffectItem_Grain(this);
case "GRAYSCALE":			return new EffectItem_Grayscale(this);
case "LUT":					return new EffectItem_LUT(this);
case "PARALLAX":			return new EffectItem_Parallax(this);
case "RAIN":				return new EffectItem_Rain(this);
}
return null;
}
public void __64(Asset asset)
{
int count = asset.__12();
if ( count==0 )
return;
m_items = new EffectItem[count];
for ( int i=0 ; i<count ; i++ )
{
string name = asset.__18();
m_items[i] = __63(name);
m_items[i].__64(asset);
}
for ( int i=0 ; i<m_output.Length ; i++ )
m_output[i] = (byte)asset.__12();
}
public void __65()
{
if ( m_items!=null )
{
for ( int i=0 ; i<m_items.Length ; i++ )
{
m_items[i].__65();
m_items[i] = null;
}
m_items = null;
}
}
public void Reset()
{
if ( m_items!=null )
{
for ( int i=0 ; i<m_items.Length ; i++ )
m_items[i].Reset();
}
}
public int __66()
{
return m_items==null ? 0 : m_items.Length;
}
public int __67(FXO output)
{
return m_output[(int)output];
}
public EffectItem __68(FXO output)
{
for ( int i=0 ; i<m_items.Length ; i++ )
{
if ( m_items[i].__74(output) )
return m_items[i];
}
return null;
}
public bool __69(ref int index, FXO output)
{
for ( int i=index+1 ; i<m_items.Length ; i++ )
{
if ( m_items[i].__74(output) )
{
index = i;
return true;
}
}
return false;
}
public void __70(FXO output, Texture texture, float nativeWidth = 0.0f, float nativeHeight = 0.0f, float opacity = 1.0f, BLEND blend = BLEND.DEFAULT, Sprite depth = null)
{
int count = __67(output);
if ( count==0 )
return;
if ( count==1 )
{
EffectItem item = __68(output);
if ( item )
item.__70(texture, nativeWidth, nativeHeight, opacity, blend, depth);
}
else
{
RenderTexture[] rts = new RenderTexture[2];
rts[0] = G.__168();
rts[1] = null;
int iRT = 0;
RenderTexture old = G.__173(rts[iRT]);
G.__172();
int index = -1;
for ( int i=0 ; i<count-1 ; i++ )
{
__69(ref index, output);
if ( i>0 )
{
iRT = 1 - iRT;
if ( rts[iRT]==null )
rts[iRT] = G.__168();
G.__173(rts[iRT]);
G.__172();
}
m_items[index].__70(texture, nativeWidth, nativeHeight, i==0 ? opacity : 1.0f, BLEND.DEFAULT, depth);
texture = rts[iRT];
}
G.__173(old);
__69(ref index, output);
m_items[index].__70(texture, nativeWidth, nativeHeight, 1.0f, blend, depth);
G.__170(rts[0]);
G.__170(rts[1]);
}
}
}
public class EffectItem
{
public enum MODEL
{
NONE,
BCS,
BEACON,
BLUR,
COLORIZATION,
CURVE,
DOWN,
FIRE,
GRAIN,
GRAYSCALE,
LUT,
PARALLAX,
RAIN,
UP,
}
public Effect m_parent;
public MODEL m_model = MODEL.NONE;
public Dictionary<string, string> m_params;
public bool[] m_output = new bool[(int)FXO.COUNT];
public static implicit operator bool(EffectItem inst) { return inst!=null; }
public virtual void __64(Asset asset)
{
int count = asset.__12();
m_params = new Dictionary<string, string>();
for ( int i=0 ; i<count ; i++ )
m_params.Add(asset.__18(), asset.__18());
uint bitset = asset.__16();
uint mask = 1;
for ( int i=0 ; i<m_output.Length ; i++ )
{
m_output[i] = (bitset & mask)!=0;
mask <<= 1;
}
}
public virtual void __65()
{
m_params = null;
}
public virtual void Reset()
{
}
public string __18(string name)
{
string value;
if ( m_params.TryGetValue(name, out value) )
return value;
return "";
}
public bool __10(string name)
{
string value;
if ( m_params.TryGetValue(name, out value) )
return G.__112(value);
return false;
}
public int __71(string name)
{
string value;
if ( m_params.TryGetValue(name, out value) )
return G.__113(ref value);
return 0;
}
public float __72(string name)
{
string value;
if ( m_params.TryGetValue(name, out value) )
return G.__114(ref value);
return 0.0f;
}
public Color __73(string name)
{
string value;
if ( m_params.TryGetValue(name, out value) )
return G.__125((uint)G.__113(ref value));
return Color.black;
}
public bool __74(FXO output)
{
return m_output[(int)output];
}
public void __75(Texture texture)
{
G.__70(texture);
}
public virtual void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
}
}
public class EffectItem_BCS : EffectItem
{
public Material m_material = null;
public float m_brightness;
public float m_contrast;
public float m_saturation;
public static implicit operator bool(EffectItem_BCS inst) { return inst!=null; }
public EffectItem_BCS(Effect parent)
{
m_model = MODEL.BCS;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_brightness = __72("brightness");
m_contrast = __72("contrast");
m_saturation = __72("saturation");
m_material = G.__165(SHADER.BCS);
m_material.SetFloat("_brightness", m_brightness);
m_material.SetFloat("_contrast", m_contrast);
m_material.SetFloat("_saturation", m_saturation);
}
public override void __65()
{
G.Release(m_material);
m_material = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
m_material.mainTexture = texture;
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
}
}
public class EffectItem_Beacon : EffectItem
{
public Material m_material = null;
public float m_width;
public float m_speed;
public float m_boost;
public float m_softness;
public float m_angle;
public float m_space;
public static implicit operator bool(EffectItem_Beacon inst) { return inst!=null; }
public EffectItem_Beacon(Effect parent)
{
m_model = MODEL.BEACON;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_width = __72("width");
m_speed = __72("speed");
m_boost = __72("boost");
m_softness = __72("softness");
m_angle = __72("angle");
m_space = __72("space");
m_material = G.__165(SHADER.BEACON);
m_material.SetFloat("_width", m_width);
m_material.SetFloat("_speed", m_speed);
m_material.SetFloat("_boost", m_boost);
m_material.SetFloat("_softness", m_softness);
m_material.SetFloat("_angle", m_angle);
m_material.SetFloat("_space", m_space);
}
public override void __65()
{
G.Release(m_material);
m_material = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
m_material.mainTexture = texture;
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
}
}
public class EffectItem_Blur : EffectItem
{
#if UNITY_WEBGL
const float m_tweak = 0.1f;
#else
const float m_tweak = 32.0f;
#endif
public float m_size;
public float m_maxSize;
public float m_maxZoom;
public static implicit operator bool(EffectItem_Blur inst) { return inst!=null; }
public EffectItem_Blur(Effect parent)
{
m_model = MODEL.BLUR;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_size = G.Clamp(__72("size"));
m_maxSize = G.Clamp(__72("maxSize"), m_size, 1.0f);
m_maxZoom = G.Clamp(__72("maxZoom"), 1.0f, 4.0f);
}
public override void __65()
{
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
float scale = G.m_game.__291().m_renderScale;
float size = m_size;
if ( m_maxZoom!=1.0f && m_size!=1.0f )
{
float s = (scale-1.0f)/(m_maxZoom-1.0f);
size += (m_maxSize-m_size)*s;
size = G.Clamp(size, m_size, m_maxSize);
}
if ( size==0.0f )
{
__75(texture);
return;
}
float radius = size * scale * m_tweak;
G.__176(radius, texture);
}
public void __76(float size, float maxZoom)
{
m_size = size;
m_maxZoom = maxZoom;
}
}
public class EffectItem_Colorization : EffectItem
{
public Material m_material = null;
public Color m_shadowColor;
public Color m_highlightColor;
public float m_smooth;
public float m_smoothLen;
public float m_smoothMin;
public static implicit operator bool(EffectItem_Colorization inst) { return inst!=null; }
public EffectItem_Colorization(Effect parent)
{
m_model = MODEL.COLORIZATION;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_shadowColor = __73("shadow");
m_highlightColor = __73("highlight");
m_smooth = G.Clamp(__71("smooth"), 1, 100)/100.0f*0.5f;
m_smoothMin = 0.5f - m_smooth;
m_smoothLen = (0.5f+m_smooth) - m_smoothMin;
m_material = G.__165(SHADER.COLORIZATION);
m_material.SetColor("_shadowColor", m_shadowColor);
m_material.SetColor("_highlightColor", m_highlightColor);
m_material.SetFloat("_smoothMin", m_smoothMin);
m_material.SetFloat("_smoothLen", m_smoothLen);
}
public override void __65()
{
G.Release(m_material);
m_material = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
m_material.mainTexture = texture;
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
}
}
public class EffectItem_Curve : EffectItem
{
public Material m_material = null;
public static implicit operator bool(EffectItem_Curve inst) { return inst!=null; }
public EffectItem_Curve(Effect parent)
{
m_model = MODEL.CURVE;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_material = G.__165(SHADER.CURVE);
float[] values = new float[256];
for ( int i=0 ; i<values.Length ; i++ )
values[i] = asset.__12()/255.0f;
m_material.SetFloatArray("_lum", values);
for ( int i=0 ; i<values.Length ; i++ )
values[i] = asset.__12()/255.0f;
m_material.SetFloatArray("_sat", values);
values = new float[360];
for ( int i=0 ; i<values.Length ; i++ )
values[i] = asset.__14()/359.0f;
m_material.SetFloatArray("_hue", values);
}
public override void __65()
{
G.Release(m_material);
m_material = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
m_material.mainTexture = texture;
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
}
}
public class EffectItem_Grain : EffectItem
{
public Material m_material = null;
public float m_gain;
public bool m_initialized = false;
public static implicit operator bool(EffectItem_Grain inst) { return inst!=null; }
public EffectItem_Grain(Effect parent)
{
m_model = MODEL.GRAIN;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_gain = G.Clamp(__71("gain"), 0, 100)/100.0f;
m_material = G.__165(SHADER.GRAIN);
m_material.SetTexture("_grainTex", null);
}
public override void __65()
{
G.Release(m_material);
m_material = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
if ( m_initialized==false )
{
m_material.SetTexture("_grainTex", G.m_textureGrain);
m_initialized = true;
}
m_material.mainTexture = texture;
m_material.SetFloat("_gain", m_gain);
m_material.SetFloat("_delta", G.__156(0.0f, 1.0f));
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
}
}
public class EffectItem_Grayscale : EffectItem
{
public Material m_material = null;
public static implicit operator bool(EffectItem_Grayscale inst) { return inst!=null; }
public EffectItem_Grayscale(Effect parent)
{
m_model = MODEL.GRAYSCALE;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_material = G.__165(SHADER.GRAYSCALE);
}
public override void __65()
{
G.Release(m_material);
m_material = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
m_material.mainTexture = texture;
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
}
}
public class EffectItem_LUT : EffectItem
{
public Material m_material = null;
public Material m_materialGray = null;
public int m_index = 0;
public float m_row = 0.0f;
public static implicit operator bool(EffectItem_LUT inst) { return inst!=null; }
public EffectItem_LUT(Effect parent)
{
m_model = MODEL.LUT;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
if ( G.m_game.m_lutNames.TryGetValue(__18("style"), out m_index)==false )
m_index = 0;
m_row = 1.0f-((m_index-1)/255.0f);
m_material = G.__165(SHADER.LUT);
m_materialGray = G.__165(SHADER.GRAYSCALE);
m_material.SetTexture("_lutTex", G.m_game.m_lutTexture);
m_material.SetFloat("_row", m_row);
}
public override void __65()
{
G.Release(m_material);
G.Release(m_materialGray);
m_material = null;
m_materialGray = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
if ( m_index==0 )
{
m_materialGray.mainTexture = texture;
G.m_graphics.__363(m_materialGray, opacity);
m_materialGray.mainTexture = null;
}
else
{
m_material.mainTexture = texture;
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
}
}
}
public class EffectItem_Fire : EffectItem
{
public Material m_material = null;
public float m_blend;
public float m_speed;
public static implicit operator bool(EffectItem_Fire inst) { return inst!=null; }
public EffectItem_Fire(Effect parent)
{
m_model = MODEL.FIRE;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_speed = __72("speed");
m_material = G.__165(SHADER.FIRE);
m_material.SetFloat("_blend", m_blend);
m_material.SetFloat("_ratio", G.m_gameRatio);
m_material.SetFloat("_speed", m_speed);
}
public override void __65()
{
G.Release(m_material);
m_material = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
m_material.mainTexture = texture;
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
}
}
public class EffectItem_Parallax : EffectItem
{
public Material m_material = null;
public float m_x;
public float m_y;
public float m_scale;
public float m_focus;
public float m_ratioX;
public float m_ratioY;
public static implicit operator bool(EffectItem_Parallax inst) { return inst!=null; }
public EffectItem_Parallax(Effect parent)
{
m_model = MODEL.PARALLAX;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_material = G.__165(SHADER.PARALLAX);
m_material.SetTexture("_depthTex", null);
__76(__71("angle"), __71("scale"), __71("focus"));
}
public override void __65()
{
G.Release(m_material);
m_material = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
m_material.mainTexture = texture;
if ( depth )
{
float ratio = nativeWidth/depth.m_width;
if ( ratio!=m_ratioX )
{
m_ratioX = ratio;
m_material.SetFloat("_ratioX", ratio);
}
ratio = nativeHeight/depth.m_height * nativeWidth/nativeHeight;
if ( ratio!=m_ratioY )
{
m_ratioY = ratio;
m_material.SetFloat("_ratioY", ratio);
}
m_material.SetTexture("_depthTex", depth.m_texture);
}
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
if ( depth )
m_material.SetTexture("_depthTex", null);
}
public void __76(int angle, int scale, int focus)
{
float a = G.__142(angle);
m_x = Mathf.Sin(a);
m_y = Mathf.Cos(a);
m_scale = G.Clamp(scale, 0, 100)/100.0f;
m_scale *= 0.05f;
m_focus = G.Clamp(focus, 0, 100)/100.0f;
m_ratioX = 1.0f;
m_ratioY = 1.0f;
m_material.SetFloat("_x", m_x);
m_material.SetFloat("_y", m_y);
m_material.SetFloat("_scale", m_scale);
m_material.SetFloat("_focus", m_focus);
m_material.SetFloat("_ratioX", m_ratioX);
m_material.SetFloat("_ratioY", m_ratioY);
}
}
public class EffectItem_Rain : EffectItem
{
public Material m_material = null;
public float m_blend;
public int m_count;
public float m_distance;
public float m_direction;
public Color m_lightColor;
public static implicit operator bool(EffectItem_Rain inst) { return inst!=null; }
public EffectItem_Rain(Effect parent)
{
m_model = MODEL.RAIN;
m_parent = parent;
}
public override void __64(Asset asset)
{
base.__64(asset);
m_count = G.Clamp(__71("count"), 1, 20);
m_distance = (float)G.Clamp(__71("distance"), 1, 100);
m_direction = __72("direction");
m_lightColor = __73("color");
m_material = G.__165(SHADER.RAIN);
m_material.SetTexture("_noiseTex", G.m_spriteNoise.m_texture);
m_material.SetFloat("_blend", m_blend);
m_material.SetInt("_count", m_count);
m_material.SetFloat("_distance", m_distance);
m_material.SetFloat("_direction", m_direction);
m_material.SetColor("_lightColor", m_lightColor);
}
public override void __65()
{
G.Release(m_material);
m_material = null;
base.__65();
}
public override void __70(Texture texture, float nativeWidth, float nativeHeight, float opacity, BLEND blend, Sprite depth)
{
m_material.mainTexture = texture;
G.m_graphics.__363(m_material, opacity, blend);
m_material.mainTexture = null;
}
}
