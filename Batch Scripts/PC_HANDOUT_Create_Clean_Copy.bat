@ECHO OFF

::Set project specific variables
SET projName=Tanks MP Workshop
SET finalPath=%USERPROFILE%\Desktop\%projName%

::Copy relevant folders to desktop (changes with each project)
xcopy /s/y/i "..\Handouts\TanksMPProject_Start\Assets" "%finalPath%\TanksMPProject_Start\Assets"
xcopy /s/y/i "..\Handouts\TanksMPProject_Start\ProjectSettings" "%finalPath%\TanksMPProject_Start\ProjectSettings"

::Copy relevant loose files to desktop (changes with each project)
