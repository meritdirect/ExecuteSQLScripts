Imports System.Data.SqlClient
Imports System.IO

Public Class clsRunScript
    Public connectionString As String
    Public scriptFile As String
    Public reportFile As String
    Public errorMessage As String
    Public returnValue As Boolean
    Public listRow As Integer
    Public errorLevel As Integer = 0
    Public saveReport As Boolean

    Private Sub OnStatementCompleted(sender As Object, args As StatementCompletedEventArgs)
        Dim msg As String = String.Format("({0} row(s) affected)", args.RecordCount)
        'Write msg to output
        If saveReport Then
            File.AppendAllText(reportFile, msg)
            Using sr As StreamWriter = File.AppendText(reportFile)
                sr.WriteLine()
            End Using
        End If
    End Sub
    Private Function LogMessages(ByVal sender As Object, ByVal e As SqlInfoMessageEventArgs) As Boolean
        If e.Errors.Count > 0 And e.Errors(0).Number >= 16 Then
            errorLevel = e.Errors(0).Number
        End If
        If saveReport Then
            File.AppendAllText(reportFile, e.Message)
            Using sr As StreamWriter = File.AppendText(reportFile)
                sr.WriteLine()
            End Using
        End If
        Return True
    End Function
    Public Sub runScript()
        Using conn = New SqlClient.SqlConnection(connectionString)
            conn.FireInfoMessageEventOnUserErrors = True
            Try
                conn.Open()
            Catch ex As Exception
                errorMessage = ex.ToString
                returnValue = False
                Return
            End Try
            AddHandler conn.InfoMessage, Function(sender1, e1) LogMessages(sender1, e1)


            Dim strScript As String = File.ReadAllText(scriptFile)
            Using cmd As New SqlClient.SqlCommand
                AddHandler cmd.StatementCompleted, AddressOf OnStatementCompleted
                cmd.Connection = conn
                cmd.CommandType = CommandType.Text
                cmd.CommandText = strScript
                cmd.ExecuteNonQuery()

            End Using

        End Using
        returnValue = True
        Return
    End Sub

End Class
