using System.Text.RegularExpressions;
using UniLua;

namespace SNovel
{
    public static class LuaHelper
    {
        public static string GetString(ILuaState state, string name)
        {
            state.GetGlobal(name);
            return state.ToString(-1);
        }

        //获取{var}中的var值
        public static string ReplaceScopedVarInString(ILuaState state, string text)
        {
           return Regex.Replace(text, @"(\{\w+\})", (m)=> ReplaceVar(state, m));
        }

       public static string ReplaceVar(ILuaState state, Match m)
       {
           if (m.Value == "{}")
               return "";
           return GetString(state, m.Value.Substring(1, m.Value.Length - 2));
       }
    }
}