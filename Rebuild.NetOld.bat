
setlocal
set  script_dir=%~dp0

CALL  "%script_dir%Config\Common.cnf.bat"

set  target=Rebuild


msbuild  -restore  -t:Clean     ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.NetOld.sln"

msbuild  -restore  -t:%target%  ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.NetOld.sln"
