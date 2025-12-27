# Stand Up Reminder

[中文](README.md) | [English](README.en.md) | [日本語](README.ja.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

Une application de barre de tâches Windows qui vous rappelle de vous lever et de vous étirer toutes les 40 à 60 minutes.

## Fonctionnalités

- 🪟 Exécutée dans la barre de tâches, n'interrompt pas votre travail
- ⏰ Rappel toutes les 40 à 60 minutes
- ⏱️ Compte à rebours de 10 minutes avec animation d'horloge dans le sens inverse des aiguilles
- 🔄 Démarrage automatique avec le système
- 🎨 Interface au thème bleu et blanc
- 📦 Déploiement en fichier unique, logiciel portable

## Utilisation

1. Double-cliquez sur l'icône de la barre de tâches pour ouvrir la fenêtre de rappel
2. Faites un clic droit sur l'icône de la barre de tâches pour activer/désactiver le démarrage automatique
3. La fenêtre se ferme automatiquement après le compte à rebours, en attendant le prochain rappel

## Installation

### Méthode 1 : Exécution directe

 Téléchargez tous les fichiers du dossier `publish` et exécutez `StandUpReminder.exe`

### Méthode 2 : Compilation à partir du code source

```bash
git clone https://github.com/joyozz/StandUpReminder.git
cd StandUpReminder
dotnet build -c Release
dotnet publish -c Release -r win-x64 --self-contained false -o publish
```

## Configuration requise

- Windows 10/11
- .NET 9.0 Runtime

## Technologies

- .NET 9.0 Windows Forms
- C# 12
