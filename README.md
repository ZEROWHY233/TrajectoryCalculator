铁巢重炮 · 弹道计算器 (Nest Cannon Trajectory Helper)
https://img.shields.io/badge/.NET%2520Framework-4.8-blueviolet
https://img.shields.io/badge/license-MIT-green
https://img.shields.io/badge/PRs-welcome-brightgreen.svg

🎯 专为《铁巢重炮》玩家设计的弹道辅助工具
快速计算炮击俯仰角，支持多种弹种、装药量，并带有命中记录与主题切换功能，让您的炮击更加精准、高效。

《铁巢重炮》是一款以火炮对射为核心的策略游戏，玩家需要根据距离、弹药类型和装药量调整炮口仰角，实现对敌方堡垒的精确打击。本工具将复杂的弹道公式可视化，助您一炮中的！


✨ 功能特性（专为游戏优化）
精确弹道计算
输入目标距离（公里）、装药量（1～6）及弹种（EMPT/AP/HE/HCHE/STAR），点击计算，立即得出炮口俯仰角（度）。自动判断是否超出射程，红色警告避免无效射击。

记录管理（实战日志）
每次计算自动生成一条记录，包含 ID、水平角、距离、装药、弹种、俯仰角。所有记录可保存，方便复盘或调整战术。

命中标记（战果标注）
实战中命中目标后，选中对应记录并点击“✔ 命中”，该行背景高亮（深色主题墨绿，浅色主题浅绿），直观统计有效打击。

批量删除与清空
支持多选删除无用记录，或一键清空日志，保持列表清爽。

主题切换（适应昼夜作战）
深色/浅色主题一键切换，夜间或光线充足环境下都能清晰识读数据。

全局字体缩放
字号下拉框支持 8～30 号，无论大屏还是小屏，都能调整到最舒服的阅读尺寸。

🚀 快速开始
系统要求
Windows 7 SP1 或更高版本

.NET Framework 4.8 （若未安装，程序启动时会提示下载）

下载与运行
从 Releases 下载最新 TrajectoryCalculator.exe。

双击即可运行，无需安装，纯绿色便携。

从源码编译（为开发者）
bash
[git clone https://github.com/yourusername/TrajectoryCalculator.git](https://github.com/ZEROWHY233/IronBallisticCalc)
cd TrajectoryCalculator
# 使用 Visual Studio 2022 或更高版本打开 .sln 文件
msbuild TrajectoryCalculator.csproj /p:Configuration=Release
🛠️ 技术栈
语言：C# 7.3（兼容 .NET Framework 4.8）

框架：WPF（Windows Presentation Foundation）

UI 样式：自定义资源字典 + 动态主题切换

数据绑定：ObservableCollection<T> + INotifyPropertyChanged

构建工具：Visual Studio 2022 / MSBuild

📖 使用说明（面向玩家）
输入炮击参数

水平角（可选）：若炮位与目标有横向偏移，输入角度（度）。

距离（必填）：目标距离，单位公里（根据游戏内测距）。

装药量：下拉选择 1～6，对应游戏内装药等级。

弹种：游戏内五种弹药类型（EMPT/AP/HE/HCHE/STAR）。

点击“一键计算”

若距离在射程内，显示俯仰角并自动生成记录。

若超射程，显示红色警告，帮助您快速调整策略。

记录操作

命中标记：选中记录后点击“命中”，该行变绿（或其他高亮色）。

删除选中：移除无用记录（可多选）。

清空全部：开始新战局时重置列表。

界面个性化

点击“浅/暗”切换主题，适应不同光照环境。

字号下拉框调整全局字体大小。

🤝 贡献指南
本工具开放源代码，欢迎各位玩家或开发者参与改进！

报告 Bug：请提交 Issue，附上复现步骤。

功能建议：欢迎提出新需求，如更多弹种、自定义射程系数等。

Pull Request：请先开 Issue 讨论，确保方向一致。

开发环境准备
Visual Studio 2022（或 VS Code + C# 扩展）

.NET Framework 4.8 开发包

克隆仓库后直接打开 .exe 文件即可。

代码规范
使用 C# 7.3 语法（确保与 .NET 4.8 兼容）。

XAML 资源键命名采用 {ObjectType}{Purpose}（如 BackgroundBrush）。

提交前确保无编译警告。

📜 许可证
MIT License，您可以自由使用、修改、分发，但需保留版权声明。

💬 反馈与支持

作者邮箱：why5202022@163.com（请替换）

