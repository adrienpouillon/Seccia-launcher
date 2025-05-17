using UnityEngine;
public class Asset
{
private bool m_doNotUseYek = false;
private bool m_closeDisabled = false;
private bool m_isValid = false;
private int m_pos = 0;
private int m_size = 0;
private byte[] m_bytes = null;
private int m_fileOffset = 0;
private System.IO.Stream m_stream = null;
private bool m_isStream = false;
public static implicit operator bool(Asset inst) { return inst!=null; }
public Asset(bool close = true)
{
m_closeDisabled = !close;
if ( m_bytes==null && m_fileOffset==0 )
{
}
}
public void __0()
{
m_doNotUseYek = true;
}
public bool __1()
{
return m_isValid;
}
#if UNITY_ANDROID
public bool Open(string path)
{
Close();
int index;
if ( G.m_obbMap.TryGetValue(path, out index)==false )
{
System.IO.Stream stream = G.OpenFile(path);
if ( stream==null )
return false;
m_isValid = true;
m_fileOffset = 0;
m_pos = 0;
m_size = (int)stream.Length;
m_stream = stream;
}
else
{
System.IO.Stream stream = G.OpenFile(G.m_obbPath);
if ( stream==null )
return false;
m_isValid = true;
m_fileOffset = G.m_obbOffsets[index];
m_pos = 0;
m_size = G.m_obbSizes[index];
m_stream = stream;
m_stream.Position = m_fileOffset;
}
m_isStream = true;
return true;
}
#else
public bool Open(string path)
{
Close();
System.IO.Stream stream = G.OpenFile(path);
if ( stream==null )
return false;
m_isValid = true;
m_fileOffset = 0;
m_pos = 0;
m_size = (int)stream.Length;
m_stream = stream;
m_isStream = true;
return true;
}
#endif
public bool Open(byte[] bytes)
{
Close();
m_isValid = true;
m_pos = 0;
m_size = bytes.Length;
m_bytes = bytes;
return true;
}
public void Close()
{
if ( m_closeDisabled )
return;
m_isValid = false;
m_pos = 0;
m_size = 0;
m_bytes = null;
m_fileOffset = 0;
if ( m_stream!=null )
{
m_stream.Close();
m_stream = null;
}
m_isStream = false;
}
public void __2()
{
if ( m_pos>0 )
{
m_pos--;
if ( m_isStream )
m_stream.Position = m_fileOffset + m_pos;
}
}
public void __3(int pos)
{
m_pos = pos;
if ( m_isStream )
m_stream.Position = m_fileOffset + m_pos;
}
public void __4(int pos)
{
m_pos += pos;
if ( m_isStream )
m_stream.Position = m_fileOffset + m_pos;
}
public int __5()
{
return m_pos;
}
public int __6()
{
return m_size;
}
public int __7()
{
int size = m_size - m_pos;
return size<0 ? 0 : size;
}
public void __8()
{
if ( m_isStream )
{
m_stream.Position = m_fileOffset + m_size - 256;
for ( int i=0 ; i<128 ; i++ )
G.m_yek[i] = (byte)(m_stream.ReadByte()+m_stream.ReadByte());
m_pos = 0;
m_stream.Position = m_fileOffset;
}
else
{
for ( int i=0, j=0 ; i<128 ; i++, j+=2 )
G.m_yek[i] = (byte)(m_bytes[m_size-256+j]+m_bytes[m_size-256+j+1]);
m_pos = 0;
}
}
int Read()
{
int i = m_pos;
m_pos++;
if ( m_isStream )
{
if ( m_doNotUseYek )
return m_stream.ReadByte();
return (int)(byte)(m_stream.ReadByte()-G.m_yek[i%128]);
}
else
{
if ( m_doNotUseYek )
return (int)m_bytes[i];
return (int)(byte)(m_bytes[i]-G.m_yek[i%128]);
}
}
public void __9(byte[] buffer, int size)
{
int remainingSize = __7();
if ( size>remainingSize )
size = remainingSize;
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
size = m_stream.Read(buffer, 0, size);
for ( int i=0 ; i<size ; i++ )
{
if ( m_doNotUseYek==false )
buffer[i] = (byte)(buffer[i]-G.m_yek[m_pos%128]);
m_pos++;
}
}
else
{
for ( int i=0 ; i<size ; i++ )
{
if ( m_doNotUseYek )
buffer[i] = m_bytes[m_pos];
else
buffer[i] = (byte)(m_bytes[m_pos]-G.m_yek[m_pos%128]);
m_pos++;
}
}
}
public byte[] __9(int size)
{
byte[] bytes = new byte[size];
__9(bytes, size);
return bytes;
}
public bool __10()
{
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
}
return Read()!=0;
}
public int __11()
{
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
}
int val = Read();
return val<128 ? val : -(256-val);
}
public int __12()
{
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
}
return Read();
}
public int __13()
{
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
}
ushort val = (ushort)Read();
val |= (ushort)(Read()<<8);
return (int)(short)val;
}
public int __14()
{
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
}
ushort val = (ushort)Read();
val |= (ushort)(Read()<<8);
return (int)val;
}
public int __15()
{
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
}
uint val = (uint)Read();
val |= (uint)(Read()<<8);
val |= (uint)(Read()<<16);
val |= (uint)(Read()<<24);
return (int)val;
}
public uint __16()
{
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
}
uint val = (uint)Read();
val |= (uint)(Read()<<8);
val |= (uint)(Read()<<16);
val |= (uint)(Read()<<24);
return val;
}
public ulong __17()
{
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
}
ulong val = (ulong)Read();
val |= (ulong)(Read())<<8;
val |= (ulong)(Read())<<16;
val |= (ulong)(Read())<<24;
val |= (ulong)(Read())<<32;
val |= (ulong)(Read())<<40;
val |= (ulong)(Read())<<48;
val |= (ulong)(Read())<<56;
return val;
}
public string __18()
{
string str = "";
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
while ( true )
{
int c = Read();
if ( c==0 || c==-1 )
break;
str += (char)(byte)c;
}
}
else
{
while ( m_pos<m_size )
{
int c = Read();
if ( c==0 )
break;
str += (char)(byte)c;
}
}
return str;
}
public string __19()
{
if ( m_isStream )
{
if ( m_stream.Position!=m_fileOffset+m_pos )
m_stream.Position = m_fileOffset + m_pos;
}
string str = "";
while ( m_pos<m_size )
{
int c = __13();
if ( c==0 )
break;
str += (char)c;
}
return str;
}
public float __20()
{
float dist = (float)__12();
if ( dist==0.0f )
return 0.0f;
dist = (dist+1.0f) * G.PATH_GRID_CELLSIZE;
return dist * dist;
}
public Term __21()
{
Term term = new Term();
for ( int i=0 ; i<G.m_game.m_languages.Length ; i++ )
term.m_values[i] = __19();
return term;
}
public Term __22(int lang)
{
Term term = new Term(true);
term.m_values[0] = __19();
return term;
}
public Color __23(int defRed = 255, int defGreen = 255, int defBlue = 255)
{
return G.__162(__18(), defRed, defGreen, defBlue);
}
public Color __23(Color defColor)
{
return G.__162(__18(), (int)(defColor.r*255.0f), (int)(defColor.g*255.0f), (int)(defColor.b*255.0f));
}
public Color __24()
{
return new Color(__12()/255.0f, __12()/255.0f, __12()/255.0f);
}
public Color __25()
{
return new Color(__12()/255.0f, __12()/255.0f, __12()/255.0f, __12()/255.0f);
}
public string[] ReadTags()
{
string[] tags;
int tagCount = __12();
if ( tagCount==0 )
tags = new string[1];
else
{
tags = new string[tagCount+1];
for ( int i=0 ; i<tagCount ; i++ )
tags[i+1] = __18();
}
tags[0] = "";
return tags;
}
public Script __26()
{
int count = __13();
if ( count==0 )
return null;
Script script = new Script();
script.__64(this, count);
return script;
}
public Sprite __27()
{
int size = __15();
if ( size==0 )
return null;
int sizeHalf = __15();
int offset = __15();
__13();
Sprite sprite = new Sprite();
if ( size==-1 )
{
sprite.m_width = __13();
sprite.m_height = __13();
sprite.m_path = __18();
}
else
{
sprite.m_offset = offset;
sprite.m_size = size;
sprite.m_sizeHalf = sizeHalf;
sprite.m_width = __13();
sprite.m_height = __13();
}
return sprite;
}
public LayerSprite __28()
{
int count = __12();
if ( count==0 )
return null;
if ( count==255 )
count = 0;
LayerSprite sprite = new LayerSprite();
int part = __12();
sprite.m_rowCount = __12();
sprite.m_colCount = __12();
if ( sprite.m_rowCount>0 )
{
sprite.m_partWidth = Mathf.Pow(2, part>>4);
sprite.m_partHeight = Mathf.Pow(2, part&0xF);
}
sprite.m_width = __13();
sprite.m_height = __13();
sprite.m_sprites = new Sprite[count];
for ( int i=0 ; i<count ; i++ )
sprite.m_sprites[i] = __27();
return sprite;
}
public Mask __29()
{
int size = __15();
if ( size==0 )
return null;
int offset = __15();
__13();
Mask mask = new Mask();
mask.m_offset = offset;
mask.m_size = size;
return mask;
}
public Sound __30(Sound.TYPE type)
{
Sound sound = new Sound(type);
switch ( type )
{
case Sound.TYPE.SFX:
{
sound.m_offset = __15();
sound.m_size = __15();
sound.m_name = __18();
int headerSize = __15();
int dataSize = sound.m_size - headerSize;
sound.m_offset += headerSize;
sound.m_size = dataSize;
sound.m_stereo = __15()==2;
sound.m_hz = __15();
sound.m_16bits = __15()==16;
sound.m_frameSize = 1;
sound.m_frameSizePerChannel = 1;
if ( sound.m_stereo )
sound.m_frameSize *= 2;
if ( sound.m_16bits )
{
sound.m_frameSize *= 2;
sound.m_frameSizePerChannel *= 2;
}
sound.m_frameCount = dataSize/sound.m_frameSize;
break;
}
default:
{
sound.m_name = __18();
break;
}
}
return sound;
}
public Rect __31()
{
return new Rect((float)__13(), (float)__13(), (float)__13(), (float)__13());
}
public Margin __32()
{
return new Margin((float)__12(), (float)__12(), (float)__12(), (float)__12());
}
}
