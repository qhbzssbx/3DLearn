@echo off
setlocal

REM 设置路径
set PROTO_DIR=F:\Project\Project5\proto
set OUTPUT_DIR=%~dp0..\Assets\Scripts\GameFrame\ProtoBuf
set PROTOC_PATH=F:\Project\Project5\protoc-29.3-win64\bin\protoc.exe

REM 检查 protoc.exe 是否存在
if not exist "%PROTOC_PATH%" (
    echo Error: protoc.exe not found at %PROTOC_PATH%
    pause
    exit /b 1
)

REM 检查 .proto 文件目录是否存在
if not exist "%PROTO_DIR%" (
    echo Error: Proto directory not found at %PROTO_DIR%
    pause
    exit /b 1
)

REM 检查输出目录, 存在则删除
if exist "%OUTPUT_DIR%" (
    rmdir /S /Q "%OUTPUT_DIR%"
    mkdir "%OUTPUT_DIR%"
)
if not exist "%OUTPUT_DIR%" (
    mkdir "%OUTPUT_DIR%"
)


REM 遍历目录下的所有 .proto 文件
for %%f in ("%PROTO_DIR%\*.proto") do (
    echo Processing %%f...
    "%PROTOC_PATH%" -I="%PROTO_DIR%" --csharp_out="%OUTPUT_DIR%" "%%f"
    if errorlevel 1 (
        echo Error: Failed to generate C# code for %%f
    ) else (
        echo Success: Generated C# code for %%f
    )
)

echo Generating ProtobufCodeInfo.cs...
python "%~dp0GenC#DictProtobufClassAndMsgCode.py" -sp "%OUTPUT_DIR%" -pcp "%PROTO_DIR%"

echo All .proto files processed. Output saved to %OUTPUT_DIR%
pause