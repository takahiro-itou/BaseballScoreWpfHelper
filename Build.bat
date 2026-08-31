
set  solution=WpfHelper
set  target=Build
set  config="Release"


msbuild  -restore  -t:%target%  ^
    -p:Configuration=%config%   -p:Platform=x64     ^
    "%solution%.sln"
