Public Class Form5

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Opacity = 0.1
    End Sub
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Int32) As Int16

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Dim tecla2 As Boolean
        tecla2 = CBool(GetAsyncKeyState(Keys.LShiftKey))
        If tecla2 = True Then
            Label1.Text = Label1.Text + "shitf+"
            Timer3.Start()
            Timer2.Stop()
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Dim tecla3 As Boolean
        tecla3 = CBool(GetAsyncKeyState(Keys.F12))
        If tecla3 = True Then
            Label1.Text = Label1.Text + "f12"
            Timer3.Stop()
        End If
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        If Label1.Text = "Ctrol+shitf+f12" Then
            Form2.Show()
            Label1.Text = ""
            Timer1.Start()
        End If
    End Sub

    Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim tecla1 As Boolean
        tecla1 = CBool(GetAsyncKeyState(Keys.LControlKey))
        If tecla1 = True Then
            Label1.Text = "Ctrol+"
            Timer2.Start()
            Timer1.Stop()
        End If
    End Sub
End Class