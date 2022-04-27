@echo off

7z >NUL 2>NUL
if %ERRORLEVEL% EQU 0 goto 7Zip_Found

echo 7z not found. Please, add it to your path.
goto end

:7zip_Found

dotnet publish -c Release
set TARGET_FOLDER=%~dp0\..\Pre-Compiled\Cecil.LINQPad.Driver.lpx6
echo %TARGET_FOLDER%

pushd Cecil.LINQPad.Driver\bin\Release\net6.0-windows\publish
del %TARGET_FOLDER%\Cecil.LINQPad.Driver.lpx6
7z a -tzip %TARGET_FOLDER%\Cecil.LINQPad.Driver.lpx6 *.dll *.xml *.pdb
popd

:end
