## 介绍

- 基本MVVM代码结构、包括Command绑定控件
- 实现单例模式
- 实现C#驱动Go项目

- View:
	- MordenWPF美化控件
	- Hardcodet实现WPF系统托盘效果
		- 代码实现图标闪烁


## CSharp using Go

```Go
package main

import (
	"C"
	"zggsong.cn/scadatool/server"
)

func main() {
	run()
}

//export run
func run() string {
	// 启动服务
	server.StartServer()
	return "success"
}
```

```C#
/// <summary>
/// 引入Go项目库
/// </summary>
/// <returns></returns>
[DllImport("scadatool_go.dll", EntryPoint = "run")]
extern static GoString run();

#本项目中的代码示例
string result = GoCSharpHelper.Instance().GoStringToCSharpString(run());

```

**Go字符串乱码问题**
```C#
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
```