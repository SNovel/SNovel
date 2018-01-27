using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace SNovel
{
    /*
     * tag = luascript
     * 
     * <desc>
     * 执行lua脚本
     * 
     * <param>
     * @script: 代码字符串
     * <sample>
     * $script
     *  goof = 1
     * $end
     */
    public class LuascriptTag : AbstractTag
    {
        public LuascriptTag()
        {
            DefaultParams = new Dictionary<string, string>()
            {
                {"script", "null"}
            };

            VitalParams = new List<string>()
            {
            };
        }

        public override void Excute()
        {
            Debug.Log("Run Lua");
            var state = Engine.LuaState;
            state.L_DoString(Params["script"]);
        }
    }
}

