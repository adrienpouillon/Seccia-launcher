using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Variable
{
public string m_name;
public string m_value;
public List<string> m_list = null;
public static implicit operator bool(Variable inst) { return inst!=null; }
public void Reset()
{
m_name = "";
m_value = "";
m_list = null;
}
}
