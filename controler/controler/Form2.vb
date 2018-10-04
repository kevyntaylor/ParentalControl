Public Class Form2

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox1.Text = My.Settings.user And TextBox2.Text = My.Settings.pass) Then
            Form1.Visible = True
            Me.Close()
        End If
    End Sub
End Class