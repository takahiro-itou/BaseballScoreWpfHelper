
setlocal
set  script_dir=%~dp0

CALL  "%script_dir%Config\Common.cnf.bat"

set  target=Clean


msbuild  -restore  -t:%target%  ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.sln"
