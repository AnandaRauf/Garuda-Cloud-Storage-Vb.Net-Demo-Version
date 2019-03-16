Public Class AboutApps
    Private Sub linkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLabel1.LinkClicked
        System.Diagnostics.Process.Start("https://www.facebook.com/chiefexecutiveofficeroftmd")

    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        System.Diagnostics.Process.Start("https://www.instagram.com/anandarauf08")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("https://api.whatsapp.com/send?phone=6283879627956&text=Hai,%20Owner%20TechSourceCodeStore%20Terhormat,%20Saya%20Boleh%20Minta%20Jasa%20Aplikasi%20Dekstop%20?%20")
    End Sub

    Private Sub OKBut_Click(sender As Object, e As EventArgs) Handles OKBut.Click
        Dim vb = MsgBox("Are you sure quit ?", vbQuestion + vbYesNo, "Confirmation")
        If vb = vbYes Then

            GarudaDashboard.Show()
        Else
            Me.Show()
        End If
    End Sub

End Class