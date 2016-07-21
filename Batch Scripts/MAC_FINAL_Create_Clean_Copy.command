#!/bin/sh

cd "${0%/*}"
cd ..

#Set project specific variables
PROJNAME="FINAL_TanksMP_Project"
FINALPATH="$root_vol$HOME/Desktop/$PROJNAME"

#Copy relevant folders to desktop (changes with each project)
ditto "TanksMP_Project/Assets" "$FINALPATH/Assets"
ditto "TanksMP_Project/ProjectSettings" "$FINALPATH/ProjectSettings"

#Copy relevant loose files to desktop (changes with each project)
#copy /y 