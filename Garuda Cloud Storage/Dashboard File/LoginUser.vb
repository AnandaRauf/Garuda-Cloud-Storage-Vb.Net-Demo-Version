Imports System.Data
Imports System.Data.SqlClient
Public Class LoginUser
    Private Sub LogBut_Click(sender As Object, e As EventArgs) Handles LogBut.Click
        Call konekdatabase()
        Dim SQLCmd As New SqlCommand
        Dim DR As SqlDataReader
        SQLCmd = New SqlCommand("Select * From tbl_user where  Username Like'" & UserBox.Text & "' and Password Like'" & PassBox.Text & "'", koneksiSQL)

        DR = SQLCmd.ExecuteReader

        If DR.Read Then

            UserBox.Focus()
            PassBox.Focus()
            MsgBox("Login Succesfull !")
            Me.Hide()
            GarudaDashboard.Show()


        Else
            MsgBox("Sorry Login Failed ! Wrong Email & Password !")

            UserBox.Clear()

            PassBox.Clear()

            UserBox.Focus()
        End If
    End Sub
    Private Sub QuitBut_Click(sender As Object, e As EventArgs) Handles QuitBut.Click
        Dim vb = MsgBox("Are you sure quit ?", vbQuestion + vbYesNo, "Confirmation")
        If vb = vbYes Then
            Call koneksiSQL.Close()
            Close()
        Else
            Me.Show()
        End If
    End Sub
End Class