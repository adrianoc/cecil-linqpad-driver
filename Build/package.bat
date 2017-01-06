REM msbuild Cecil.LINQPad.Driver\Cecil.LINQPad.Driver.csproj
pushd Cecil.LINQPad.Driver\bin\Debug
del ..\..\..\Pre-Compiled\Cecil.LINQPad.Driver.lpx
7z a -tzip ..\..\..\Pre-Compiled\Cecil.LINQPad.Driver.lpx *.dll *.xml *.pdb
popd