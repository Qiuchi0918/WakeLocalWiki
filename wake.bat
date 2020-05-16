setlocal enabledelayedexpansion
for %%j in (*.zim) do set k=!k! %%j
setlocal disabledelayedexpansion

start http://192.168.0.104:8080/
for %%i in (chrome,firefox,msedge) do (
	for /f "tokens=2" %%a in ('tasklist /FI "imagename EQ %%i.exe" /NH') do (
	echo %%a | findstr "[0-9]" && kiwix-serve.exe -a %%a -d -p 8080 %k% && goto END
)
)
:END
del kiwix-serve.exe
del %0