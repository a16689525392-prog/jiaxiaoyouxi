# 键盘驾驶游戏：恢复源码与本地构建

本仓库包含从开发者提供的 Unity 成品恢复的 **418 个 C# 文件**，以及键盘驾驶改造、构建安装脚本和运行测试。
已从这些 C# 文件重新编译 `Assembly-CSharp.dll`，并在原有 Unity 2019.4.9f1 成品中验证车辆前进、倒车、转向、刹车、离合及手刹。

## 工程范围

这是可重新编译游戏业务程序集的恢复源码工程，不是原始 Unity 编辑器工程。
原始场景、模型、音频、材质、编辑器配置和 `.meta` 文件没有作为可编辑 Unity 资源恢复。
运行仍需开发者已有的 Windows 成品资源；本源码 ZIP 不包含 Unity 运行时、场景 AssetBundle、第三方 DLL、账户配置或服务密钥。
源码来自反编译，局部变量名、注释和排版无法保证与原始工程相同。

## 键盘操作

进入能够自由驾驶的自主练习或训练模式。初始为空挡、电源打开、安全带系好。
先按住 F 约一秒点火，再按 1 挂一挡，按住 W 起步。低速起步带自动半离合辅助。

| 按键 | 功能 |
| --- | --- |
| W / S | 油门 / 脚刹 |
| A / D | 左转 / 右转 |
| 空格 / 左 Shift | 手刹 / 离合 |
| F / H | 点火 / 喇叭 |
| 1–5 / R / N | 前进挡 / 倒挡 / 空挡 |
| Q / E / X | 左转向灯 / 右转向灯 / 关闭转向灯 |
| L / V | 切换车灯 / 雨刷 |
| P / B | 切换电源 / 安全带 |
| 方向键 / Enter / Esc | 菜单选择 / 确认 / 返回 |

演示和指导模式继续使用原来的回放或固定路线逻辑。

## 从源码构建

需要 PowerShell 7（`pwsh`，包含 Roslyn 编译器）和 Python 3。
先将开发者已有的完整成品复制到一个独立文件夹，例如 `C:\Games\KeyboardPlayer`。
原版运行时和资源保留；脚本只替换这个副本的游戏业务程序集，并保存替换前的备份。

在本仓库目录运行：

```powershell
pwsh -NoProfile -File .\tools\build.ps1 -ManagedDirectory 'C:\Games\KeyboardPlayer\开发项目_Data\Managed'
python .\tools\install.py --player 'C:\Games\KeyboardPlayer'
```

输出为 `build/Assembly-CSharp.dll`。安装脚本保留成品运行时要求的前 1024 字节编码。
随后运行副本中的 `开发项目.exe`。
`src/Game/Assembly-CSharp.csproj` 也提供 IDE 工程信息；可以用 `GameManagedDirectory` 指定本地依赖目录。
本次验证使用 PowerShell 内置编译器，未将 IDE 工程测试等同于完整 Unity 编辑器构建。

## 运行测试

```powershell
python .\tests\keyboard_smoke.py --player 'C:\Games\KeyboardPlayer' --pwsh pwsh
python .\tests\verify_delivery.py --player 'C:\Games\KeyboardPlayer'
```

驾驶测试临时编译一个模拟按键输入的测试程序集，自动进入科目二倒车入库场景，记录车辆速度与各输入信号。
测试结束后自动恢复手动输入版本，不把自动输入交付给玩家。
验证结果见 `validation/source_keyboard_summary.json` 与 `validation/delivery_summary.json`。
本次未完成图形界面人工按键试玩：窗口采集接口返回 `FrameArrived timed out` 和 `window capture timed out`。
全部地图、评分精度与联网服务也未全面验证。

## AI 写实材质阶段版本

独立“高清场景键盘版”当前已审核并装入 964 / 964 种贴图，1288 处引用，31 个资源包。全部场景贴图重绘、审核及整合已完成。 ZIP 包含本源码与全部当前批准 AI 素材、精确提示词、来源及处理脚本，不包含完整 Unity 编辑器工程或商业运行时。

全部 1288 处替换纹理通过实际 GPU 读取，科目二摄像机及模拟键盘驾驶回归通过，正式副本已恢复手动输入。全部地图的人工驾驶与评分仍未逐项验证。

## 联网配置

公开源码已经移除固定服务密钥与原业务服务地址。
默认使用本地游客路径；云登录、训练上报、充值、绑定和在线语音合成需要开发者自行配置自己的服务。
变量名见 `config/environment.example.ps1`；示例只含空值，不含真实凭据。
现有本地音频资源仍由成品提供。

## 源码来源与许可

来源与恢复修复记录见 `docs/PROVENANCE.md`。
开源许可证尚待项目所有者选择，见 `LICENSE_SELECTION.md`。项目许可不覆盖 Unity 运行时和第三方组件。
上传平台时上传本源码 ZIP 的解压内容；不要把本地 `build/`、依赖 DLL、运行日志或真实服务配置提交到仓库。
