Public Class Form4

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox1.Text <> "") Then
            My.Settings.sitius += "127.0.0.1            " + TextBox1.Text + vbCrLf
            MsgBox("Sitio web añadido con exito", MsgBoxStyle.Information, "Bloqueo")
            TextBox1.Clear()
            TextBox1.Focus()
        Else
            MsgBox("Digite algo", MsgBoxStyle.Critical, "Error")
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If My.Settings.filter Is Nothing Then
            My.Settings.filter = New System.Collections.Specialized.StringCollection
        End If
        If TextBox2.Text <> "" Then
            My.Settings.filter.Add(TextBox2.Text)
            MsgBox("Filtro añadido con exito", MsgBoxStyle.Information, "Bloqueo")
            TextBox2.Clear()
            TextBox2.Focus()
        Else
            MsgBox("Digite algo", MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox3.Text = My.Settings.correo
        ComboBox1.Text = My.Settings.ruta
        TextBox4.Text = My.Settings.user
        TextBox5.Text = My.Settings.pass
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        My.Settings.correo = TextBox3.Text
        My.Settings.ruta = ComboBox1.Text + "Windows\System32\drivers\etc\hosts"
        My.Settings.user = TextBox4.Text
        My.Settings.pass = TextBox5.Text
        MsgBox("Configuración guardada", MsgBoxStyle.Information, "Configuración")
        Me.Close()
    End Sub
End Class