Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.VisualBasic.CompilerServices

Module ReadWritingMemory

    <DllImport("kernel32", EntryPoint:="ReadProcessMemory", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
    Private Function ReadMemoryByte(ByVal Handle As Integer, ByVal Address As Integer, ByRef Value As Byte, ByVal Optional Size As Integer = 2, ByRef Optional Bytes As Integer = 0) As Byte
    End Function

    <DllImport("kernel32", EntryPoint:="WriteProcessMemory", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
    Private Function WriteMemoryInteger(ByVal Handle As Integer, ByVal Address As Integer, ByRef Value As Integer, ByVal Optional Size As Integer = 4, ByRef Optional Bytes As Integer = 0) As Integer
    End Function

    <DllImport("kernel32", EntryPoint:="ReadProcessMemory", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
    Private Function ReadMemoryInteger(ByVal Handle As Integer, ByVal Address As Integer, ByRef Value As Integer, ByVal Optional Size As Integer = 4, ByRef Optional Bytes As Integer = 0) As Integer
    End Function


    Public Function ReadByte(ByVal EXENAME As String, ByVal Address As Integer) As Byte
        Dim num2 As Byte

        If Process.GetProcessesByName(EXENAME).Length > 0 Then
            Dim handle As Integer = CInt(Process.GetProcessesByName(EXENAME)(0).Handle)

            If handle > 0 Then
                Dim bytes As Integer = 0
                ReadMemoryByte(handle, Address, num2, 2, bytes)
            End If
        End If

        Return num2
    End Function

    Public Function ReadPointerIntegerCustom(ByVal ID As Integer, ByVal Pointer As Integer, ByVal ParamArray Offset As Integer()) As Integer
        Dim num2 As Integer
        If (Process.GetProcessById(ID).ToString.Length > 0) Then
            Dim num6 As Integer
            Dim handle As Integer = CInt(Process.GetProcessById(ID).Handle)
            If (handle <= 0) Then
                Return num2
            End If
            Dim num5 As Integer
            For Each num5 In Offset
                num6 = 0
                ReadMemoryInteger(handle, Pointer, Pointer, 4, num6)
                Pointer = (Pointer + num5)
            Next
            num6 = 0
            ReadMemoryInteger(handle, Pointer, num2, 4, num6)
        End If
        Return num2
    End Function



    Public Function ReadDouble(ByVal EXENAME As String, ByVal Address As Integer) As Double
        Dim num2 As Double
        If (Process.GetProcessesByName(EXENAME).Length > 0) Then
            Dim handle As Integer = CInt(Process.GetProcessesByName(EXENAME)(0).Handle)
            If (handle > 0) Then
                Dim num4 As Byte = CByte(Math.Round(num2))
                Dim bytes As Integer = 0
                ReadMemoryByte(handle, Address, num4, 2, bytes)
                num2 = num4
            End If
        End If
        Return num2
    End Function




    Public Function ReadPointerInteger(ByVal EXENAME As String, ByVal Pointer As Integer, ByVal ParamArray Offset As Integer()) As Integer
        Dim num2 As Integer
        If (Process.GetProcessesByName(EXENAME).Length > 0) Then
            Dim num6 As Integer
            Dim handle As Integer = CInt(Process.GetProcessesByName(EXENAME)(0).Handle)
            If (handle <= 0) Then
                Return num2
            End If
            Dim num5 As Integer
            For Each num5 In Offset
                num6 = 0
                ReadMemoryInteger(handle, Pointer, Pointer, 4, num6)
                Pointer = (Pointer + num5)
            Next
            num6 = 0
            ReadMemoryInteger(handle, Pointer, num2, 4, num6)
        End If
        Return num2
    End Function




    Public Sub WritePointerInteger(ByVal EXENAME As String, ByVal Pointer As Integer, ByVal Value As Integer, ByVal ParamArray Offset As Integer())
        If (Process.GetProcessesByName(EXENAME).Length > 0) Then
            Dim handle As Integer = CInt(Process.GetProcessesByName(EXENAME)(0).Handle)
            If (handle > 0) Then
                Dim num4 As Integer
                Dim num3 As Integer
                For Each num3 In Offset
                    num4 = 0
                    ReadMemoryInteger(handle, Pointer, Pointer, 4, num4)
                    Pointer = (Pointer + num3)
                Next
                num4 = 0
                WriteMemoryInteger(handle, Pointer, Value, 4, num4)
            End If
        End If
    End Sub


    Public Sub WritePointerIntegerCustom(ByVal ID As Integer, ByVal Pointer As Integer, ByVal Value As Integer, ByVal ParamArray Offset As Integer())
        If (Process.GetProcessById(ID).ToString.Length > 0) Then
            Dim handle As Integer = CInt(Process.GetProcessById(ID).Handle)
            If (handle > 0) Then
                Dim num4 As Integer
                Dim num3 As Integer
                For Each num3 In Offset
                    num4 = 0
                    ReadMemoryInteger(handle, Pointer, Pointer, 4, num4)
                    Pointer = (Pointer + num3)
                Next
                num4 = 0
                WriteMemoryInteger(handle, Pointer, Value, 4, num4)
            End If
        End If
    End Sub



    Public Function ReadMemoryString(ByVal ID As Integer, ByVal Address As Integer) As Byte
        Dim num2 As Byte

        If Process.GetProcessById(ID).ToString().Length > 0 Then
            Dim handle As Integer = CInt(Process.GetProcessById(ID).Handle)

            If handle > 0 Then
                Dim bytes As Integer = 0
                ReadMemoryByte(handle, Address, num2, 2, bytes)
            End If
        End If

        Return num2
    End Function

    '<DllImport("kernel32.dll", SetLastError:=True)>
    'Private Function ReadProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, <Out()> ByVal lpBuffer() As Byte, ByVal dwSize As IntPtr, ByRef lpNumberOfBytesRead As IntPtr) As Boolean
    'End Function

    '<DllImport("kernel32.dll", SetLastError:=True)>
    'Private Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    'End Function

    '<DllImport("kernel32.dll")>
    'Private Function OpenProcess(ByVal dwDesiredAccess As UInt32, <MarshalAs(UnmanagedType.Bool)> ByVal bInheritHandle As Boolean, ByVal dwProcessId As Integer) As IntPtr
    'End Function

    'Public Const PROCESS_VM_WRITE As UInteger = &H20
    'Public Const PROCESS_VM_READ As UInteger = &H10
    'Public Const PROCESS_VM_OPERATION As UInteger = &H8
    'Public TargetProcess As String = "SO2Game"
    'Public ProcessHandle As IntPtr = IntPtr.Zero
    'Public LastKnownPID As Integer = -1

    'Public PIDGame As Integer
    'Dim ppid As Process

    'Private Function ProcessIDExists(ByVal pID As Integer) As Boolean
    '    For Each p As Process In Process.GetProcessesByName(TargetProcess)
    '        If p.Id = PIDGame Then Return True

    '    Next

    '    Return False
    'End Function

    'Public Function ReadByte(ByVal EXENAME As String, ByVal Address As Integer) As Byte
    '    Dim num2 As Byte

    '    If Process.GetProcessesByName(EXENAME).Length > 0 Then
    '        Dim handle As Integer = CInt(Process.GetProcessesByName(EXENAME)(0).Handle)

    '        If handle > 0 Then
    '            Dim bytes As Integer = 0
    '            ReadMemoryByte(handle, Address, num2, 2, bytes)
    '        End If
    '    End If

    '    Return num2
    'End Function

    'Public Function ReadString(ByVal ID As Integer, ByVal Address As Integer) As Byte
    '    Dim num2 As Byte

    '    If Process.GetProcessById(ID).ToString().Length > 0 Then
    '        Dim handle As Integer = CInt(Process.GetProcessById(ID).Handle)

    '        If handle > 0 Then
    '            Dim bytes As Integer = 0
    '            ReadMemoryByte(handle, Address, num2, 2, bytes)
    '        End If
    '    End If

    '    Return num2
    'End Function

    'Public Sub SetProcessName(ByVal processName As String)
    '    TargetProcess = processName
    '    If ProcessHandle <> IntPtr.Zero Then CloseHandle(ProcessHandle)
    '    LastKnownPID = -1
    '    ProcessHandle = IntPtr.Zero
    '    ppid = Process.GetProcessById(PIDGame)
    'End Sub

    'Public Function GetCurrentProcessName() As String
    '    Return TargetProcess
    'End Function

    'Public Function UpdateProcessHandle() As Boolean
    '    If LastKnownPID = -1 OrElse Not ProcessIDExists(LastKnownPID) Then
    '        If ProcessHandle <> IntPtr.Zero Then CloseHandle(ProcessHandle)
    '        Dim p() As Process = Process.GetProcessesByName(TargetProcess)
    '        If p.Length = 0 Then Return False
    '        LastKnownPID = p(0).Id
    '        ProcessHandle = OpenProcess(PROCESS_VM_READ Or PROCESS_VM_WRITE Or PROCESS_VM_OPERATION, False, PIDGame)
    '        If ProcessHandle = IntPtr.Zero Then Return False
    '    End If
    '    Return True
    'End Function

    'Public Function ReadMemory(Of T)(ByVal address As Object) As T
    '    Return ReadMemory(Of T)(CLng(address))
    'End Function

    'Public Function ReadMemory(Of T)(ByVal address As Integer) As T
    '    Return ReadMemory(Of T)(New IntPtr(address), 0, False)
    'End Function

    'Public Function ReadMemory(Of T)(ByVal address As Long) As T
    '    Return ReadMemory(Of T)(New IntPtr(address), 0, False)
    'End Function

    'Public Function ReadMemory(Of T)(ByVal address As IntPtr) As T
    '    Return ReadMemory(Of T)(address, 0, False)
    'End Function

    'Public Function ReadMemory(ByVal address As IntPtr, ByVal length As Integer) As Byte()
    '    Return ReadMemory(Of Byte())(address, length, False)
    'End Function

    'Public Function ReadMemory(ByVal address As Integer, ByVal length As Integer) As Byte()
    '    Return ReadMemory(Of Byte())(New IntPtr(address), length, False)
    'End Function

    'Public Function ReadMemory(ByVal address As Long, ByVal length As Integer) As Byte()
    '    Return ReadMemory(Of Byte())(New IntPtr(address), length, False)
    'End Function

    'Public Function ReadMemory(Of T)(ByVal address As IntPtr, ByVal length As Integer, ByVal unicodeString As Boolean) As T
    '    Dim buffer() As Byte
    '    If GetType(T) Is GetType(String) Then
    '        If unicodeString Then buffer = New Byte(length * 2 - 1) {} Else buffer = New Byte(length - 1) {}
    '    ElseIf GetType(T) Is GetType(Byte()) Then
    '        buffer = New Byte(length - 1) {}
    '    Else
    '        buffer = New Byte(Marshal.SizeOf(GetType(T)) - 1) {}
    '    End If
    '    If Not UpdateProcessHandle() Then Return Nothing
    '    Dim success As Boolean = ReadProcessMemory(, address, buffer, New IntPtr(buffer.Length), IntPtr.Zero)
    '    If Not success Then Return Nothing
    '    If GetType(T) Is GetType(Byte()) Then Return CType(CType(buffer, Object), T)
    '    If GetType(T) Is GetType(String) Then
    '        If unicodeString Then Return CType(CType(Encoding.Unicode.GetString(buffer), Object), T)
    '        Return CType(CType(Encoding.ASCII.GetString(buffer), Object), T)
    '    End If
    '    Dim gcHandle As GCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned)
    '    Dim returnObject As T = CType(Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject, GetType(T)), T)
    '    gcHandle.Free()
    '    Return returnObject
    'End Function

    'Private Function GetObjectBytes(ByVal value As Object) As Byte()
    '    If value.GetType() Is GetType(Byte()) Then Return CType(value, Byte())
    '    Dim buffer(Marshal.SizeOf(value) - 1) As Byte
    '    Dim ptr As IntPtr = Marshal.AllocHGlobal(buffer.Length)
    '    Marshal.StructureToPtr(value, ptr, True)
    '    Marshal.Copy(ptr, buffer, 0, buffer.Length)
    '    Marshal.FreeHGlobal(ptr)
    '    Return buffer
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As Object, ByVal value As T) As Boolean
    '    Return WriteMemory(CLng(address), value)
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As Object, ByVal value As Object) As Boolean
    '    Return WriteMemory(CLng(address), CType(value, T))
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As Integer, ByVal value As T) As Boolean
    '    Return WriteMemory(New IntPtr(address), value)
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As Integer, ByVal value As Object) As Boolean
    '    Return WriteMemory(address, CType(value, T))
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As Long, ByVal value As T) As Boolean
    '    Return WriteMemory(New IntPtr(address), value)
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As Long, ByVal value As Object) As Boolean
    '    Return WriteMemory(address, CType(value, T))
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As IntPtr, ByVal value As T) As Boolean
    '    Return WriteMemory(address, value, False)
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As IntPtr, ByVal value As Object) As Boolean
    '    Return WriteMemory(address, CType(value, T), False)
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As Object, ByVal value As T, ByVal unicode As Boolean) As Boolean
    '    Return WriteMemory(CLng(address), value, unicode)
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As Integer, ByVal value As T, ByVal unicode As Boolean) As Boolean
    '    Return WriteMemory(New IntPtr(address), value, unicode)
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As Long, ByVal value As T, ByVal unicode As Boolean) As Boolean
    '    Return WriteMemory(New IntPtr(address), value, unicode)
    'End Function

    'Public Function WriteMemory(Of T)(ByVal address As IntPtr, ByVal value As T, ByVal unicode As Boolean) As Boolean
    '    If Not UpdateProcessHandle() Then Return False
    '    Dim buffer() As Byte
    '    If TypeOf value Is String Then
    '        If unicode Then buffer = Encoding.Unicode.GetBytes(value.ToString()) Else buffer = Encoding.ASCII.GetBytes(value.ToString())
    '    Else
    '        buffer = GetObjectBytes(value)
    '    End If
    '    Dim result As Boolean = WriteProcessMemory(ProcessHandle, address, buffer, New IntPtr(buffer.Length), IntPtr.Zero)
    '    Return result
    'End Function
