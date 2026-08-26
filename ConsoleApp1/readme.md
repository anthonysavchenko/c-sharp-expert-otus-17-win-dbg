# Анализ ConsoleApp1

1. Установил `dotnet-dump` и `WinDbg`. Запустил приложение `ConsoleApp1.exe`. Нашел его PID с помощью `dotnet-dump ps`. Создал дамп [dump.dmp](./dump.dmp) с помощью `dotnet-dump collect -p 23396`

2. Открыл дамп в WinDbg. С помощью команды `~e* !clrstack` увидел в стэке вызовов двух потоков (4 и 5) вход в критическую секцию кода `System.Threading.Monitor.Enter`

```
0:000> ~*e !clrstack
...
OS Thread Id: 0x2f70 (4)
        Child SP               IP Call Site
000000958D57F900 00007ffb87100ed4 [InlinedCallFrame: 000000958d57f900]
000000958D57F900 00007ffb1d8a3bc7 [InlinedCallFrame: 000000958d57f900]
000000958D57F8D0 00007ffb1d8a3bc7 System.Threading.Monitor.Enter_Slowpath(System.Object) [/_/src/runtime/src/coreclr/System.Private.CoreLib/src/System/Threading/Monitor.CoreCLR.cs @ 73]
000000958D57F9A0 00007ffb1d8a3d53 System.Threading.Monitor.Enter(System.Object, Boolean ByRef) [/_/src/runtime/src/coreclr/System.Private.CoreLib/src/System/Threading/Monitor.CoreCLR.cs @ 121]
000000958D57F9E0 00007ffabe964998 ConsoleApp1.Process.Method1() [C:\Code\github\c-sharp-expert-otus-17-win-dbg\ConsoleApp1\Process.cs @ 16]
000000958D57FA30 00007ffb1d8a48bb System.Threading.Thread.StartCallback()
000000958D57FC80 00007ffb1e5b3893 [DebuggerU2MCatchHandlerFrame: 000000958d57fc80]
OS Thread Id: 0x6b90 (5)
        Child SP               IP Call Site
000000958D87F5D0 00007ffb87100ed4 [InlinedCallFrame: 000000958d87f5d0]
000000958D87F5D0 00007ffb1d8a3bc7 [InlinedCallFrame: 000000958d87f5d0]
000000958D87F5A0 00007ffb1d8a3bc7 System.Threading.Monitor.Enter_Slowpath(System.Object) [/_/src/runtime/src/coreclr/System.Private.CoreLib/src/System/Threading/Monitor.CoreCLR.cs @ 73]
000000958D87F670 00007ffb1d8a3d53 System.Threading.Monitor.Enter(System.Object, Boolean ByRef) [/_/src/runtime/src/coreclr/System.Private.CoreLib/src/System/Threading/Monitor.CoreCLR.cs @ 121]
000000958D87F6B0 00007ffabe964b48 ConsoleApp1.Process.Method2() [C:\Code\github\c-sharp-expert-otus-17-win-dbg\ConsoleApp1\Process.cs @ 31]
000000958D87F700 00007ffb1d8a48bb System.Threading.Thread.StartCallback()
000000958D87F950 00007ffb1e5b3893 [DebuggerU2MCatchHandlerFrame: 000000958d87f950]
```

3. С помощью команды `!syncblk` увидел, что те же самые потоки (4 и 5) владеют активными блокировками, которые находятся в статусе MonitorHeld = 3 (то есть есть ожидающие блокировку другие потоки). Таким образом можно предположить, что оба потока ожидают блокировки, захваченные друг-другом. То есть наблюдаем взаимную блокировку (deadlock)

```
0:004> !syncblk
Index         SyncBlock MonitorHeld Recursion Owning Thread Info          SyncBlock Owner
    1 0000028A3FA86E58            3         1 0000028A3FA82DD0 2f70   4   0000027a8b09f0c8 System.Object
    2 0000028A3FA86EB0            3         1 0000028A3FA83100 6b90   5   0000027a8b09f0e0 System.Object
-----------------------------
Total           2
CCW             0
RCW             0
ComClassFactory 0
Free            0
```
