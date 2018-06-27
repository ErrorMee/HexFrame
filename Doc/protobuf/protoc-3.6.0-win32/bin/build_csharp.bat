:: c++ :: protoc --cpp_out=./ *.proto

:: 定义.proto目录
SET SRC_DIR=./proto_src
:: 定义c#目录
SET DST_DIR=./csharp_prod

protoc -I=%SRC_DIR% --csharp_out=%DST_DIR% %SRC_DIR%/addressbook.proto

pause