End Module


'Imports System.Math
'Imports System.Threading
'Imports System.Runtime.InteropServices

'Module ReadWritingMemory


'    '//Guess what this does ;D
'    Public Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer

'    '//Allows us to read a float from the memory.
'    Private Declare Function ReadProcessMemory Lib "kernel32" _
'    Alias "ReadProcessMemory" (ByVal hProcess As Integer, _
'                               ByVal lpBaseAddress As Integer, _
'                               ByRef lpBuffer As Single, _
'                               ByVal nSize As Integer, _
'                               ByRef lpNumberOfBytesWritten As Integer) As Integer

'    '//Allows us to read an integer or string from the memory. String will require a loop.
'    Private Declare Function ReadProcessMemoryInt Lib "kernel32" _
'        Alias "ReadProcessMemory" (ByVal hProcess As Integer, _
'                                   ByVal lpBaseAddress As Integer, _
'                                   ByRef lpBuffer As Integer, _
'                                   ByVal nSize As Integer, _
'                                   ByRef lpNumberOfBytesWritten As Integer) As Integer

'    '//Allows us to find a memory address when provided with a pointer and offsets
'    Private Declare Function ReadProcessMemoryPointer Lib "kernel32" Alias "ReadProcessMemory" ( _
'       ByVal hProcess As IntPtr, _
'       ByVal lpBaseAddress As IntPtr, _
'       <Out()> ByVal lpBuffer() As Byte, _
'       ByVal dwSize As Integer, _
'       ByRef lpNumberOfBytesRead As Integer) As Boolean

