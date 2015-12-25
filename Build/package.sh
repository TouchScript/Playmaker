#!/bin/bash

if [[ $# -lt 1 ]] ; then
    printf "\e[31mUsage: package.sh <package folder>\e[39m\n"
    exit 0
fi

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
PROJECT=$(cd "$DIR/../" && pwd)
PACKAGENAME=$(basename "$PROJECT")
EXPORTFOLDERS="Assets/TouchScript/Modules/$PACKAGENAME Assets/PlayMakerUnity2D"

printf "\n\e[1;36mPackaging Modules/$PACKAGENAME.\e[0;39m\n"

"$DIR/../../../Build/utils/build_package.sh" "$PROJECT" "$1/$PACKAGENAME.unitypackage" "$EXPORTFOLDERS"