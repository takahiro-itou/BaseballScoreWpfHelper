
set  solution=WpfControl
set  target=Clean


msbuild  -restore  -t:%target%  ^
    -p:Configuration="Release"  -p:Platform=x64     ^
    "%solution%.sln"

msbuild  -restore  -t:%target%  ^
    -p:Configuration="Debug"    -p:Platform=x64     ^
    "%solution%.sln"

msbuild  -restore  -t:%target%  ^
    -p:Configuration="Release"  -p:Platform=x86     ^
    "%solution%.sln"

msbuild  -restore  -t:%target%  ^
    -p:Configuration="Debug"    -p:Platform=x86     ^
    "%solution%.sln"


msbuild  -restore  -t:%target%  ^
    -p:Configuration="Release"  -p:Platform=x64     ^
    "%solution%.NetOld.sln"

msbuild  -restore  -t:%target%  ^
    -p:Configuration="Debug"    -p:Platform=x64     ^
    "%solution%.NetOld.sln"

msbuild  -restore  -t:%target%  ^
    -p:Configuration="Release"  -p:Platform=x86     ^
    "%solution%.NetOld.sln"

msbuild  -restore  -t:%target%  ^
    -p:Configuration="Debug"    -p:Platform=x86     ^
    "%solution%.NetOld.sln"
