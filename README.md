# Stand Up Reminder

[中文](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

一个 Windows 系统托盘应用，每隔 40-60 分钟提醒你站起来活动一下。

## 功能特点

- 🪟 系统托盘运行，不影响工作
- ⏰ 每 40-60 分钟提醒一次
- ⏱️ 10 分钟倒计时（逆时针时钟动画）
- 🔄 支持开机自启动
- 🎨 蓝色白色主题界面
- 📦 单文件发布，绿色软件

## 使用方法

1. 双击托盘图标打开提醒窗口
2. 右键托盘图标可设置开机自启动
3. 倒计时结束后自动关闭窗口，等待下一次提醒

## 安装方法

### 方法一：直接运行

下载 `publish` 文件夹内的所有文件，直接运行 `StandUpReminder.exe`

### 方法二：源码编译

```bash
git clone https://github.com/joyozz/StandUpReminder.git
cd StandUpReminder
dotnet build -c Release
dotnet publish -c Release -r win-x64 --self-contained false -o publish
```

## 系统要求

- Windows 10/11
- .NET 9.0 Runtime

## 技术栈

- .NET 9.0 Windows Forms
- C# 12
