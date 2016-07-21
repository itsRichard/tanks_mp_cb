@ECHO OFF

::Set project specific variables
SET projName=FINAL_TanksMP_Project
SET finalPath=%USERPROFILE%\Desktop\%projName%

::Copy relevant folders to desktop (changes with each project)
xcopy /s/y/i "..\TanksMP_Project\Assets" "%finalPath%\Assets"
xcopy /s/y/i "..\TanksMP_Project\ProjectSettings" "%finalPath%\ProjectSettings"

::Copy relevant loose files to desktop (changes with each project)
::copy /y 