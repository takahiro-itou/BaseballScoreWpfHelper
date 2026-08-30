
set  solution=WpfControl
set  target=Clean
set  config="Debug"


msbuild  -restore  -t:%target%  ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.sln"
