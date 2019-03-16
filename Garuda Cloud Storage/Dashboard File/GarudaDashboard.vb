Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports Google.Apis.Auth
Imports Google.Apis.Upload
Imports Google.Apis.Download
Imports Google.Apis.Drive.v3
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports System.Threading
Imports Google.Apis.Drive.v3.Data



Public Class GarudaDashboard
    Private GarudaService As DriveService = New DriveService
    Private GarudaViewLink As String = ""

    Private Sub CreateService()

        Dim ClientId = "895908116661-8v9oter810mijgrsdml547geccdm7tvg.apps.googleusercontent.com"
        Dim ClientSecret = "RsdfqKhA_MMzdr_B0aZ_Se2v"

        Dim MyUserCredential As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(New ClientSecrets() With {.ClientId = ClientId, .ClientSecret = ClientSecret}, {DriveService.Scope.Drive}, "user", CancellationToken.None).Result
        GarudaService = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = MyUserCredential, .ApplicationName = "Garuda Dashboard"})

    End Sub


    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        pnlStats.Height = btnHome.Height
        pnlStats.Top = btnHome.Top
        pnlFile.Visible = False
        Me.Show()

    End Sub

    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click
        pnlStats.Height = btnFile.Height
        pnlStats.Top = btnFile.Top
        pnlFile.Visible = True
    End Sub

    Private Sub btnAboutApp_Click(sender As Object, e As EventArgs) Handles btnAbtAp.Click
        AboutApps.Show()
        Me.Hide()
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnRprt.Click
        System.Diagnostics.Process.Start("https://api.whatsapp.com/send?phone=6283879627956&text=Hai,%20Owner%20TechSourceCodeStore%20Terhormat,%20Saya%20Menemukan%20Bug%20Di%20Aplikasi%20Dekstop%20Anda%20")

    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        pnlStats.Height = btnSettings.Height
        pnlStats.Top = btnSettings.Top
        pnlFile.Visible = False
    End Sub

    Private Sub LogBut_Click(sender As Object, e As EventArgs) Handles LogBut.Click
        Dim vb = MsgBox("Are you sure quit ?", vbQuestion + vbYesNo, "Confirmation")
        If vb = vbYes Then
            Call koneksiSQL.Close()
            Close()
        Else
            Me.Show()
        End If
    End Sub

    Private Sub UserPictureBox_Click(sender As Object, e As EventArgs) Handles UserPictureBox.Click
        On Error Resume Next
        OpenFileDialog1.Filter = "(*.jpg)|*.jpg|(*.bmp)|*.bmp|(*.png)|*.png|(*.ico)|*.ico|All files (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
        UserPictureBox.Load(OpenFileDialog1.FileName)
        UserPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub Lbl_Nm_Usr_Change(sender As Object, e As EventArgs) Handles Lbl_Nm_Usr.TextChanged
        Call konekdatabase()
        Dim SQLCmd As New SqlCommand

        SQLCmd = New SqlCommand("Select * From tbl_user where  Username Like'" & Lbl_Nm_Usr.Text & "''", koneksiSQL)


    End Sub

    Private Sub DownldPictBoxClick(sender As Object, e As EventArgs) Handles DownloadPictBox.Click

    End Sub

    Private Sub UploadPictBox_Click(sender As Object, e As EventArgs) Handles UploadPictBox.Click
        ' Checking if the Service is still alive, if not create the service again.
        If GarudaService.ApplicationName <> "Garuda Dashboard" Then CreateService()

        Dim oGDriveFile As New File()

        oGDriveFile.Name = "" ' Set your File Name here.
        oGDriveFile.Description = "" ' Set a meaningful description, I had set the same name as my project name
        oGDriveFile.MimeType = "application/pdf" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "file/jpg" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/png" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/doc" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/exe" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/pptx" ' You must set your MIME type carefully. Refer to the table aboveoGDriveFile.MimeType = "application/pdf" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/xl" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/xlsx" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/rar" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/zip" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/all" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "files/all files" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/ipa" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "application/xap" ' You must set your MIME type carefully. Refer to the table above
        oGDriveFile.MimeType = "(*.jpg)|*.jpg|(*.bmp)|*.bmp|(*.png)|*.png|(*.ico)|*.ico|All files (*.*)|*.*" ' You must set your MIME type carefully. Refer to the table above
        Dim bArrByteArray As Byte() = System.IO.File.ReadAllBytes("") ' Your File Path from where you would want to upload from.
        Dim oStream As New System.IO.MemoryStream(bArrByteArray)

        Dim oUploadRequest As FilesResource.CreateMediaUpload

        oUploadRequest = GarudaService.Files.Create(oGDriveFile, oStream, oGDriveFile.MimeType)

        oUploadRequest.Fields = "id"
        oUploadRequest.Alt = FilesResource.CreateMediaUpload.AltEnum.Json

        oUploadRequest.Upload()

        Dim oFile As File = oUploadRequest.ResponseBody

        ' Setting this permission will allow anyone having the link to directly download the file. 
        ' Google Drive, will not show any login page to the user, while attempting to download the file.
        ' The below two blocks are imperative.
        Dim permission As New Google.Apis.Drive.v3.Data.Permission()
        permission.Type = "anyone"
        permission.Role = "reader"
        permission.AllowFileDiscovery = True

        Dim request As PermissionsResource.CreateRequest = GarudaService.Permissions.Create(permission, oFile.Id)
        request.Fields = "id"
        request.Execute()
        MessageBox.Show("Upload Success")

        If Not IsNothing(oFile) Then

            GarudaViewLink = " https://drive.google.com/uc?id=" & oFile.Id
        Else
            Cursor.Current = Cursors.Default
            MessageBox.Show("Unable to contact Google Drive. Check your Connection.", "Title", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        End If

    End Sub




End Class