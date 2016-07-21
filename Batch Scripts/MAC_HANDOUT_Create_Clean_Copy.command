#!/bin/sh

cd "${0%/*}"
cd ..

#Set project specific variables
PROJNAME="Tanks MP Workshop"
FINALPATH="$root_vol$HOME/Desktop/$PROJNAME"

#Copy relevant folders to desktop (changes with each project)
ditto "Handouts/TanksMPProject_Start/Assets" "$FINALPATH/TanksMPProject_Start/Assets"
ditto "Handouts/TanksMPProject_Start/ProjectSettings" "$FINALPATH/TanksMPProject_Start/ProjectSettings"

#Copy relevant loose files to desktop (changes with each project)
