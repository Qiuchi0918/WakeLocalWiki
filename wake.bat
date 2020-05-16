setlocal enabledelayedexpansion
for %%j in (*.zim) do set k=!k! %%j
setlocal disabledelayedexpansion
for /f "tokens=4" %%a in ('route print^|findstr 0.0.0.0.*0.0.0.0') do (
	set IP=%%a
)

start http://%IP%:8080/
for %%i in (chrome,firefox,msedge) do (
	for /f "tokens=2" %%a in ('tasklist /FI "imagename EQ %%i.exe" /NH') do (
	kiwix-serve.exe -a %%a -d -p 8080 %k% && goto END
)
)
:END
del kiwix-serve.exe
del %0