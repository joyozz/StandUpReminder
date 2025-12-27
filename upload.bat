@echo off
cd /d %~dp0

echo Configuring Git user...
"C:\Program Files\Git\bin\git.exe" config --global user.name "joyozz"
"C:\Program Files\Git\bin\git.exe" config --global user.email "2665396157@qq.com"

echo Adding files to Git...
"C:\Program Files\Git\bin\git.exe" add .

echo Committing changes...
"C:\Program Files\Git\bin\git.exe" commit -m "Complete StandUpReminder application with auto-start functionality"

echo Setting up remote repository...
"C:\Program Files\Git\bin\git.exe" remote remove origin 2>nul
"C:\Program Files\Git\bin\git.exe" remote add origin https://github.com/joyozz/StandUpReminder.git

echo Pushing to GitHub...
"C:\Program Files\Git\bin\git.exe" branch -M main
"C:\Program Files\Git\bin\git.exe" push -u origin main

pause
