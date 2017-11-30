using System;
using System.Linq;
using System.Collections.Generic;
namespace WentOne.Common.Utils
{
	public class StringUtil
	{
		public static int CountCharInString(string source,char target){
			return source.Where<char> ((c, i) => c == target).Count<char> ();
		}
	}
}

