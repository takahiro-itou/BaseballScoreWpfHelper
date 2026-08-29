
set  solution=WpfControl
set  target=Rebuild
set  config="Release"


msbuild  -restore  -t:Clean     ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.NetOld.sln"

msbuild  -restore  -t:%target%  ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.NetOld.sln"
