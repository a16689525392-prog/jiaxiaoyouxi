# 来源与恢复记录

- 输入：开发者提供的 Unity 2019.4.9f1 Windows Mono 成品。
- 原始业务程序集 SHA-256：`976e033d9b4dc02b5afb8aa38207e845c7898797a9b1a2889f5574a0419eaab7`。
- C# 恢复工具：[ILSpy / ICSharpCode.Decompiler 9.1.0.7988](https://www.nuget.org/packages/ICSharpCode.Decompiler/9.1.0.7988)。工具本身未打入源码 ZIP。
- 恢复输出：418 个 C# 文件。没有原始调试符号或原版 Unity 工程。
- 新增行为：授权检查通过；键盘输入替代 USB 采集；在原版车辆控制前调用本地动力学；低速半离合辅助。
- 恢复修复：恢复 JsonData / JsonMockWrapper 的显式 `IDictionary.Keys` 声明；将诊断方法名恢复为 `JudgeSoftCorrect`；编译时统一旧版本 System 程序集引用。
- 公开配置：联网服务的 AES 参数、服务 URL、语音合成凭据改为环境变量；移除访问令牌日志。未配置时不启用对应网络功能。
- 编译验证：从本包 C# 代码生成新的 `Assembly-CSharp.dll`，将其安装到独立成品副本并通过模拟键盘驾驶测试。
- 未恢复内容：Unity 编辑器场景、资源原文件、项目 `.meta` 关联、原版构建流水线。

游戏程序集中的通用工具、JSON 库和 Unity 组件代码随程序集一并恢复。本文记录技术来源，不替第三方组件指定新许可证。