'    '//Close a handle
'    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer

'    '//Shortcut to get read & write access to an application
'    Const PROCESS_ALL_ACCESS = &H1F0FFF

'    ' //Function to get string from memory
'    Public Function memstring(ByVal address As Long, ByVal length As Int32, ByVal processHandle As IntPtr)
'        Dim stringinmemory As Long
'        Dim ret1 As Byte() = Nothing
'        Dim tStr(length) As Char
'        Dim retStr As String = ""
'        For i As Int32 = 0 To length - 1
'            ReadProcessMemoryInt(processHandle, address + i, stringinmemory, 1, 0)
'            ret1 = BitConverter.GetBytes(stringinmemory)
'            tStr(i) = System.Text.Encoding.ASCII.GetString(ret1) : retStr += tStr(i)
'        Next
'        Return retStr
'    End Function

'    '//Function to get float from memory. I don't know what those comments are all about, it's been a long time since I used this code.
'    Public Function memfloat(ByVal address As Long, ByVal processHandle As IntPtr)
'        Dim floatvalueinmemory As Single
'        ReadProcessMemory(processHandle, address, floatvalueinmemory, 4, 0)
'        '//Floatvalueinmemory didn't give the desired result, so going to try to TryParse
'        Dim letstryagain As Single
'        Single.TryParse(floatvalueinmemory, letstryagain)
'        '//Unfortunately returns the same result as floatvalueinmemory did
'        Return CStr(letstryagain)
'    End Function

