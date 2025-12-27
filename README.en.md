# Stand Up Reminder

[中文](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

A Windows system tray application that reminds you to stand up and stretch every 40-60 minutes.

## Features

- 🪟 Runs in system tray, won't interrupt your work
- ⏰ Reminds every 40-60 minutes
- ⏱️ 10-minute countdown with counterclockwise clock animation
- 🔄 Auto-start on system boot
- 🎨 Blue and white themed interface
- 📦 Single-file deployment, green software

## Usage

1. Double-click the tray icon to open the reminder window
2. Right-click the tray icon to enable/disable auto-start
3. The window automatically closes after countdown, waiting for the next reminder

## Installation

### Method 1: Direct Run

Download all files from the `publish` folder and run `StandUpReminder.exe`

### Method 2: Build from Source

```bash
git clone https://github.com/joyozz/StandUpReminder.git
cd StandUpReminder
dotnet build -c Release
dotnet publish -c Release -r win-x64 --self-contained false -o publish
```

## Requirements

- Windows 10/11
- .NET 9.0 Runtime

## Tech Stack

- .NET 9.0 Windows Forms
- C# 12
