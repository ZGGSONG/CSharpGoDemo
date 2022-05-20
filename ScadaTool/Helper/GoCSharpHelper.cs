using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ScadaTool.Helper
{
    public class GoCSharpHelper : SingletonMode<GoCSharpHelper>
    {
        /// <summary>
        /// 转换Go string类型为C# String
        /// </summary>
        public struct GoString
        {
            public IntPtr p;
            public int n;
            public GoString(IntPtr n1, int n2)
            {
                p = n1; n = n2;
            }
        }
        public string GoStringToCSharpString(GoString goString)
        {
            byte[] bytes = new byte[goString.n];
            for (int i = 0; i < goString.n; i++)
            {
                bytes[i] = Marshal.ReadByte(goString.p, i);
            }
            string result = Encoding.UTF8.GetString(bytes);
            return result;
        }
    }
}
