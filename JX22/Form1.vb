﻿Imports System.Text
Imports System.Runtime.InteropServices
Imports System.String
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Runtime.CompilerServices
Imports System.Math
Imports System.Threading
Imports DotNetScanMemory_SmoLL
Imports FacilCoding
Public Class Form1

    'Dim FC As New FacilCoding
    Dim AOB As New DotNetScanMemory_SmoLL
    Dim Founds As IntPtr()
    Dim Proc = Process.GetProcessById(procID)
    Friend Declare Auto Function AU3_ControlSend64 Lib "Autoit64.dll" Alias "AU3_ControlSend" (<MarshalAs(UnmanagedType.LPWStr)> title As String, <MarshalAs(UnmanagedType.LPWStr)> text As String, <MarshalAs(UnmanagedType.LPWStr)> control As String, <MarshalAs(UnmanagedType.LPWStr)> sendText As String, mode As Integer) As Integer
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function SetWindowText(ByVal hwnd As IntPtr, ByVal lpString As String) As Boolean
    End Function

    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long
    Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As Long, ByVal lpdwProcessId As Long) As Long
    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwProcessId As Long) As Long
    Private Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As Long, ByVal lpBaseAddress As Long, ByRef lpBuffer As Byte, ByVal nSize As Long, ByVal lpNumberOfBytesWritten As Long) As Long
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Long) As Long

    Public Const PROCESS_VM_WRITE As UInteger = &H20
    Public Const PROCESS_VM_READ As UInteger = &H10
    Public Const PROCESS_VM_OPERATION As UInteger = &H8

    'Dim LastKnownPID As Integer
    ' Dim ProcessHandle As IntPtr
    Dim TargetProcess As String = "SO2Game"
    ' Const PROCESS_ALL_ACCESS = &H1F0FFF
    Dim procID As Integer
    Private Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    '    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '        Inject.Start()
    '    End Sub

    '    Private Sub Inject_Tick(sender As Object, e As EventArgs) Handles Inject.Tick

    '    End Sub
    'End Class


    Private Function GetObjectBytes(ByVal value As Object) As Byte()
        If (value.GetType Is GetType(Byte())) Then
            Return DirectCast(value, Byte())
        End If
        Dim destination As Byte() = New Byte(((Marshal.SizeOf(RuntimeHelpers.GetObjectValue(value)) - 1) + 1) - 1) {}
        Dim ptr As IntPtr = Marshal.AllocHGlobal(destination.Length)
        Marshal.StructureToPtr(RuntimeHelpers.GetObjectValue(value), ptr, True)
        Marshal.Copy(ptr, destination, 0, destination.Length)
        Marshal.FreeHGlobal(ptr)
        Return destination
    End Function


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SlcGroupBox2.Hide()
        SlCbtn2.Hide()

        'Dim hwnd As Long ' Holds the handle returned by FindWindow
        'Dim pid As Long ' Holds the Process Id
        'Dim pHandle As Long ' Holds the Process Handle
        'Dim str As String ' * 20 ' String to hold display text
        'Dim test As Long ' this var is supposed to hold the value in the memory address
        '' First get a handle to the "game" window
        'hwnd = FindWindow(vbNullString, "JX Online II (13.0)")
        'If (hwnd = 0) Then Exit Sub
        '' We can now get the pid
        'GetWindowThreadProcessId(hwnd, pid)
        '' Use the pid to get a Process Handle
        'pHandle = OpenProcess(PROCESS_VM_READ Or PROCESS_VM_WRITE Or PROCESS_VM_OPERATION, False, pid)

    End Sub

    Private Sub SlCbtn1_Click(sender As Object, e As EventArgs) Handles SlCbtn1.Click
        AppActivate(PlayerName)
        Clear.Start()
        'MsgBox(SlcComboBox1.SelectedItem)
    End Sub

    Private Sub Clear_Tick(sender As Object, e As EventArgs) Handles Clear.Tick
        WriteByte(TargetProcess, &H7CD3018, 0)
        SendKeys.Send("{ENTER}")
        SendKeys.Send("{ENTER}")

        SendKeys.Send("{ENTER}")
        SendKeys.Send("/Open('bookcompose')")
        SendKeys.Send("{ENTER}")
        Clear.Stop()
    End Sub


    Dim PlayerName As String
    Dim PlayerSect As String
    Dim PlayerLevel As String
    Private Sub PlayerChar()
        Dim num As Integer = 0
        PlayerName = ""
        Do
            Try
                PlayerName = PlayerName & Conversions.ToString(Convert.ToChar(ReadMemoryString(procID, &H76029C + num)))
            Catch exception1 As Exception
                SlcComboBox1.Text = ""
            Finally
                Label3.Text = MyBase.Name
            End Try
            num += 1
        Loop While (num <= 13)
        Label3.Text = ": " + PlayerName
    End Sub
    Private Sub getSect()
        PlayerSect = ""
        Dim num As Integer = 0
        Do
            Try
                PlayerSect = PlayerSect & Conversions.ToString(Convert.ToChar(ReadMemoryString(procID, (&H18FA10 + num))))
            Catch exception1 As Exception
                SlcComboBox1.Text = ""
            Finally
                Label6.Text = MyBase.Name
            End Try
            num += 1
        Loop While (num <= 6)
        Label6.Text = ": " + PlayerSect
    End Sub

    Private Sub GetLvl()
        Dim num As Integer = 0
        PlayerLevel = ""
        Do
            Try
                PlayerLevel = PlayerLevel & Conversions.ToString(ReadMemoryString(procID, &H18FA38 + num))
            Catch exception1 As Exception
                SlcComboBox1.Text = ""
            Finally
                Label4.Text = MyBase.Name
            End Try
            num += 1
        Loop While (num <= 0)
        Label4.Text = ": " + PlayerLevel
    End Sub
    Private Sub getExp()
        Dim offset As Integer() = New Integer() {&H20D8}
        Dim a As Double = ReadPointerDouble(procID, &H957F84, offset)
        SlcProgrssBar1.Value = CInt(Math.Round(a)) 'Processbar to load EXP %
        Label10.Text = SlcProgrssBar1.Value & "%"
        Timer1.Stop()
    End Sub

    Private Sub SlCbtn2_Click(sender As Object, e As EventArgs) Handles SlCbtn2.Click
        'PJ_QZ()

    End Sub

    Private Sub SlcComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles SlcComboBox1.SelectedIndexChanged
        procID = CInt(Convert.ToUInt64(Me.SlcComboBox1.Text.Replace("SO2Game", "")))
        Label8.Text = ": " + procID.ToString
        PlayerChar()
        getSect()
        GetLvl()
        Timer1.Start()
        Timer2.Start()
        'Label8.Text = processHandle.ToString

        'LastKnownPID = CInt(Convert.ToUInt64(SlcComboBox1.Text.Replace("SO2Game", "")))
        'Label8.Text = ": " + PIDGame.ToString
        'Dim PlayerName As String = ReadMemory(Of String)(&H76063C, &H18, False)
        'Dim Level As String = ReadMemory(Of Byte)(&H18FA38, &H2, False)
        'Dim Sect As String = ReadMemory(Of String)(&H18FA10, &H6, False)
        'Label3.Text = ": " + PlayerName
        'Label4.Text = ": " + Level
        'Label6.Text = ": " + Sect
    End Sub

    'Public pIDGame As Integer

    Dim processs As Process

    Private Sub SlcComboBox1_Click(sender As Object, e As EventArgs) Handles SlcComboBox1.Click
        SlcComboBox1.Items.Clear()

        For Each processs In Process.GetProcessesByName("SO2Game")
            SlcComboBox1.Items.Add((processs.ProcessName & processs.Id.ToString))
        Next
        'CType(sender, SLCComboBox).Items.Clear()
        'For Each p As Process In Process.GetProcessesByName(TargetProcess)
        '    PIDGame = p.Id
        '    CType(sender, SLCComboBox).Items.Add(p.ProcessName & PIDGame.ToString)
        '    ' PIDGame = p.Id
        'Next

    End Sub


    'Public Sub EXP_QZ()
    '    Founds = AOB.ScanArray(Proc, "38 12 72 00 2D 27 00 00 ?? 00 00 00 64 00")
    '    Label12.Text = Founds(0).ToString("X")
    '    AOB.WriteArray(Founds(0), "38 12 72 00 6A 3F 00 00 ?? 00 00 00 64 00")
    'End Sub

    'Public Sub PJ_QZ()
    '    Founds = AOB.ScanArray(Proc, "38 12 72 00 29 27 00 00 ?? 00 00 00 64 00")
    '    Label12.Text = Founds(0).ToString("X")
    '    AOB.WriteArray(Founds(0), "38 12 72 00 AC 3F 00 00 ?? 00 00 00 64 00")
    'End Sub


    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Application.DoEvents()
        Dim procGet = Process.GetProcessById(procID)
        'Dim processes As Process() = Process.GetProcesses
        'Dim num As Integer = procGet.Length - 1
        'Dim i As Integer = 0
        'Do While (i <= num)
        If Strings.LCase(procGet.ProcessName) = Strings.LCase("SO2Game") Then
            SetWindowText(procGet.MainWindowHandle, PlayerName)
            Timer2.Stop()
        End If
        'i += 1
        'Loop
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        getExp()
    End Sub

    Private Sub SlCbtn3_Click(sender As Object, e As EventArgs) Handles SlCbtn3.Click
        End
    End Sub

    Private Sub SlCbtn4_Click(sender As Object, e As EventArgs) Handles SlCbtn4.Click

    End Sub
End Class
