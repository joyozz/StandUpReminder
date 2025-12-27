# Stand Up Reminder

[中文](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

40〜60 分ごとに立ち上がって体を動かすよう促す Windows システムトレイアプリケーションです。

## 機能

- 🪟 システムトレイで実行、作業の妨げになりません
- ⏰ 40〜60 分ごとにリマインダーを表示
- ⏱️ 10 分カウントダウン（反時計回りアニメーション）
- 🔄 スタートアップ対応
- 🎨 青と白のテーマインターフェース
- 📦 単一ファイル展開、グリーンソフトウェア

## 使い方

1. トレイアイコンをダブルクリックしてリマインダーウィンドウを開く
2. トレイアイコンを右クリックしてスタートアップを有効/無効に設定
3. カウントダウン後、ウィンドウが自動的に閉じて次のリマインダーを待ちます

## インストール方法

### 方法 1: 直接実行

`publish` フォルダ内のすべてのファイルをダウンロードし、`StandUpReminder.exe` を実行してください

### 方法 2: ソースからビルド

```bash
git clone https://github.com/joyozz/StandUpReminder.git
cd StandUpReminder
dotnet build -c Release
dotnet publish -c Release -r win-x64 --self-contained false -o publish
```

## 必要環境

- Windows 10/11
- .NET 9.0 Runtime

## 技術スタック

- .NET 9.0 Windows Forms
- C# 12
