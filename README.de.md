# Stand Up Reminder

[中文](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

Eine Windows-Systemleistenanwendung, die Sie alle 40-60 Minuten daran erinnert, aufzustehen und sich zu stretchen.

## Funktionen

- 🪟 Läuft im System Tray, unterbricht Ihre Arbeit nicht
- ⏰ Erinnert alle 40-60 Minuten
- ⏱️ 10-Minuten-Countdown mit Gegenuhrzeigersinn-Animation
- 🔄 Autostart beim Systemstart
- 🎨 Blau-weiß-Thema-Oberfläche
- 📦 Einzeldatei-Bereitstellung, portable Software

## Verwendung

1. Doppelklicken Sie auf das Tray-Symbol, um das Erinnerungsfenster zu öffnen
2. Klicken Sie mit der rechten Maustaste auf das Tray-Symbol, um Autostart zu aktivieren/deaktivieren
3. Das Fenster schließt sich automatisch nach dem Countdown und wartet auf die nächste Erinnerung

## Installation

### Methode 1: Direkte Ausführung

Laden Sie alle Dateien aus dem `publish`-Ordner herunter und führen Sie `StandUpReminder.exe` aus

### Methode 2: Aus dem Quellcode erstellen

```bash
git clone https://github.com/joyozz/StandUpReminder.git
cd StandUpReminder
dotnet build -c Release
dotnet publish -c Release -r win-x64 --self-contained false -o publish
```

## Anforderungen

- Windows 10/11
- .NET 9.0 Runtime

## Technologien

- .NET 9.0 Windows Forms
- C# 12
