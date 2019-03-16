Imports System.Data.SqlClient
Module SQL_Server_2014_Module
    Public koneksiSQL As SqlConnection

    Sub konekdatabase()
        Try
            koneksiSQL = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Data Rauf non Enkripsi Program\Database Sql Server 2014\db_dashboardSaveFile.mdf;Integrated Security=True;Connect Timeout=30")
            koneksiSQL.Open()
        Catch ex As Exception
            MsgBox("Sorry, Conecction Failed !", MsgBoxStyle.Critical, "Check Your Data Source Database !")
        End Try
    End Sub
    Public Sub jalankansql(ByVal sql As String)
        Dim SQLCmd As New SqlCommand
        konekdatabase()
        Try
            SQLCmd.Connection = koneksiSQL
            SQLCmd.CommandType = CommandType.Text
            SQLCmd.CommandText = sql
            SQLCmd.ExecuteNonQuery()
            SQLCmd.Dispose()
            koneksiSQL.Close()

            MsgBox("Your Data Has Been Saved", vbInformation)
        Catch ex As Exception
            MsgBox("Sorry, Your Data Failed Saved To Database " + "Saved To Server Failed !" & ex.Message)
        End Try
    End Sub
End Module