'    '//Function to get int from memory
'    Public Function memInt(ByVal address As Long, ByVal processHandle As IntPtr)
'        Dim intvalueinmemory As Integer
'        ReadProcessMemoryInt(processHandle, address, intvalueinmemory, 4, 0)
'        Return CStr(intvalueinmemory)
'    End Function

'    '//Function to find a memory address when provided with a pointer and offsets
'    Private Function FindAddress(ByVal pHandle As IntPtr, ByVal BaseAddress As IntPtr, ByVal StaticPointer As IntPtr, ByVal Offsets() As IntPtr) As IntPtr
'        ' // Create a buffer that is 4 bytes on a 32-bit system or 8 bytes on a 64-bit system.
'        Dim tmp(IntPtr.Size - 1) As Byte
'        Dim Address As IntPtr = BaseAddress
'        '// We must check for 32-bit vs 64-bit.
'        If IntPtr.Size = 4 Then
'            Address = New IntPtr(Address.ToInt32 + StaticPointer.ToInt32)
'        Else
'            Address = New IntPtr(Address.ToInt64 + StaticPointer.ToInt64)
'        End If
'        '// Loop through each offset to find the address
'        For i As Integer = 0 To Offsets.Length - 1
'            ReadProcessMemoryPointer(pHandle, Address, tmp, IntPtr.Size, 0)
'            If IntPtr.Size = 4 Then
'                Address = BitConverter.ToInt32(tmp, 0) + Offsets(i).ToInt32()
'            Else
'                Address = BitConverter.ToInt64(tmp, 0) + Offsets(i).ToInt64()
'            End If
'        Next
'        Return Address
'    End Function

'    Partial Public Class NativeMethods
'        <DllImport("user32.dll")> _
'        Public Shared Function ReadProcessMemory(ByVal hProcess As System.IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As System.IntPtr, ByVal nSize As UInteger, ByVal lpNumberOfBytesRead As IntPtr) As Boolean
'        End Function
'    End Class


'End Module

